using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyPortal.Database.Models;

namespace MyPortal.Database.Helpers
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string ConnectionString = @"";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
