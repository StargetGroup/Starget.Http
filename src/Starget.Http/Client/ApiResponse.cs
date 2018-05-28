using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public class ApiResponse
    {
        public string Content { get; set; }

        public bool StatusIsSucceed { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public virtual async Task DeserializeMessageAsync(HttpResponseMessage message)
        {
            this.StatusIsSucceed = message.IsSuccessStatusCode;
            this.StatusCode = message.StatusCode;
            this.StatusMessage = message.ReasonPhrase;
            this.Content = await message.Content.ReadAsStringAsync();
        }
    }

    public class ApiResponse<T>:ApiResponse where T:class,new()
    {
        public T Data { get; set; }

        public async Task DeserializeMessageAsync(HttpResponseMessage message, ApiResultBuildOption<T> resultOption = null)
        {
            await base.DeserializeMessageAsync(message);
            if (this.StatusIsSucceed)
            {
                if(resultOption?.DeserializeObjectCallBack != null)
                {
                    this.Data = resultOption?.DeserializeObjectCallBack(message);
                }
                else
                {
                    this.Data = JsonConvert.DeserializeObject<T>(this.Content);
                }

                if (resultOption?.SetModelStatus??false)
                {
                    SetModelStatus();
                }
            }
            else if (resultOption?.SetModelStatus ?? false)
            {
                this.Data = new T();
                SetModelStatus();
            }
        }

        public void SetModelStatus()
        {
            if (this.Data == null)
            {
                this.Data = new T();
            }
            var type = this.Data.GetType();
            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                var attrs = p.GetCustomAttributes(typeof(ApiStatusCodeAttribute), true);
                if (attrs != null && attrs.Count() > 0)
                {
                    if (typeof(HttpStatusCode).IsAssignableFrom(p.PropertyType))
                    {
                        p.SetValue(this.Data, this.StatusCode);
                    }
                    else if (typeof(int).IsAssignableFrom(p.PropertyType) || typeof(Int32).IsAssignableFrom(p.PropertyType))
                    {
                        p.SetValue(this.Data, (int)this.StatusCode);
                    }
                    else if (typeof(string).IsAssignableFrom(p.PropertyType) || typeof(String).IsAssignableFrom(p.PropertyType))
                    {
                        p.SetValue(this.Data, this.StatusCode.ToString());
                    }
                    continue;
                }
                attrs = p.GetCustomAttributes(typeof(ApiStatusMessageAttribute), true);
                if (attrs != null && attrs.Count() > 0)
                {
                    if (typeof(string).IsAssignableFrom(p.PropertyType) || typeof(String).IsAssignableFrom(p.PropertyType))
                    {
                        p.SetValue(this.Data, this.StatusMessage);
                    }
                }
                attrs = p.GetCustomAttributes(typeof(ApiStatusIsSucceedAttribute), true);
                if (attrs != null && attrs.Count() > 0)
                {
                    if (typeof(bool).IsAssignableFrom(p.PropertyType) || typeof(Boolean).IsAssignableFrom(p.PropertyType))
                    {
                        p.SetValue(this.Data, this.StatusIsSucceed);
                    }
                }
            }
        }
    }
}
