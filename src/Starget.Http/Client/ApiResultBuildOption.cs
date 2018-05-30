using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Starget.Http.Client
{
    public class ApiResultBuildOption:ICloneable
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public bool SetModelStatus { get; set; }

        public virtual object Clone()
        {
            ApiResultBuildOption option = new ApiResultBuildOption();
            option.JsonSerializerSettings = this.JsonSerializerSettings;
            option.SetModelStatus = this.SetModelStatus;
            return option;
        }
    }

    public class ApiResultBuildOption<T> : ApiResultBuildOption where T : class, new()
    {
        public Func<HttpResponseMessage, T> DeserializeObjectCallBack { get; set; }
        public static ApiResultBuildOption<T> Create(ApiResultBuildOption option,Func<HttpResponseMessage, T> deserializeObjectCallBack = null)
        {
            ApiResultBuildOption<T> o = new ApiResultBuildOption<T>();
            o.SetModelStatus = option.SetModelStatus;
            o.JsonSerializerSettings = option.JsonSerializerSettings;
            o.DeserializeObjectCallBack = deserializeObjectCallBack;
            return o;
        }

        public override object Clone()
        {
            ApiResultBuildOption<T> option = new ApiResultBuildOption<T>();
            option.JsonSerializerSettings = this.JsonSerializerSettings;
            option.SetModelStatus = this.SetModelStatus;
            option.DeserializeObjectCallBack = this.DeserializeObjectCallBack;
            return option;
        }
    }
}
