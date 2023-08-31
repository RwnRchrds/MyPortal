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
    public class StudentChargeRepository : BaseReadWriteRepository<StudentCharge>, IStudentChargeRepository
    {
        public StudentChargeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Charges as C", "C.Id", $"{TblAlias}.ChargeId");
            query.LeftJoin("ChargeBillingPeriods as CBP", "CBP.Id", $"{TblAlias}.ChargeBillingPeriodId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Charge), "C");
            query.SelectAllColumns(typeof(ChargeBillingPeriod), "CBP");

            return query;
        }

        protected override async Task<IEnumerable<StudentCharge>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var studentCharges =
                await DbUser.Transaction.Connection
                    .QueryAsync<StudentCharge, Student, Charge, ChargeBillingPeriod, StudentCharge>(sql.Sql,
                        (sc, student, charge, billingPeriod) =>
                        {
                            sc.Student = student;
                            sc.Charge = charge;
                            sc.ChargeBillingPeriod = billingPeriod;

                            return sc;
                        }, sql.NamedBindings, DbUser.Transaction);

            return studentCharges;
        }

        public async Task<IEnumerable<StudentCharge>> GetOutstandingByBillingPeriod(Guid chargeBillingPeriodId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(StudentCharge entity)
        {
            var charge = await DbUser.Context.StudentCharges.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (charge == null)
            {
                throw new EntityNotFoundException("Student charge not found.");
            }

            charge.ChargeBillingPeriodId = entity.ChargeBillingPeriodId;
            charge.Description = entity.Description;
        }
    }
}
