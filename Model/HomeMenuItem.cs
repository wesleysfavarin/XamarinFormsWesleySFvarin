using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Model
{
    public enum MenuType
    {
        Principal,
        About,
       
    }
    public class HomeMenuItem : BaseModel
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Principal;
        }
        public string Icon { get; set; }
        public MenuType MenuType { get; set; }
    }
}
