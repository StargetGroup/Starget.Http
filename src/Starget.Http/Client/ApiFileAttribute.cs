using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiFileAttribute :Attribute
    {
        public string Name { get; set; }

        public string FileName { get; set; }

        public string FileNameProperty { get; set; }

        public ApiFileAttribute(string fileNameProperty, string name = null,string fileName = null)
        {
            this.Name = name;
            this.FileName = fileName;
            this.FileNameProperty = fileNameProperty;
        }
    }
}
