using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("AuditActions")]
public class AuditAction : BaseTypes.LookupItem
{
    public AuditAction()
    {
        AuditLogs = new HashSet<AuditLog>();
    }

    public virtual ICollection<AuditLog> AuditLogs { get; set; }
}