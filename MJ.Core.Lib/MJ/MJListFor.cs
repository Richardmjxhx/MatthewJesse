using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatthewJesse
{
    public static partial class _MJ_Helper
    {

        public static IMJValidate<T, string> ListFor<T>(this ListView ctl, Expression<Action<ListView, T>> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoLstCtl<T, string>>();
            fv.obj = ctl;
            fv.act = m.Compile();

            if (lstforDict.ContainsKey(type))
                lstforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                lstforDict.Add(type, list);
            }
            return fv;
        }
    }
}
