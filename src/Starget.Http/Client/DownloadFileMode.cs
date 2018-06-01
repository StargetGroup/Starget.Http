using System;
using System.Collections.Generic;
using System.Text;

namespace Starget.Http.Client
{
    public enum DownloadFileMode
    {
        NotSet = -1,
        Get = 0,
        GetByteArray = 1,
        GetStream = 2,
        Send =3,
        String =4,
    }
}
