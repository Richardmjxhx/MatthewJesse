using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MatthewJesse.MVC
{
    public static class _MJ_MVC_Helper
    {
        private static List<_MJ_Method> list = new List<_MJ_Method>();

        //public static void _MJ_(this Form form, string controller, string method, params object[] values)
        //{
        //    var asm = Assembly.GetCallingAssembly();

        //    var clsname = string.IsNullOrEmpty(controller) ? "MAINCONTROLLER" : controller.ToUpper();

        //    var types = from t in asm.GetTypes()
        //                where t.IsClass &&
        //                t.IsPublic &&
        //                t.Name.ToUpper().Equals(clsname)
        //                select t;

        //    if (types.Count() > 0)
        //    {
        //        object classInstance = Activator.CreateInstance(types.ElementAt(0), null);

        //        List<Type> paraTypes = new List<Type>();

        //        foreach(var p in values)
        //        {
        //            paraTypes.Add(p.GetType());
        //        }
        //        var methodInstance = types.ElementAt(0).GetMethod(method, paraTypes.ToArray());

        //        if (methodInstance != null && methodInstance.ReturnType == typeof(object[]))
        //        {
        //            object[] retvalues = (object[])methodInstance.Invoke(classInstance, values);

        //            if (retvalues == null)
        //                return;

        //            foreach(var o in retvalues)
        //            {
        //                var genericMethod = typeof(_MJ_Helper).GetMethod("Load")
        //                                        .MakeGenericMethod(new Type[] { o.GetType() });

        //                genericMethod.Invoke(null, new object[] { form, o });
        //            }
        //        }
        //        else
        //            throw new Exception(string.Format("[MJ] cannot find proper Method [{0}] from [{1}]", method, clsname));


        //    }
        //    else
        //        throw new Exception(string.Format("[MJ] cannot find controller [{0}]",clsname));
            
        //}

        private static bool CollectMethods(Assembly asm)
        {
            list.Clear();

            var classtypes = from t in asm.GetTypes()
                        where t.IsClass &&
                        t.IsPublic &&
                        t.Name.ToUpper().Contains("CONTROLLER")
                        select t;

            if(classtypes == null)
                return false;

            foreach(var cls in classtypes)
            {
                var mtdInfos = cls.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                if (mtdInfos == null)
                    continue;
                foreach(var m in mtdInfos)
                {
                    if (m.ReturnType != typeof(object[]))
                        continue;

                    var ps = m.GetParameters();

                    List<Type> paraTypes = new List<Type>();

                    var count = ps != null ? ps.Count() : 0;

                    if (count > 0)
                        foreach (var p in ps)
                        {
                            paraTypes.Add(p.ParameterType);
                        }
                    else
                        paraTypes = null;

                    var mtd = new _MJ_Method(m.Name,
                        cls.Name,
                        count,
                        paraTypes,
                        m
                        );
                    list.Add(mtd);
                }
            }
            if (list.Count() > 0)
                return true;
            else
                return false;
        }

        public static void _MJ_(this Form form, string controller, string method, params object[] values)
        {
            var asm = Assembly.GetCallingAssembly();

            if(list.Count() == 0)
                if (!CollectMethods(asm))
                    throw new Exception(string.Format(
                        "[MJ] cannot find controllers or their methods in {0},so cann't use MJ MVC", asm.FullName));

            List<Type> paraTypes = new List<Type>();

            var count = values != null ? values.Count() : 0;

            if(count > 0)
                foreach (var p in values)
                {
                    paraTypes.Add(p.GetType());
                }
            else
                paraTypes = null;

            var mtds = from m in list
                       where m.Name == method &&
                       m.ParamsCount == count &&
                       m.ParamsType.SequenceEqual(paraTypes)
                       select m;
            
            if(mtds == null || mtds.Count() == 0)
                throw new Exception(string.Format("[MJ] cannot find proper Method [{0}] from [{1}]", method, asm.FullName));

            var tecount = mtds.Count();

            _MJ_Method invoker = null;

            if (mtds.Count() == 1)
                invoker = mtds.ElementAt(0);
            else
            {
                var exect = from m in mtds
                            where m.ClassName.ToUpper().Equals(controller.ToUpper())
                            select m;
                if (exect != null && exect.Count() > 0)
                    invoker = exect.ElementAt(0);
                else
                {
                    var like = from m in mtds
                               where m.ClassName.ToUpper().Contains(controller.ToUpper())
                               select m;

                    if (like != null && like.Count() > 0)
                        invoker = like.ElementAt(0);
                    else
                        throw new Exception(string.Format("[MJ] cannot find proper Method [{0}] from [{1}]", method, asm.FullName));
                    
                }
            }

            object classInstance = Activator.CreateInstance(invoker.Method.DeclaringType, null);

            List<Type> valuesTypes = new List<Type>();

            foreach (var p in values)
            {
                valuesTypes.Add(p.GetType());
            }

            object[] retvalues = (object[])invoker.Method.Invoke(classInstance, values);

            if (retvalues == null)
                return;

            foreach (var o in retvalues)
            {
                var genericMethod = typeof(_MJ_Helper).GetMethod("Load")
                                        .MakeGenericMethod(new Type[] { o.GetType() });

                genericMethod.Invoke(null, new object[] { form, o });
            }

        }


    }
}
