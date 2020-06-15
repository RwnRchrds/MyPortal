using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SaleRepository : BaseReadWriteRepository<Sale>, ISaleRepository
    {
        public SaleRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Person))}
{EntityHelper.GetPropertyNames(typeof(Product))},
{EntityHelper.GetPropertyNames(typeof(AcademicYear))}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Sale].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Product]", "[Product].[Id]", "[Sale].[ProductId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Sale].[AcademicYearId]")}";
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
    }
}