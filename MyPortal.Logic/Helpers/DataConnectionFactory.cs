using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Helpers
{
    public class DataConnectionFactory
    {
        public static string ConnectionString { get; set; }

        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ConnectionString).Options;

            return new ApplicationDbContext(options);
        }

        public static IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(ConnectionString);

            return connection;
        }
    }
}
