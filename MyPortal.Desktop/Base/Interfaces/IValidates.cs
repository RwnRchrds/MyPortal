using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Desktop.Base.Interfaces
{
    public interface IValidates
    {
        String[] ValidationErrors { get; }
        String[] WarningMessages { get; }
    }
}
