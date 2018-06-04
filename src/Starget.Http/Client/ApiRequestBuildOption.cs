using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Starget.Http.Client
{
    public class ApiRequestBuildOption : ICloneable
    {
        public ApiSerializeLocationType DefaultLocationType { get; set; } = ApiSerializeLocationType.NotSet;
        public ApiSerializeTextCaseType DefaultTextCaseType { get; set; } = ApiSerializeTextCaseType.None;
        public DownloadFileMode DefaultDownloadFileMode { get; set; } = DownloadFileMode.NotSet;
        public ApiEnumSerializeType DefaultEnumSerializeType { get; set; } = ApiEnumSerializeType.NotSet;

        public Func<object, HttpContent> SerializeObjectCallBack { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public object Clone()
        {
            ApiRequestBuildOption option = new ApiRequestBuildOption();
            option.DefaultLocationType = this.DefaultLocationType;
            option.DefaultTextCaseType = this.DefaultTextCaseType;
            option.DefaultEnumSerializeType = this.DefaultEnumSerializeType;
            option.DefaultDownloadFileMode = this.DefaultDownloadFileMode;
            option.SerializeObjectCallBack = this.SerializeObjectCallBack;
            option.JsonSerializerSettings = this.JsonSerializerSettings;
            return option;
        }
    }
}
