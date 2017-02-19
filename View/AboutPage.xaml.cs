using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Marvel.View
{
    public partial class AboutPage : ContentPage
    {
        void OpenBrowser(string url)
        {
            CrossShare.Current.OpenBrowser(url, new Plugin.Share.Abstractions.BrowserOptions
            {
                ChromeShowTitle = true,
                ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
                UseSafariReaderMode = true,
                UseSafariWebViewController = true
            });
        }
        public AboutPage()
        {
            InitializeComponent();
            linkedin.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => OpenBrowser("https://www.linkedin.com/in/wesley-s-favarin-61249755/"))
            });
        }
    }
}
