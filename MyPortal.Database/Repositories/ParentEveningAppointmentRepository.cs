using System;
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
    public class ParentEveningAppointmentRepository : BaseReadWriteRepository<ParentEveningAppointment>,
        IParentEveningAppointmentRepository
    {
        public ParentEveningAppointmentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ParentEveningStaffMembers as PESM", "PESM.Id", $"{TblAlias}.ParentEveningStaffId");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ParentEveningStaffMember), "PESM");
            query.SelectAllColumns(typeof(Student), "S");

            return query;
        }

        protected override async Task<IEnumerable<ParentEveningAppointment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var appointments =
                await DbUser.Transaction.Connection
                    .QueryAsync<ParentEveningAppointment, ParentEveningStaffMember, Student, ParentEveningAppointment>(
                        sql.Sql,
                        (appointment, staff, student) =>
                        {
                            appointment.ParentEveningStaffMember = staff;
                            appointment.Student = student;

                            return appointment;
                        }, sql.NamedBindings, DbUser.Transaction);

            return appointments;
        }

        public async Task Update(ParentEveningAppointment entity)
        {
            var appointment =
                await DbUser.Context.ParentEveningAppointments.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (appointment == null)
            {
                throw new EntityNotFoundException("Appointment not found.");
            }

            appointment.Start = entity.Start;
            appointment.End = entity.End;
            appointment.Attended = entity.Attended;
        }

        public async Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByStaffMember(Guid parentEveningId,
            Guid staffMemberId)
        {
            var query = GetDefaultQuery();

            query.Where("PESM.ParentEveningId", parentEveningId);
            query.Where("PESM.StaffMemberId", staffMemberId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByContact(Guid parentEveningId,
            Guid contactId)
        {
            return null;
        }

        public async Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByStaffMember(Guid staffMemberId,
            DateTime fromDate, DateTime toDate)
        {
            return null;
        }

        public async Task<IEnumerable<ParentEveningAppointment>> GetAppointmentsByContact(Guid contactId,
            DateTime fromDate, DateTime toDate)
        {
            return null;
        }
    }
}