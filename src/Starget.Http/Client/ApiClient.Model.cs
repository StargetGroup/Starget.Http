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
        public async Task<FileResponse> DownloadFileByModelAsync<T>(object model,string url = null,ApiRequestBuildOption requestOption = null) where T:class,new()
        {

            var request = ApiRequest.FromModel(model,url, requestOption??this.DefaultRquestBuildOption);
            return await DownloadFileAsync(request);
        }

        public async Task<ApiResponse<T>> GetByModelAsync<T>(object model, string url = null, ApiRequestBuildOption requestOption = null, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            var request = ApiRequest.FromModel(model, url, requestOption??this.DefaultRquestBuildOption);
            var response = await this.GetAsync<T>(request, resultOption);
            response.SetModelStatus();
            return response;
        }

        public async Task<ApiResponse<T>> PostByModelAsync<T>(object model, string url = null, ApiRequestBuildOption requestOption = null, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            var request = ApiRequest.FromModel(model, url, requestOption??this.DefaultRquestBuildOption);
            var response = await this.PostAsync<T>(request, resultOption);
            response.SetModelStatus();
            return response;
        }

    }
}
