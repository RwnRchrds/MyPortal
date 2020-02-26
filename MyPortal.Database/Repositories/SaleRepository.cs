using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class SaleRepository : BaseReadWriteRepository<Sale>, ISaleRepository
    {
        public SaleRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person))}
{EntityHelper.GetAllColumns(typeof(Product))},
{EntityHelper.GetAllColumns(typeof(AcademicYear))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Sale].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Product]", "[Product].[Id]", "[Sale].[ProductId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Sale].[AcademicYearId]")}";
        }

        protected override async Task<IEnumerable<Sale>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Sale, Student, Person, Product, AcademicYear, Sale>(sql,
                (sale, student, person, product, acadYear) =>
                {
                    sale.Student = student;
                    sale.Student.Person = person;
                    sale.Product = product;
                    sale.AcademicYear = acadYear;

                    return sale;
                }, param);
        }

        public async Task Update(Sale entity)
        {
            var sale = await Context.Sales.FindAsync(entity.Id);

            sale.Processed = entity.Processed;
            sale.Refunded = entity.Refunded;
            sale.Deleted = entity.Deleted;
        }
    }
}