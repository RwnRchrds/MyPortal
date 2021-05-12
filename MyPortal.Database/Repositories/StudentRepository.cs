using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Enums;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        private static void ApplySearch(Query query, StudentSearchOptions search)
        {
            switch (search.Status)
            {
                case StudentStatus.OnRoll:
                    query.Where(q =>
                        q.WhereNull("Student.DateLeaving").OrWhereDate("Student.DateLeaving", ">", DateTime.Today));
                    break;
                case StudentStatus.Leavers:
                    query.WhereDate("Student.DateLeaving", "<=", DateTime.Today);
                    break;
                case StudentStatus.Future:
                    query.WhereDate("Student.DateStarting", ">", DateTime.Today);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(search.FirstName))
            {
                query.WhereStarts( "StudentPerson.FirstName", search.FirstName.Trim());
            }

            if (!string.IsNullOrWhiteSpace(search.LastName))
            {
                query.WhereStarts("StudentPerson.LastName", search.LastName.Trim());
            }

            if (!string.IsNullOrWhiteSpace(search.Gender))
            {
                query.Where("StudentPerson.Gender", search.Gender);
            }

            if (search.Dob != null)
            {
                query.WhereDate("StudentPerson.Dob", search.Dob.Value);
            }

            // TODO: Add filter for student group

            if (search.SenStatusId != null)
            {
                query.Where("Student.SenStatusId", search.SenStatusId.Value);
            }
        }

        public async Task<Student> GetByUserId(Guid userId)
        {
            var query = GenerateQuery();

            query.Where("StudentPerson.UserId", userId);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public async Task<Student> GetByPersonId(Guid personId)
        {
            var query = GenerateQuery();

            query.Where("Student.PersonId", personId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(StudentSearchOptions searchParams)
        {
            var query = GenerateQuery();
            
            ApplySearch(query, searchParams);
            
            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Student>> GetByContact(Guid contactId, bool reportableOnly)
        {
            var query = GenerateQuery();

            query.LeftJoin("StudentContactRelationships as SCR", "SCR.StudentId", "Student.Id");

            query.Where("SCR.ContactId", contactId);

            if (reportableOnly)
            {
                query.Where("SCR.PupilReport", true);
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<string>> GetUpns(int leaCode, int establishmentNo, int allocationYear)
        {
            var query = new Query("Students AS S");

            query.Select("S.Upn");

            query.WhereLike("S.Upn", $"{leaCode}{establishmentNo}{allocationYear}", false);

            return await ExecuteQuery<string>(query);
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var query = GenerateQuery();

            query.Where("Student.GiftedAndTalented", true);

            return await ExecuteQuery(query);
        }
    }
}
