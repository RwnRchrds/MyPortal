using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Interfaces
{
    public interface ICreationAudit
    {
        Guid CreatedById { get; set; }
        DateTime CreatedDate { get; set; }

        ApplicationUser CreatedBy { get; set; }
    }
}
