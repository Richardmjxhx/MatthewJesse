using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatthewJesse
{
    public interface IMJValidate<T,TResult>
    {
        IMJResult<T, TResult> ToValidate(Func<T, bool> v);
        IMJValidate<T, TResult> ToIgnore(Func<T, bool> v);
    }
}
