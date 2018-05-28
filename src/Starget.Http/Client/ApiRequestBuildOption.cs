using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Starget.Http.Client
{
    public class ApiRequestBuildOption
    {
        public ApiSerializeType DefaultSeralizeType { get; set; } = ApiSerializeType.None;
        public ApiSerializeTextCaseType DefaultTextCaseType { get; set; } = ApiSerializeTextCaseType.None;
        public Func<object, HttpContent> SerializeObjectCallBack { get; set; }
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

    }
}
