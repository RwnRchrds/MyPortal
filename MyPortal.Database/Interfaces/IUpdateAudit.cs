using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Interfaces
{
    public interface IUpdateAudit
    {
        Guid UpdatedById { get; set; }
        DateTime UpdatedDate { get; set; }

        ApplicationUser UpdatedBy { get; set; }
    }
}
