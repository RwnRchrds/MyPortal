using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("Audits")]
public class Audit : BaseTypes.Entity
{
    [Column(Order = 2)]
    public string TableName { get; set; }
    
    [Column(Order = 3)]
    public Guid EntityId { get; set; }
    
    [Column(Order = 4)]
    public Guid AuditActionId { get; set; }
    
    [Column(Order = 5)]
    public string OldValue { get; set; }
    
    [Column(Order = 6)]
    public string NewValue { get; set; }

    public virtual AuditAction Action { get; set; }
}