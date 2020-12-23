using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentContactRepository : BaseReadWriteRepository<StudentContactRelationship>, IStudentContactRepository
    {
        public StudentContactRepository(ApplicationDbContext context) : base(context, "StudentContact")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ContactRelationshipType), "RelationshipType");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(Contact), "Contact");
            query.SelectAllColumns(typeof(Person), "ContactPerson");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("ContactRelationshipTypes as RelationshipType", "RelationshipType.Id", "StudentContact.RelationshipTypeId");
            query.LeftJoin("Students as Student", "Student.Id", "StudentContact.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Contacts as Contact", "Contact.Id", "StudentContact.ContactId");
            query.LeftJoin("People as ContactPerson", "ContactPerson.Id", "Contact.PersonId");
        }

        protected override async Task<IEnumerable<StudentContactRelationship>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<StudentContactRelationship, ContactRelationshipType, Student, Person, Contact, Person, StudentContactRelationship>(sql.Sql,
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