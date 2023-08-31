using System;
using System.Data.Common;

namespace MyPortal.Database.Models.Connection;

public class DbUserWithContext : DbUser
{
    public DbUserWithContext(Guid userId, DbTransaction transaction, ApplicationDbContext context) : base(userId,
        transaction)
    {
        Context = context;
    }

    public ApplicationDbContext Context { get; }
}