using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface ISoftDelete
    {
        bool Deleted { get; set; }
    }
}
