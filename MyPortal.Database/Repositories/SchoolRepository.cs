using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        private readonly string TblName = @"[dbo].[School] as [School]";

        internal static readonly string AllColumns = EntityHelper.GetAllColumns(typeof(School), "School");

        private readonly string JoinPhase = @"LEFT JOIN [dbo].[Phase] AS [Phase] ON [Phase].[Id] = [School].[PhaseId]";

        public SchoolRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<School>> GetAll()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName}";

            return await Connection.QueryAsync<School>(sql);
        }

        public async Task<School> GetById(int id)
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [School].[Id] = @SchoolId";

            return (await ExecuteQuery(sql, new {SchoolId = id})).SingleOrDefault();
        }

        public async Task Update(School entity)
        {
            var schoolInDb = await Context.Schools.FindAsync(entity.Id);

            if (schoolInDb == null)
            {
                throw new Exception("School not found.");
            }

            schoolInDb.Name = entity.Name;
            schoolInDb.LocalAuthorityId = entity.LocalAuthorityId;
            schoolInDb.EstablishmentNumber = entity.EstablishmentNumber;
            schoolInDb.Urn = entity.Urn;
            schoolInDb.Uprn = entity.Uprn;
            schoolInDb.PhaseId = entity.PhaseId;
            schoolInDb.TypeId = entity.TypeId;
            schoolInDb.GovernanceTypeId = entity.GovernanceTypeId;
            schoolInDb.IntakeTypeId = entity.IntakeTypeId;
            schoolInDb.HeadTeacherId = entity.HeadTeacherId;
            schoolInDb.TelephoneNo = entity.TelephoneNo;
            schoolInDb.FaxNo = entity.FaxNo;
            schoolInDb.EmailAddress = entity.EmailAddress;
            schoolInDb.Website = entity.Website;
        }

        public async Task<string> GetLocalSchoolName()
        {
            var sql = $"SELECT [School].[Name] FROM {TblName} WHERE [School].[Local] = 1";

            return await Connection.QuerySingleOrDefaultAsync<string>(sql);
        }

        public async Task<School> GetLocal()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [School].[Local] = 1";

            return (await ExecuteQuery(sql)).SingleOrDefault();
        }

        protected override async Task<IEnumerable<School>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<School>(sql, param);
        }
    }
}
