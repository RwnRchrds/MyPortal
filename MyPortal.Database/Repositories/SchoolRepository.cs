using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseRepository, ISchoolRepository
    {
        private const string TblName = @"[dbo].[School] as [S]";

        private const string AllColumns =
            @"[S].[Id],[S].[Name],[S].[LocalAuthorityId],[S].[EstablishmentNumber],[S].[Urn],[S].[Uprn],[S].[PhaseId],[S].[TypeId],[S].[GovernanceTypeId],[S].[IntakeTypeId],
[S].[HeadTeacherId],[S].[TelephoneNo],[S].[FaxNo],[S].[EmailAddress],[S].[Website],[S].[Local]";

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
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [S].[Id] = @SchoolId";

            return await Connection.QuerySingleOrDefaultAsync<School>(sql, new {SchoolId = id});
        }

        public void Create(School entity)
        {
            Context.Schools.Add(entity);
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

        public async Task Delete(int id)
        {
            var schoolInDb = await Context.Schools.FindAsync(id);

            if (schoolInDb == null)
            {
                throw new Exception("School not found.");
            }

            Context.Schools.Remove(schoolInDb);
        }

        public async Task<string> GetLocalSchoolName()
        {
            var sql = $"SELECT [S].[Name] FROM {TblName} WHERE [S].[Local] = 1";

            return await Connection.QuerySingleOrDefaultAsync<string>(sql);
        }

        public async Task<School> GetLocal()
        {
            var sql = $"SELECT {AllColumns} FROM {TblName} WHERE [S].[Local] = 1";

            return await Connection.QuerySingleOrDefaultAsync<School>(sql);
        }
    }
}
