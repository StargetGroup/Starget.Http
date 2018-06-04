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
        public async Task<FileResponse> DownloadFileByModelAsync(object model,string url = null,ApiRequestBuildOption requestOption = null)
        {
            var reqOption = this.BuildRequestOption(requestOption);
            if(reqOption.DefaultLocationType == ApiSerializeLocationType.Auto)
            {
                reqOption.DefaultLocationType = ApiSerializeLocationType.FromQuery;
            }
            var request = ApiRequest.FromModel(model,url, reqOption);
            return await DownloadFileAsync(request);
        }

        public async Task<ApiResponse<T>> GetByModelAsync<T>(object model, string url = null, ApiRequestBuildOption requestOption = null, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            var reqOption = this.BuildRequestOption(requestOption);
            if (reqOption.DefaultLocationType == ApiSerializeLocationType.Auto)
            {
                reqOption.DefaultLocationType = ApiSerializeLocationType.FromQuery;
            }
            var resOption = this.BuildResultOption(resultOption);
            var request = ApiRequest.FromModel(model, url, reqOption);
            var response = await this.GetAsync<T>(request, resOption);
            response.SetModelStatus();
            return response;
        }

        public async Task<ApiResponse<T>> PostByModelAsync<T>(object model, string url = null, ApiRequestBuildOption requestOption = null, ApiResultBuildOption<T> resultOption = null) where T : class, new()
        {
            var reqOption = this.BuildRequestOption(requestOption);
            if (reqOption.DefaultLocationType == ApiSerializeLocationType.Auto)
            {
                reqOption.DefaultLocationType = ApiSerializeLocationType.FromForm;
            }
            var resOption = this.BuildResultOption(resultOption);
            var request = ApiRequest.FromModel(model, url, reqOption);
            var response = await this.PostAsync<T>(request, resOption);
            response.SetModelStatus();
            return response;
        }

        protected ApiRequestBuildOption BuildRequestOption(ApiRequestBuildOption requestOption)
        {
            var option = requestOption?.Clone() as ApiRequestBuildOption;
            if(option == null)
            {
                option = this.DefaultRquestBuildOption?.Clone() as ApiRequestBuildOption;
            }
            else if(this.DefaultRquestBuildOption != null)
            {
                if(option.DefaultLocationType == ApiSerializeLocationType.NotSet)
                {
                    option.DefaultLocationType = this.DefaultRquestBuildOption.DefaultLocationType;
                }
                if (option.DefaultTextCaseType == ApiSerializeTextCaseType.NotSet)
                {
                    option.DefaultTextCaseType = this.DefaultRquestBuildOption.DefaultTextCaseType;
                }
                if (option.DefaultDownloadFileMode == DownloadFileMode.NotSet)
                {
                    option.DefaultDownloadFileMode = this.DefaultRquestBuildOption.DefaultDownloadFileMode;
                }
                if (option.DefaultEnumSerializeType == ApiEnumSerializeType.NotSet)
                {
                    option.DefaultEnumSerializeType = this.DefaultRquestBuildOption.DefaultEnumSerializeType;
                }
            }

            return option;            
        }

        protected ApiResultBuildOption<T> BuildResultOption<T>(ApiResultBuildOption<T> resultOption) where T : class, new()
        {
            var option = resultOption?.Clone() as ApiResultBuildOption<T>;
            if (option == null)
            {
                option = ApiResultBuildOption<T>.Create(this.DefaultResultBuildOption);
            }
            else if (this.DefaultRquestBuildOption != null)
            {

            }

            return option;
        }
    }
}
