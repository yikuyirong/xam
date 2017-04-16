using Hungsum.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Utilities
{
    public class HsDependencyService<T> where T:class
    {
        public static T Instance()
        {
            T t = DependencyService.Get<T>();

            if (t == null)
            {
                throw new HsException("无法实例化DependencyService对象。");
            }

            return t;
        }
    }
}
