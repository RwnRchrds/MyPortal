using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class SaleRepository : BaseReadWriteRepository<Sale>, ISaleRepository
    {
        public SaleRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person));
            query.SelectAll(typeof(Product));
            query.SelectAll(typeof(AcademicYear));
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "Sale.StudentId");
            query.LeftJoin("dbo.Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("dbo.Product", "Product.Id", "Sale.ProductId");
            query.LeftJoin("dbo.AcademicYear", "AcademicYear.Id", "Sale.AcademicYearId");
        }

        protected override async Task<IEnumerable<Sale>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Sale, Student, Person, Product, AcademicYear, Sale>(sql.Sql,
                (sale, student, person, product, acadYear) =>
                {
                    sale.Student = student;
                    sale.Student.Person = person;
                    sale.Product = product;
                    sale.AcademicYear = acadYear;

                    return sale;
                }, sql.NamedBindings);
        }
    }
}