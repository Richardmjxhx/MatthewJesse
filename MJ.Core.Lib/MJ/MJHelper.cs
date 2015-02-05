using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MatthewJesse
{
    public static partial class _MJ_Helper
    {
        private static Dictionary<Type, List<dynamic>> txtforDict = new Dictionary<Type, List<dynamic>>();
        private static Dictionary<Type, List<dynamic>> picforDict = new Dictionary<Type, List<dynamic>>();
        private static Dictionary<Type, List<dynamic>> enbforDict = new Dictionary<Type, List<dynamic>>();
        private static Dictionary<Type, List<dynamic>> lstforDict = new Dictionary<Type, List<dynamic>>();


        public static void Load<T>(this Control ctl, T model)
        {
            var type = typeof(T);

            if (txtforDict.ContainsKey(type))
            {
                foreach(var f in txtforDict[type])
                {
                    var excute = f.Ignore == null?true:!f.Ignore(model);
                    if (excute)
                    {
                        f.obj.Text = model == null?"":f.func(model);
                        if (f.Validate != null && model != null)
                        {
                            var v = f.Validate(model) ? f.vldsuccess : f.vldfail;

                            if (v != null)
                                v(f.obj, model);
                        }
                    }
                }
            }

            if (enbforDict.ContainsKey(type))
            {
                foreach (var f in enbforDict[type])
                {
                    var excute = f.Ignore == null ? true : !f.Ignore(model);
                    if (excute)
                    {
                        f.obj.Enabled = model == null ? true : f.func(model);
                        if (f.Validate != null && model != null)
                        {
                            var v = f.Validate(model) ? f.vldsuccess : f.vldfail;

                            if (v != null)
                                v(f.obj, model);
                        }
                    }
                }
            }

            if (picforDict.ContainsKey(type))
            {
                foreach (var f in picforDict[type])
                {
                    var excute = f.Ignore == null ? true : !f.Ignore(model);
                    if (excute)
                    {
                        ((PictureBox)f.obj).Image = model == null ? null : f.func(model);
                        if (f.Validate != null && model != null)
                        {
                            var v = f.Validate(model) ? f.vldsuccess : f.vldfail;

                            if (v != null)
                                v(f.obj, model);
                        }
                    }
                }
            }

            if (lstforDict.ContainsKey(type))
            {
                foreach (var f in lstforDict[type])
                {
                    var excute = f.Ignore == null ? true : !f.Ignore(model);
                    if (excute)
                    {
                        f.act((ListView)f.obj, model);
                        if (f.Validate != null && model != null)
                        {
                            var v = f.Validate(model) ? f.vldsuccess : f.vldfail;

                            if (v != null)
                                v(f.obj, model);
                        }
                    }
                }
            }
        }
        public static void Get<T>(this T model) 
        {
            var type = typeof(T);
            if (txtforDict.ContainsKey(type))
            {
                foreach (var f in txtforDict[type])
                {
                    if (f.MemberName != null)
                    {
                        PropertyInfo prop = model.GetType().GetProperty(f.MemberName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(model, f.obj.Text, null);
                        } 
                    }
                }
            }

            if (picforDict.ContainsKey(type))
            {
                foreach (var f in picforDict[type])
                {
                    if (f.MemberName != null)
                    {
                        PropertyInfo prop = model.GetType().GetProperty(f.MemberName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(model, f.obj.Image, null);
                        }
                    }
                }
            }

            if (enbforDict.ContainsKey(type))
            {
                foreach (var f in enbforDict[type])
                {
                    if (f.MemberName != null)
                    {
                        PropertyInfo prop = model.GetType().GetProperty(f.MemberName, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite)
                        {
                            prop.SetValue(model, f.obj.Enabled, null);
                        }
                    }
                }
            }

        }

    }
}
