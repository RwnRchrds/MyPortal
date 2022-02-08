using System;
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
        public StudentContactRelationshipRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "RelationshipTypes", "RT", "RelationshipTypeId");
            JoinEntity(query, "Students", "S", "StudentId");
            JoinEntity(query, "Contacts", "C", "ContactId");

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
            var query = GenerateQuery();

            query.Where("C.Id", contactId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<StudentContactRelationship>> GetRelationshipsByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where("S.Id", studentId);

            return await ExecuteQuery(query);
        }

        protected override async Task<IEnumerable<StudentContactRelationship>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var contactRelationships =
                await Transaction.Connection
                    .QueryAsync<StudentContactRelationship, RelationshipType, Student, Contact,
                        StudentContactRelationship>(sql.Sql,
                        (contactRelationship, type, student, contact) =>
                        {
                            contactRelationship.RelationshipType = type;
                            contactRelationship.Student = student;
                            contactRelationship.Contact = contact;

                            return contactRelationship;
                        }, sql.NamedBindings, Transaction);

            return contactRelationships;
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