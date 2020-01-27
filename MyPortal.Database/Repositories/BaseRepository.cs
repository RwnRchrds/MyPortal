using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class BaseRepository : IDisposable
    {
        protected readonly IDbConnection Connection;
        protected readonly ApplicationDbContext Context;

        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(connection.ConnectionString);

            Context =  new ApplicationDbContext(optionsBuilder.Options);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
            Connection.Dispose();
        }
    }
}
