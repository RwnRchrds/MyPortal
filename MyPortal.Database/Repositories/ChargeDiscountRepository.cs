using System;
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
    public class ChargeDiscountRepository : BaseReadWriteRepository<ChargeDiscount>, IChargeDiscountRepository
    {
        public ChargeDiscountRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Charges as C", "C.Id", $"{TblAlias}.ChargeId");
            query.LeftJoin("Discounts as D", "D.Id", $"{TblAlias}.DiscountId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Charge), "C");
            query.SelectAllColumns(typeof(Discount), "D");

            return query;
        }

        protected override async Task<IEnumerable<ChargeDiscount>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var chargeDiscounts =
                await DbUser.Transaction.Connection.QueryAsync<ChargeDiscount, Charge, Discount, ChargeDiscount>(
                    sql.Sql,
                    (chargeDiscount, charge, discount) =>
                    {
                        chargeDiscount.Charge = charge;
                        chargeDiscount.Discount = discount;

                        return chargeDiscount;
                    }, sql.NamedBindings, DbUser.Transaction);

            return chargeDiscounts;
        }

        public async Task<IEnumerable<ChargeDiscount>> GetByDiscount(Guid discountId)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.DiscountId", discountId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<ChargeDiscount>> GetByStudent(Guid studentId)
        {
            var query = GetDefaultQuery();

            query.Join("StudentChargeDiscounts as SCD", "SCD.ChargeDiscountId", $"{TblAlias}.Id");

            query.Where("SCD.StudentId", studentId);

            return await ExecuteQuery(query);
        }
    }
}