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

        public static IMJValidate<T, bool> EnabledBindTo<T>(this Control ctl, Expression<Func<T, bool>> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, bool>>();
            fv.obj = ctl;
            fv.func = m.Compile();

            var expression = GetMemberInfo(m);

            fv.MemberName = expression == null ? null : expression.Member.Name;

            if (enbforDict.ContainsKey(type))
                enbforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                enbforDict.Add(type, list);
            }

            return fv;
        }
        public static IMJValidate<T, bool> EnabledFor<T>(this Control ctl, Func<T, bool> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, bool>>();
            fv.obj = ctl;
            fv.func = m;

            fv.MemberName = null ;

            if (enbforDict.ContainsKey(type))
                enbforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                enbforDict.Add(type, list);
            }

            return fv;
        }
    }
}
