using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentContactRelationshipRepository : BaseReadWriteRepository<StudentContactRelationship>,
        IStudentContactRelationshipRepository
    {
        public StudentContactRelationshipRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("RelationshipTypes as RT", "RT.Id", $"{TblAlias}.RelationshipTypeId");
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Contacts as C", "C.Id", $"{TblAlias}.ContactId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(RelationshipType), "RT");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Contact), "C");

            return query;
        }

        public async Task<IEnumerable<StudentContactRelationship>> GetRelationshipsByContact(Guid contactId)
        {
            var query = GetDefaultQuery();

            query.Where("C.Id", contactId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<StudentContactRelationship>> GetRelationshipsByStudent(Guid studentId)
        {
            var query = GetDefaultQuery();

            query.Where("S.Id", studentId);

            return await ExecuteQuery(query);
        }

        protected override async Task<IEnumerable<StudentContactRelationship>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var contactRelationships =
                await DbUser.Transaction.Connection
                    .QueryAsync<StudentContactRelationship, RelationshipType, Student, Contact,
                        StudentContactRelationship>(sql.Sql,
                        (contactRelationship, type, student, contact) =>
                        {
                            contactRelationship.RelationshipType = type;
                            contactRelationship.Student = student;
                            contactRelationship.Contact = contact;

                            return contactRelationship;
                        }, sql.NamedBindings, DbUser.Transaction);

            return contactRelationships;
        }

        public async Task Update(StudentContactRelationship entity)
        {
            var relationship =
                await DbUser.Context.StudentContactRelationships.FirstOrDefaultAsync(x => x.Id == entity.Id);

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