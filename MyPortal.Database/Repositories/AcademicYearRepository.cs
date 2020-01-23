using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class AcademicYearRepository : BaseRepository, IAcademicYearRepository
    {
        private const string TblName = "[dbo].[AcademicYear] AS [A]";

        private const string AllColumns = "[A].[Id],[A].[Name],[A].[FirstDate],[A].[LastDate]";

        public AcademicYearRepository(IDbConnection connection) : base(connection)
        {
        }

        public Task<IEnumerable<AcademicYear>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return Connection.QueryAsync<AcademicYear>(sql);
        }

        public Task<IEnumerable<AcademicYear>> GetById(int id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [A].[Id] = @AcademicYearId";

            return Connection.QueryAsync<AcademicYear>(sql, new {AcademicYearId = id});
        }

        public void Create(AcademicYear entity)
        {
            Context.AcademicYears.Add(entity);
        }

        public async Task Update(AcademicYear entity)
        {
            var academicYearInDb = await Context.AcademicYears.FindAsync(entity.Id);

            if (academicYearInDb == null)
            {
                throw new Exception("Academic year not found.");
            }

            academicYearInDb.Name = entity.Name;
        }

        public async Task Delete(int id)
        {
            var academicYearInDb = await Context.AcademicYears.FindAsync(id);

            if (academicYearInDb == null)
            {
                throw new Exception("Academic year not found.");
            }

            Context.AcademicYears.Remove(academicYearInDb);
        }

        public Task<AcademicYear> GetCurrent()
        {
            var sql =
                $"SELECT {AllColumns} FROM {TblName} WHERE [A].[FirstDate] <= DateToday AND [A].[LastDate] >= DateToday";

            return Connection.QueryFirstOrDefaultAsync<AcademicYear>(sql, new {DateToday = DateTime.Today});
        }
    }
}
