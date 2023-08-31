using System.Collections.Generic;

namespace MyPortal.Database.Models.Entity;

public class AuditAction : BaseTypes.LookupItem
{
    public AuditAction()
    {
        Audits = new HashSet<Audit>();
    }
    
    public virtual ICollection<Audit> Audits { get; set; }
}