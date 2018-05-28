using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starget.Http.Client
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiUrlAttribute : Attribute
    {
    }
}
