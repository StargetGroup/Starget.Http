using System;
using System.Collections.Generic;
using System.Text;

namespace Starget.Http.Client
{
    public class ApiEnumAttribute : Attribute
    {
        public ApiEnumAttribute()
        {

        }

        public ApiEnumAttribute(ApiEnumSerializeType serializeType)
        {
            this.SerializeType = serializeType;
        }
        public ApiEnumSerializeType SerializeType { get; set; } = ApiEnumSerializeType.Value;
    }
}
