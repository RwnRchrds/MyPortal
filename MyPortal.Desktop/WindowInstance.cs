using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Desktop.ViewModels;

namespace MyPortal.Desktop
{
    public class WindowInstance
    {
        public static readonly AppViewModel _value = new AppViewModel();

        public static AppViewModel Value
        {
            get { return _value; }
        }
    }
}
