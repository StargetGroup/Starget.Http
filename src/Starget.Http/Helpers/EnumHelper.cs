using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace Starget.Http.Helpers
{
    public static class EnumHelper
    {
        public static string Serialize(object value)
         {
            if(value == null)
            {
                return null;
            }
            if (!(value is Enum))
            {
                return value.ToString();
            }

            var type = value.GetType();

            object underlyingValue = Convert.ChangeType(value, Enum.GetUnderlyingType(type));

            return underlyingValue.ToString();

        }

        public static TEnum Deserialize<TEnum>(string value) where TEnum : struct
        {
            TEnum parsed;
            if (Enum.TryParse<TEnum>(value, out parsed))
                return parsed;

            var found = typeof(TEnum).GetMembers()
                .Select(x => new
                {
                    Member = x,
                    Attribute = x.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault()
                })
                .FirstOrDefault(x => x.Attribute?.Value == value);
            if (found != null)
                return (TEnum)Enum.Parse(typeof(TEnum), found.Member.Name);
            return default(TEnum);
        }
    }
}
