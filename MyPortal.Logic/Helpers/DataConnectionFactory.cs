using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyPortal.Database.Models;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Helpers
{
    public class DataConnectionFactory
    {
        public static string ConnectionString { get; set; }

        public static ApplicationDbContext CreateContext()
        {
            CheckConnectionString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(ConnectionString).Options;

            return new ApplicationDbContext(options);
        }

        public static IDbConnection CreateConnection()
        {
            CheckConnectionString();

            var connection = new SqlConnection(ConnectionString);

            return connection;
        }

        private static void CheckConnectionString()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new ConnectionStringException("The connection string has not been set.");
            }
        }
    }
}
