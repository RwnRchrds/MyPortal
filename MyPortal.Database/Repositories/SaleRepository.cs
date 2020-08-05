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
        public SaleRepository(ApplicationDbContext context) : base(context, "Sale")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "Person");
            query.SelectAllColumns(typeof(Product), "Product");
            query.SelectAllColumns(typeof(AcademicYear), "AcademicYear");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "Sale.StudentId");
            query.LeftJoin("People as Person", "Person.Id", "Student.PersonId");
            query.LeftJoin("Products as Product", "Product.Id", "Sale.ProductId");
            query.LeftJoin("AcademicYears as AcademicYear", "AcademicYear.Id", "Sale.AcademicYearId");
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