using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public partial class ApiClient : HttpClient
    {
        public ApiClient()
        {

        }

        public string Credentials { get; protected set; }

        public ApiRequestBuildOption DefaultRquestBuildOption { get; set; } = new ApiRequestBuildOption();
        public ApiResultBuildOption DefaultResultBuildOption { get; set; } = new ApiResultBuildOption();

        public ApiClient(string baseAddress)
        {
            this.BaseAddress = new Uri(baseAddress);
        }

        public ApiClient(string baseAddress,HttpClientHandler handler)
            :base(handler)
        {
            this.BaseAddress = new Uri(baseAddress);
        }

        public bool SetBeararCredentials(string token)
        {
            if(string.IsNullOrEmpty(token))
            {
                return false;
            }
            this.Credentials = "Bearar " + token;
            this.DefaultRequestHeaders.Add("Authorization", "Bearar " + token);
            return true;
        }

        public bool SetBasicCredentials(string userName,string password)
        {
            if(string.IsNullOrEmpty(userName) || password == null)
            {
                return false;
            }
            var byteArray = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", userName, password));
            var str = Convert.ToBase64String(byteArray);
            this.Credentials = "Basic "+str;
            this.DefaultRequestHeaders.Add("Authorization", "Basic " + str);
            return true;
        }

        public async Task<ApiResponse> GetAsync(ApiRequest request)
        {
            var response = new ApiResponse();
            try
            {
                var message = await this.GetAsync(request.GetUrl());
                await response.DeserializeMessageAsync(message);
            }
            catch { }

            return response;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(ApiRequest request, ApiResultBuildOption<T> resultOption = null) where T : class,new()
        {
            var response = new ApiResponse<T>();
            try
            {
                var message = await this.GetAsync(request.GetUrl());
                if(resultOption == null && this.DefaultResultBuildOption != null)
                {
                    resultOption = ApiResultBuildOption<T>.Create(this.DefaultResultBuildOption);
                }
                await response.DeserializeMessageAsync(message, resultOption);
            }
            catch { response.Data = null; }

            return response;
        }

        public async Task<ApiResponse> PostAsync(ApiRequest request)
        {
            var requestMessage = request.GetRequestMessage();
            requestMessage.Method = HttpMethod.Post;
            var response = new ApiResponse();
            try
            {
                var message = await this.SendAsync(requestMessage);
                
                await response.DeserializeMessageAsync(message);
            }
            catch { }

            return response;
        }

        public async Task<ApiResponse<T>> PostAsync<T>(ApiRequest request, ApiResultBuildOption<T> resultOption = null) where T : class,new()
        {
            var requestMessage = request.GetRequestMessage();
            requestMessage.Method = HttpMethod.Post;
            var response = new ApiResponse<T>();
            try
            {
                var message = await this.SendAsync(requestMessage);
                if (resultOption == null && this.DefaultResultBuildOption != null)
                {
                    resultOption = ApiResultBuildOption<T>.Create(this.DefaultResultBuildOption);
                }
                await response.DeserializeMessageAsync(message, resultOption);
            }
            catch { response.Data = null; }

            return response;
        }

        public async Task<FileResponse> DownloadFileAsync(ApiRequest request)
        {
            var response = new FileResponse();

            try
            {
                var message = await this.GetAsync(request.GetUrl());
                await response.DeserializeMessageAsync(message);
            }
            catch { }

            return response;
        }
    }
}
