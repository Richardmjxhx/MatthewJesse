using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatthewJesse
{
    public class MtoLstCtl<T, TResult>:MtoCtl<T,TResult>
    {
        public MtoLstCtl() { ;}
        public MtoLstCtl(Control ctl, Action<ListView, T> m) { obj = ctl; act = m; }
        public Action<ListView, T> act { get; set; }
    }
}
