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
        public Func<object, HttpContent> SerializeObjectCallBack { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public object Clone()
        {
            ApiRequestBuildOption option = new ApiRequestBuildOption();
            option.DefaultLocationType = this.DefaultLocationType;
            option.DefaultTextCaseType = this.DefaultTextCaseType;
            option.SerializeObjectCallBack = this.SerializeObjectCallBack;
            option.JsonSerializerSettings = this.JsonSerializerSettings;
            return option;
        }
    }
}
