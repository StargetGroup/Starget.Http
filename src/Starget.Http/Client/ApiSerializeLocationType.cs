using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public enum ApiSerializeLocationType
    {
        NotSet = -1,
        Auto = 0,
        FromQuery =1,
        FromHeader=2,
        FromForm =3,
        Ignore = 4,
    }
}
