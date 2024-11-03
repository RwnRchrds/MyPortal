using System;
using System.Data;

namespace MyPortal.Database.Models.Connection;

public class DbUser
{
    public DbUser(Guid userId, IDbTransaction transaction)
    {
        UserId = userId;
        Transaction = transaction;
    }

    public Guid UserId { get; }
    public IDbTransaction Transaction { get; }
}