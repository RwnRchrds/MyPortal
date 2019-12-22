using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Desktop.Base.Interfaces
{
    public interface IBaseVM : INotifyPropertyChanged
    {
        Boolean HasChanges { get; }
    }
}
