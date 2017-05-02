using System;
using Hungsum.Framework.UI.Views;
using Hungsum.iOS.CustomRenderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UcWebView), typeof(UcWebViewRenderer))]
namespace Hungsum.iOS.CustomRenderer
{
    public class UcWebViewRenderer : WebViewRenderer
    {
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			UIWebView view = (UIWebView)NativeView;

			view.BackgroundColor = UIColor.Blue;

			view.ScalesPageToFit = true;

			view.ScrollView.ScrollEnabled = true;


		}
	}
}

