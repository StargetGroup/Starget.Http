using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    public class FileContent
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public byte[] Bytes { get; set; }
    }
}
