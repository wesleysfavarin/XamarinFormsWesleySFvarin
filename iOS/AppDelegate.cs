using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Microsoft.Azure.Mobile;
using UIKit;

namespace Marvel.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			ImageCircleRenderer.Init();


			LoadApplication(new App());

			MobileCenter.Configure("0c53e05e-5f03-4f4c-a53d-f029401dafa0");

			return base.FinishedLaunching(app, options);
		}
	}
}
