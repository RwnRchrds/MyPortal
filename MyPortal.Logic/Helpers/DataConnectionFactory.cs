using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    internal class DataConnectionFactory
    {
        private static ApplicationDbContext CreateContext()
        {
            CheckConnectionString();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            DbContextOptions<ApplicationDbContext> options;

            switch (Configuration.Instance.DatabaseProvider)
            {
                case DatabaseProvider.MsSqlServer:
                    options = builder.UseSqlServer(Configuration.Instance.ConnectionString).Options;
                    break;
                case DatabaseProvider.MySql:
                    options = builder.UseMySQL(Configuration.Instance.ConnectionString).Options;
                    break;
                default:
                    throw new ConfigurationException("A database provider has not been set.");
            }

            return new ApplicationDbContext(options);
        }

        private static SqlConnection CreateConnection()
        {
            CheckConnectionString();

            var connection = new SqlConnection(Configuration.Instance.ConnectionString);

            return connection;
        }

        internal static async Task CheckDbConnection()
        {
            var connection = CreateConnection();

            await connection.OpenAsync();
            await connection.CloseAsync();
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
