using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    public class DataConnectionFactory
    {
        private static ApplicationDbContext CreateContext()
        {
            CheckConnectionString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(Configuration.Instance.ConnectionString).Options;

            return new ApplicationDbContext(options);
        }

        internal static async Task<IUnitOfWork> CreateUnitOfWork()
        {
            var context = CreateContext();
            return await UnitOfWork.Create(context);
        }

        private static void CheckConnectionString()
        {
            if (string.IsNullOrWhiteSpace(Configuration.Instance.ConnectionString))
            {
                throw new ConnectionStringException("The connection string has not been set.");
            }
        }
    }
}
