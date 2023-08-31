using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StaffAbsenceRepository : BaseReadWriteRepository<StaffAbsence>, IStaffAbsenceRepository
    {
        public StaffAbsenceRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as SM", "SM.Id", $"{TblAlias}.StaffMemberId");
            query.LeftJoin("StaffAbsenceTypes as SAT", "SAT.Id", $"{TblAlias}.AbsenceTypeId");
            query.LeftJoin("StaffIllnessTypes as SIT", "SIT.Id", $"{TblAlias}.IllnessTypeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "SM");
            query.SelectAllColumns(typeof(StaffAbsenceType), "SAT");
            query.SelectAllColumns(typeof(StaffIllnessType), "SIT");

            return query;
        }

        protected override async Task<IEnumerable<StaffAbsence>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var absences = await DbUser.Transaction.Connection
                .QueryAsync<StaffAbsence, StaffMember, StaffAbsenceType, StaffIllnessType, StaffAbsence>(sql.Sql,
                    (absence, staff, type, illnessType) =>
                    {
                        absence.StaffMember = staff;
                        absence.AbsenceType = type;
                        absence.IllnessType = illnessType;

                        return absence;
                    }, sql.NamedBindings, DbUser.Transaction);

            return absences;
        }

        public async Task Update(StaffAbsence entity)
        {
            var absence = await DbUser.Context.StaffAbsences.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (absence == null)
            {
                throw new EntityNotFoundException("Absence not found.");
            }

            absence.AbsenceTypeId = entity.AbsenceTypeId;
            absence.IllnessTypeId = entity.IllnessTypeId;
            absence.StartDate = entity.StartDate;
            absence.EndDate = entity.EndDate;
            absence.Confidential = entity.Confidential;
            absence.Notes = entity.Notes;
        }
    }
}