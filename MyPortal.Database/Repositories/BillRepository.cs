using System.Collections.Generic;
using System.Data;
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
    public class BillRepository : BaseReadWriteRepository<Bill>, IBillRepository
    {
        public BillRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");

            return query;
        }

        protected override async Task<IEnumerable<Bill>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var bills = await Transaction.Connection.QueryAsync<Bill, Student, Bill>(sql.Sql, (bill, student) =>
            {
                bill.Student = student;

                return bill;
            }, sql.NamedBindings, Transaction);

            return bills;
        }

        public async Task Update(Bill entity)
        {
            var bill = await Context.Bills.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (bill == null)
            {
                throw new EntityNotFoundException("Bill not found.");
            }

            bill.DueDate = entity.DueDate;
            bill.Dispatched = entity.Dispatched;
        }
    }
}