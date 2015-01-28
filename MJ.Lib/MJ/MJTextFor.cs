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

        public static IMJValidate<T,string> TextBindTo<T>(this Control ctl, Expression<Func<T, string>> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, string>>();
            fv.obj = ctl;
            fv.func = m.Compile();

            var expression = GetMemberInfo(m);

            fv.MemberName = expression == null?null:expression.Member.Name;

            if (txtforDict.ContainsKey(type))
                txtforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                txtforDict.Add(type, list);
            }

            return fv;
        }
        public static IMJValidate<T, string> TextFor<T>(this Control ctl, Func<T, string> m)
        {
            var type = typeof(T);
            var fv = Activator.CreateInstance<MtoCtl<T, string>>();
            fv.obj = ctl;
            fv.func = m;

            fv.MemberName = null;

            if (txtforDict.ContainsKey(type))
                txtforDict[type].Add(fv);
            else
            {
                List<dynamic> list = new List<dynamic>();
                list.Add(fv);
                txtforDict.Add(type, list);
            }

            return fv;
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                return null;

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            return memberExpr;
        }

    }
}
