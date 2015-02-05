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

        public static IMJValidate<T, Image> ImageBindTo<T>(this Control ctl, Expression<Func<T, Image>> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, Image>>();
            fv.obj = ctl;
            fv.func = m.Compile();

            var expression = GetMemberInfo(m);

            fv.MemberName = expression == null ? null : expression.Member.Name;

            if (picforDict.ContainsKey(type))
                picforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                picforDict.Add(type, list);
            }

            return fv;
        }
        public static IMJValidate<T, Image> ImageFor<T>(this Control ctl, Func<T, Image> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, Image>>();
            fv.obj = ctl;
            fv.func = m;

            fv.MemberName = null;

            if (picforDict.ContainsKey(type))
                picforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                picforDict.Add(type, list);
            }

            return fv;
        }
    }
}
