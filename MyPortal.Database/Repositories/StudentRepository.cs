using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Enums;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        private Query HouseCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("Houses as H", "H.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.SelectAllColumns(typeof(House), "H");

                return query;
            }
        }

        private Query RegGroupCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("RegGroups as R", "R.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.SelectAllColumns(typeof(RegGroup), "R");

                return query;
            }
        }

        private Query YearGroupCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("YearGroups as Y", "Y.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.SelectAllColumns(typeof(YearGroup), "Y");

                return query;
            }
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "People", "P", "PersonId");
            JoinEntity(query, "SenStatus", "SS", "SenStatusId");
            JoinEntity(query, "SenTypes", "ST", "SenTypeId");
            JoinEntity(query, "EnrolmentStatus", "ES", "EnrolmentStatusId");
            JoinEntity(query, "BoarderStatus", "BS", "BoarderStatusId");
            
            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "P");
            query.SelectAllColumns(typeof(SenStatus), "SS");
            query.SelectAllColumns(typeof(SenType), "ST");
            query.SelectAllColumns(typeof(EnrolmentStatus), "ES");
            query.SelectAllColumns(typeof(BoarderStatus), "BS");

            return query;
        }

        protected override async Task<IEnumerable<Student>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var students = await Transaction.Connection
                .QueryAsync<Student, Person, SenStatus, SenType, EnrolmentStatus, BoarderStatus, Student>(sql.Sql,
                    (student, person, senStatus, senType, enrolmentStatus, boarderStatus) =>
                    {
                        student.Person = person;
                        student.SenStatus = senStatus;
                        student.SenType = senType;
                        student.EnrolmentStatus = enrolmentStatus;
                        student.BoarderStatus = boarderStatus;

                        return student;
                    }, sql.NamedBindings, Transaction);

            return students;
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

        public async Task<IEnumerable<Student>> GetAllExtended(StudentSearchOptions searchOptions)
        {
            var query = GenerateQuery();
            ApplySearch(query, searchOptions);
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

        public async Task<IEnumerable<int>> GetAdmissionNumbers()
        {
            var query = new Query("Students AS S");

            query.Select("S.AdmissionNumber");

            return await ExecuteQuery<int>(query);
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var query = GenerateQuery();

            query.Where("Student.GiftedAndTalented", true);

            return await ExecuteQuery(query);
        }

        public async Task Update(Student entity)
        {
            var student = await Context.Students.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (student == null)
            {
                throw new EntityNotFoundException("Student not found.");
            }
            
            student.AdmissionNumber = entity.AdmissionNumber;
            student.DateStarting = entity.DateStarting;
            student.DateLeaving = entity.DateLeaving;
            student.FreeSchoolMeals = entity.FreeSchoolMeals;
            student.SenStatusId = entity.SenStatusId;
            student.SenTypeId = entity.SenTypeId;
            student.EnrolmentStatusId = entity.EnrolmentStatusId;
            student.PupilPremium = entity.PupilPremium;
            student.Upn = entity.Upn;
        }
    }
}
