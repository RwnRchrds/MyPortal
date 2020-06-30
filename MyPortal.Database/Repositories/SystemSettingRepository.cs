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
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;
using SqlKata.Compilers;
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
            var query = new Query("dbo.SystemSetting");

            query.Select("SystemSetting.Name, SystemSetting.Type, SystemSetting.Setting");

            query.Where("SystemSetting.Name", name);

            var sql = new SqlServerCompiler().Compile(query);

            return (await _connection.QueryAsync<SystemSetting>(sql.Sql, sql.NamedBindings)).FirstOrDefault();
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
