using Hungsum.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hungsum.Framework.Utilities
{
    public class HsDevice
    {
        public static T OnPlatform<T>(T iOS, T android)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return iOS;
                case Device.Android:
                    return android;
                default:
                    throw new HsException($"未定义平台【{Device.RuntimePlatform}】的返回值");
            }
        }
    }
}
