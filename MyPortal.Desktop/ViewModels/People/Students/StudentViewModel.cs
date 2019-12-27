using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Desktop.Base.Interfaces;
using MyPortal.Desktop.Base.ViewModels;

namespace MyPortal.Desktop.ViewModels.People.Students
{
    public class StudentViewModel : BaseViewModel, IAppWindow
    {
        public string Header { get; } = "Students";
        public async Task OnTabSelected()
        {
            
        }

        public async Task OnTabUnselected()
        {
            
        }

        public bool ShowSaveIcon { get; }
        public string SaveIcon { get; }
        public string SaveIconTooltip { get; }
        public bool BeforeTabClosed()
        {
            return true;
        }

        public async Task OnTabClosed()
        {
            
        }

        public async Task OnTabOpened()
        {
            
        }
    }
}
