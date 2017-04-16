using System;
using System.Xml.Linq;
using Hungsum.Framework.Events;

namespace Hungsum.Framework.UI.Pages
{
    public interface IUcLoginPage : IUcPage
    {
        event EventHandler<HsEventArgs<XElement>> LoginSuccess;
    }
}