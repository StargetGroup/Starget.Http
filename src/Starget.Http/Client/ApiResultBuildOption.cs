using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Starget.Http.Client
{
    public class ApiResultBuildOption
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public bool SetModelStatus { get; set; }
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
    }
}
