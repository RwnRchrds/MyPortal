using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Services;
using SqlKata.Compilers;

namespace MyPortal.Tests.TestData
{
    public class TestDataFactory
    {
        private static DbContextOptions<ApplicationDbContext> CreateOptions(DbConnection connection)
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(connection).Options;
        }
        
        internal static ApplicationDbContext GetContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            
            connection.Open();

            var options = CreateOptions(connection);

            using (var context = new ApplicationDbContext(options) {TestData = true})
            {
                context.Database.EnsureCreated();
            }

            return new ApplicationDbContext(CreateOptions(connection)) {TestData = true};
        }

        internal static ISchoolService CreateSchoolService(ApplicationDbContext context)
        {
            return new SchoolService(new SchoolRepository(context.Database.GetDbConnection(), context));
        }
    }
}