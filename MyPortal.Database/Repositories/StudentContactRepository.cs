using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentContactRepository : BaseReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(RelationshipType));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Contact));
            query.SelectAll(typeof(Person), "ContactPerson");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.RelationshipType", "RelationshipType.Id", "StudentContact.RelationshipTypeId");
            query.LeftJoin("dbo.Student", "Student.Id", "StudentContact.StudentId");
            query.LeftJoin("dbo.Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("dbo.Contact", "Contact.Id", "StudentContact.ContactId");
            query.LeftJoin("dbo.Person as ContactPerson", "ContactPerson.Id", "Contact.PersonId");
        }

        protected override async Task<IEnumerable<StudentContact>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<StudentContact, RelationshipType, Student, Person, Contact, Person, StudentContact>(sql.Sql,
                    (sContact, relationship, student, sPerson, contact, cPerson) =>
                    {
                        sContact.RelationshipType = relationship;
                        sContact.Student = student;
                        sContact.Student.Person = sPerson;
                        sContact.Contact = contact;
                        sContact.Contact.Person = cPerson;

                        return sContact;
                    }, sql.NamedBindings);
        }
    }
}