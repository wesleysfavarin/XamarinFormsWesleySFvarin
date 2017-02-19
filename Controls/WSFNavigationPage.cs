    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Marvel.Controls
{
   public class WSFNavigationPage : NavigationPage
    {
        public WSFNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public WSFNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#C62828");
            BarTextColor = Color.White;
        }
    }
}
