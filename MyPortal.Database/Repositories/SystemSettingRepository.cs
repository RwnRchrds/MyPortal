using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SystemSettingRepository : ISystemSettingRepository
    {
        private IDbConnection _connection;
        private ApplicationDbContext _context;

        public SystemSettingRepository(IDbConnection connection, ApplicationDbContext context)
        {
            _connection = connection;
            _context = context;
        }

        public async Task<SystemSetting> Get(string name)
        {
            var sql = @"SELECT [Name],[Type],[Setting] FROM [dbo].[SystemSetting]";

            SqlHelper.Where(ref sql, "[SystemSetting].[Name] = @Name");

            return (await _connection.QueryAsync<SystemSetting>(sql, new {Name = name})).FirstOrDefault();
        }

        public async Task<SystemSetting> GetWithTracking(string name)
        {
            var setting = await _context.SystemSettings.FirstOrDefaultAsync(x => x.Name == name);

            return setting;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _context?.Dispose();
        }
    }
}
