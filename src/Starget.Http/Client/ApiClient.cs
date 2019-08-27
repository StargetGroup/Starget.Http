using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ApiRequestBuildOption DefaultRequestBuildOption { get; set; } = new ApiRequestBuildOption();
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
                var task = this.GetAsync(request.GetUrl());
                task.Wait();
                var message = task.Result;
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
                var task = this.GetAsync(request.GetUrl());
                task.Wait();
                var message = task.Result;
                if (resultOption == null && this.DefaultResultBuildOption != null)
                {
                    resultOption = ApiResultBuildOption<T>.Create(this.DefaultResultBuildOption);
                }
                await response.DeserializeMessageAsync(message, resultOption);
            }
            catch { response.Data = null; }

            return response;
        }

        public async Task<ApiResponse> GetByUrlAsync(string url)
        {
            var response = new ApiResponse();
            try
            {
                var task = this.GetAsync(url);
                task.Wait();
                var message = task.Result;
                await response.DeserializeMessageAsync(message);
            }
            catch { }

            return response;
        }

        public async Task<ApiResponse<T>> GetByUrlAsync<T>(string url, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            var response = new ApiResponse<T>();
            try
            {
                var task = this.GetAsync(url);
                task.Wait();
                var message = task.Result;
                if (resultOption == null && this.DefaultResultBuildOption != null)
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
                var task = this.SendAsync(requestMessage);
                task.Wait();
                var message = task.Result;
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
                var task = this.SendAsync(requestMessage);
                task.Wait();
                var message = task.Result;
                if (resultOption == null && this.DefaultResultBuildOption != null)
                {
                    resultOption = ApiResultBuildOption<T>.Create(this.DefaultResultBuildOption);
                }
                await response.DeserializeMessageAsync(message, resultOption);
            }
            catch { response.Data = null; }

            return response;
        }

        public async Task<ApiResponse> PostByUrlAsync(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(url);
            requestMessage.Method = HttpMethod.Post;
            var response = new ApiResponse();
            try
            {
                var task = this.SendAsync(requestMessage);
                task.Wait();
                var message = task.Result;
                await response.DeserializeMessageAsync(message);
            }
            catch { }

            return response;
        }

        public async Task<ApiResponse<T>> PostByUrlAsync<T>(string url, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri(url);
            requestMessage.Method = HttpMethod.Post;
            var response = new ApiResponse<T>();
            try
            {
                var task = this.SendAsync(requestMessage);
                task.Wait();
                var message = task.Result;
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
                if(request.DownloadFileMode == DownloadFileMode.GetStream)
                {
                    var task = this.GetStreamAsync(request.GetUrl());
                    task.Wait();
                    var stream = task.Result;
                    response = await FileResponse.FromStreamAsync(stream);
                }
                else if(request.DownloadFileMode == DownloadFileMode.GetByteArray)
                {
                    var task = this.GetByteArrayAsync(request.GetUrl());
                    task.Wait();
                    var bytes = task.Result;
                    response = await FileResponse.FromBytesAsync(bytes);
                }
                else if(request.DownloadFileMode == DownloadFileMode.Get)
                {
                    var task = this.GetAsync(request.GetUrl());
                    task.Wait();
                    var message = task.Result;
                    await response.DeserializeMessageAsync(message);
                }
                else if (request.DownloadFileMode == DownloadFileMode.Send)
                {
                    var requestMessage = request.GetRequestMessage();
                    var task = this.SendAsync(requestMessage);
                    task.Wait();
                    var message = task.Result;
                    await response.DeserializeMessageAsync(message);
                    //Stream stream = await (await this.SendAsync(requestMessage).ConfigureAwait(false)).Content.ReadAsStreamAsync();
                    //response = await FileResponse.FromStreamAsync(stream);
                }
                else if (request.DownloadFileMode == DownloadFileMode.String)
                {
                    var task = this.GetStringAsync(request.GetUrl());
                    task.Wait();
                    var str = task.Result;
                    var bytes = Encoding.UTF8.GetBytes(str);
                    response = await FileResponse.FromBytesAsync(bytes);
                }
            }
            catch { }

            return response;
        }

        public async Task<FileResponse> DownloadFileByUrlAsync(string url, DownloadFileMode mode = DownloadFileMode.GetStream)
        {
            var response = new FileResponse();
            var request = new ApiRequest(url);
            request.DownloadFileMode = mode;
            return await DownloadFileAsync(request);
        }
    }
}
