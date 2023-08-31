using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class BillStudentStudentChargeRepository : BaseReadWriteRepository<BillStudentCharge>,
        IBillStudentChargeRepository
    {
        public BillStudentStudentChargeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Bills as B", "B.Id", $"{TblAlias}.BillId");
            query.LeftJoin("StudentCharges as SC", "SC.Id", $"{TblAlias}.StudentChargeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Bill), "B");
            query.SelectAllColumns(typeof(StudentCharge), "SC");

            return query;
        }

        protected override async Task<IEnumerable<BillStudentCharge>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var billCharges = await DbUser.Transaction.Connection
                .QueryAsync<BillStudentCharge, Bill, StudentCharge, BillStudentCharge>(sql.Sql,
                    (bc, bill, studentCharge) =>
                    {
                        bc.Bill = bill;
                        bc.StudentCharge = studentCharge;

                        return bc;
                    }, sql.NamedBindings, DbUser.Transaction);

            return billCharges;
        }
    }
}