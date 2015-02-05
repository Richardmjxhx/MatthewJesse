using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatthewJesse.MVC
{
    class _MJ_Method
    {
        public string Name  { get; set; }
        public string ClassName { get; set; }
        public int ParamsCount { get; set; }
        public List<Type> ParamsType { get; set; }
        public MethodInfo Method { get; set; }

        public _MJ_Method(string name, string clsname, int paramscount, List<Type> paramstype, MethodInfo method)
        {
            Name = name; ClassName = clsname; ParamsCount = paramscount; ParamsType = paramstype; Method = method;
        }

    }
}
