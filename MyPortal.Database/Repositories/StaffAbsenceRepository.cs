using System.Collections.Generic;
using System.Data.Common;
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
    public class StaffAbsenceRepository : BaseReadWriteRepository<StaffAbsence>, IStaffAbsenceRepository
    {
        public StaffAbsenceRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StaffMembers", "SM", "StaffMemberId");
            JoinEntity(query, "StaffAbsenceTypes", "SAT", "AbsenceTypeId");
            JoinEntity(query, "StaffIllnessTypes", "SIT", "IllnessTypeId");

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

            var absences = await Transaction.Connection
                .QueryAsync<StaffAbsence, StaffMember, StaffAbsenceType, StaffIllnessType, StaffAbsence>(sql.Sql,
                    (absence, staff, type, illnessType) =>
                    {
                        absence.StaffMember = staff;
                        absence.AbsenceType = type;
                        absence.IllnessType = illnessType;

                        return absence;
                    }, sql.NamedBindings, Transaction);

            return absences;
        }

        public async Task Update(StaffAbsence entity)
        {
            var absence = await Context.StaffAbsences.FirstOrDefaultAsync(x => x.Id == entity.Id);

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