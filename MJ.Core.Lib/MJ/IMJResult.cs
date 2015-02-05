using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatthewJesse
{
    public interface IMJResult<T, TResult>
    {
        IMJResult<T, TResult> ToSuccess(Action<Control, T> success);
        IMJResult<T, TResult> ToFail(Action<Control, T> fail);
    }
}
