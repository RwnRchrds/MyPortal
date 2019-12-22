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
        public string Header { get; }
        public Task OnTabSelected()
        {
            throw new NotImplementedException();
        }

        public Task OnTabUnselected()
        {
            throw new NotImplementedException();
        }

        public bool ShowSaveIcon { get; }
        public string SaveIcon { get; }
        public string SaveIconTooltip { get; }
        public bool BeforeTabClosed()
        {
            throw new NotImplementedException();
        }

        public Task OnTabClosed()
        {
            throw new NotImplementedException();
        }

        public Task OnTabOpened()
        {
            throw new NotImplementedException();
        }
    }
}
