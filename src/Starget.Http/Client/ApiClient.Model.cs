using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public partial class ApiClient
    {
        public async Task<FileResponse> DownloadFileByModelAsync<M,T>(M model,string url = null) where M : class where T:class,new()
        {
            var request = ApiRequest.FromModel(model,RequestType.Get,url);
            return await DownloadFileAsync(request);
        }

        public async Task<ApiResponse<T>> GetByModelAsync<M, T>(M model, string url = null, Func<object, HttpContent> serializeObjectCallBack = null, Func<HttpResponseMessage, T> deserializeObjectCallBack = null) where M : class where T : class, new()
        {
            var request = ApiRequest.FromModel(model,RequestType.Get, url);
            request.SerializeObjectCallBack = serializeObjectCallBack;
            var response = await this.GetAsync<T>(request, deserializeObjectCallBack);
            response.SetModelStatus();
            return response;
        }

        public async Task<ApiResponse<T>> PostByModelAsync<M, T>(M model, string url = null, Func<object, HttpContent> serializeObjectCallBack = null, Func<HttpResponseMessage, T> deserializeObjectCallBack = null) where M : class where T : class, new()
        {
            var request = ApiRequest.FromModel(model,RequestType.Post, url);
            request.SerializeObjectCallBack = serializeObjectCallBack;
            var response = await this.PostAsync<T>(request, deserializeObjectCallBack);
            response.SetModelStatus();
            return response;
        }

    }
}
