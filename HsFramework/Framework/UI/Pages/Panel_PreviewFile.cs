using System;
using Hungsum.Framework.UI.Views;
using Xamarin.Forms;

namespace Hungsum.Framework.UI.Pages
{
	public class Panel_PreviewFile : UcContentPage
    {
		private UcWebView _view;

        public Panel_PreviewFile()
        {
			_view = new UcWebView();

			Content = _view;
        }


		public WebViewSource Source
		{
			set
			{
				_view.Source = value;
			}
			
		}

    }
}

