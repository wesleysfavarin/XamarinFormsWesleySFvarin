using System;
using UIKit;
using Foundation;
using Marvel.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(OpenWebService))]
namespace Marvel.iOS
{
	public class OpenWebService : IOpenWebService
	{
		public OpenWebService()
		{
		}

		public void OpenUrl(string url)
		{
			UIApplication.SharedApplication.OpenUrl(new NSUrl(url));
		}
	}
}