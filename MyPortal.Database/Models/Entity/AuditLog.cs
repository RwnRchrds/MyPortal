using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Database.Models.Entity;

[Table("AuditLogs")]
public class AuditLog : BaseTypes.Entity
{
    [Column(Order = 2)] public string TableName { get; set; }

    [Column(Order = 3)] public Guid EntityId { get; set; }

    [Column(Order = 4)] public Guid UserId { get; set; }

    [Column(Order = 5)] public Guid AuditActionId { get; set; }

    [Column(Order = 6)] public DateTime CreatedDate { get; set; }

    [Column(Order = 7)] public string OldValue { get; set; }

    public virtual User User { get; set; }
    public virtual AuditAction Action { get; set; }
}