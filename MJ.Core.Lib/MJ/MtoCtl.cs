using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatthewJesse
{
    public class MtoCtl<T, TResult> : IMJValidate<T, TResult>, IMJResult<T, TResult>
    {
        public MtoCtl() {;}
        public MtoCtl(Control ctl, Func<T, TResult> m) { obj = ctl; func = m; }
        public Control obj { get; set; }
        public Func<T, TResult> func { get; set; }

        public Func<T, bool> Validate { get; set; }
        public Func<T, bool> Ignore { get; set; }
        public Action<Control, T> vldsuccess { get; set; }
        public Action<Control, T> vldfail { get; set; }

        public IMJResult<T, TResult> ToValidate(Func<T, bool> v) { Validate = v; return this; }
        public IMJValidate<T, TResult> ToIgnore(Func<T, bool> v) { Ignore = v; return this; }
        public IMJResult<T, TResult> ToSuccess(Action<Control, T> success) { vldsuccess = success; return this; }
        public IMJResult<T, TResult> ToFail(Action<Control, T> fail) { vldfail = fail; return this; }

        public string MemberName { get; set; }
    }
}
