using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Desktop.Base.Interfaces
{
    public interface IAppWindow
    {
        String Header { get; }
        Task OnTabSelected();
        Task OnTabUnselected();
        Boolean ShowSaveIcon { get; }

        String SaveIcon { get; }
        String SaveIconTooltip { get; }
        Boolean BeforeTabClosed();
        Task OnTabClosed();
        Task OnTabOpened();
    }
}
