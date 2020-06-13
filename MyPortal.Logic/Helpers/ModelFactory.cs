using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Helpers
{
    public class ModelFactory
    {
        public static string ConnectionString { get; set; }

        public static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ConnectionString).Options;

            return new ApplicationDbContext(options);
        }
    }
}
