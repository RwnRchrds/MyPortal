using System;
using System.Data;

namespace MyPortal.Database.Models.Connection;

public class DbUser
{
    public DbUser(Guid userId, IDbTransaction transaction, bool auditEnabled = true)
    {
        UserId = userId;
        Transaction = transaction;
        AuditEnabled = auditEnabled;
    }

    public Guid UserId { get; }
    public IDbTransaction Transaction { get; }
    public bool AuditEnabled { get; }
}