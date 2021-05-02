using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SchoolRepository : BaseReadWriteRepository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task<string> GetLocalSchoolName()
        {
            var query = new Query(TblName).Select("School.Name");

            query.Where("School.Local", true);

            return await ExecuteQueryStringResult(query);
        }

        public async Task<School> GetLocal()
        {
            var query = GenerateQuery();

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }

        public async Task Update(School entity)
        {
            var school = await Context.Schools.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (school == null)
            {
                throw new EntityNotFoundException("School not found.");
            }

            school.Name = entity.Name;
            school.LocalAuthorityId = entity.LocalAuthorityId;
            school.EstablishmentNumber = entity.EstablishmentNumber;
            school.Urn = entity.Urn;
            school.Uprn = entity.Uprn;
            school.PhaseId = entity.PhaseId;
            school.TypeId = entity.TypeId;
            school.GovernanceTypeId = entity.GovernanceTypeId;
            school.IntakeTypeId = entity.IntakeTypeId;
            school.HeadTeacherId = entity.HeadTeacherId;
            school.TelephoneNo = entity.TelephoneNo;
            school.FaxNo = entity.FaxNo;
            school.EmailAddress = entity.EmailAddress;
            school.Website = entity.Website;
        }
    }
}
