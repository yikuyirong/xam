using System;

namespace Hungsum.Framework.UI.Pages
{
    public interface IUcMainPage : IUcPage
    {
        event EventHandler Logout;
    }
}