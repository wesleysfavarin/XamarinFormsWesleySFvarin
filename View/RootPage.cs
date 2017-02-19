using Marvel.Controls;
using Marvel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Marvel.View
{
    public class RootPage : MasterDetailPage
    {

        public static bool IsUWPDesktop { get; set; }
        Dictionary<MenuType, NavigationPage> Pages { get; set; }
        public RootPage()
        {

            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new ViewModelBase
            {
                Title = "Wesley S. Favarin",
                Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(MenuType.About);

            InvalidateMeasure();
        }



        public async Task NavigateAsync(MenuType id)
        {

            if (Detail != null)
            {
                if (IsUWPDesktop || Device.Idiom != TargetIdiom.Tablet)
                    IsPresented = false;

                if (Device.OS == TargetPlatform.Android)
                    await Task.Delay(300);
            }

            Page newPage;
            if (!Pages.ContainsKey(id))
            {

                switch (id)
                {
                    case MenuType.About:
                        Pages.Add(id, new WSFNavigationPage(new AboutPage()));
                        break;
                    case MenuType.Principal:
                        Pages.Add(id, new WSFNavigationPage(new PrincipalPage()));
                        break;
                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.OS == TargetPlatform.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }
    }
}
