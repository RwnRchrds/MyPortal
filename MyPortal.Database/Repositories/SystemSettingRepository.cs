using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories
{
    public class SystemSettingRepository : ISystemSettingRepository
    {
        private ApplicationDbContext _context;
        private DbTransaction _transaction;

        public SystemSettingRepository(ApplicationDbContext context, DbTransaction transaction)
        {
            _transaction = transaction;
            _context = context;
        }

        public async Task<SystemSetting> Get(string name)
        {
            var query = new Query("SystemSettings as SystemSetting");

            query.Select("SystemSetting.Name, SystemSetting.Type, SystemSetting.Setting");

            query.Where("SystemSetting.Name", name);

            var sql = new SqlServerCompiler().Compile(query);

            return (await _transaction.Connection.QueryAsync<SystemSetting>(sql.Sql, sql.NamedBindings, _transaction)).FirstOrDefault();
        }

        public async Task<SystemSetting> GetForEditing(string name)
        {
            var setting = await _context.SystemSettings.FirstOrDefaultAsync(x => x.Name == name);

            return setting;
        }
    }
}
