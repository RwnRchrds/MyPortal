using System.Collections.Generic;
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
    public class StudentContactRelationshipRepository : BaseReadWriteRepository<StudentContactRelationship>, IStudentContactRelationshipRepository
    {
        public StudentContactRelationshipRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "StudentContact")
        {

        }   

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(RelationshipType), "RelationshipType");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(Contact), "Contact");
            query.SelectAllColumns(typeof(Person), "ContactPerson");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("RelationshipTypes as RelationshipType", "RelationshipType.Id", "StudentContact.RelationshipTypeId");
            query.LeftJoin("Students as Student", "Student.Id", "StudentContact.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Contacts as Contact", "Contact.Id", "StudentContact.ContactId");
            query.LeftJoin("People as ContactPerson", "ContactPerson.Id", "Contact.PersonId");
        }

        protected override async Task<IEnumerable<StudentContactRelationship>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection
                .QueryAsync<StudentContactRelationship, RelationshipType, Student, Person, Contact, Person, StudentContactRelationship>(sql.Sql,
                    (sContact, relationship, student, sPerson, contact, cPerson) =>
                    {
                        sContact.RelationshipType = relationship;
                        sContact.Student = student;
                        sContact.Student.Person = sPerson;
                        sContact.Contact = contact;
                        sContact.Contact.Person = cPerson;

                        return sContact;
                    }, sql.NamedBindings, Transaction);
        }

        public async Task Update(StudentContactRelationship entity)
        {
            var relationship = await Context.StudentContactRelationships.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (relationship == null)
            {
                throw new EntityNotFoundException("Contact relationship not found.");
            }

            relationship.Correspondence = entity.Correspondence;
            relationship.ParentalResponsibility = entity.ParentalResponsibility;
            relationship.CourtOrder = entity.CourtOrder;
            relationship.PupilReport = entity.PupilReport;
        }
    }
}