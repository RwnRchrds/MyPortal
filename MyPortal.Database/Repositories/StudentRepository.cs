using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Student;
using MyPortal.Database.Models.Search;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        private static Query HouseCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("Houses as H", "H.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.Select("SG.Description as HouseName");
                query.Select("H.Id as HouseId");

                return query;
            }
        }

        private static Query RegGroupCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("RegGroups as R", "R.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.Select("SG.Description as RegGroupName");
                query.Select("R.Id as RegGroupId");

                return query;
            }
        }

        private static Query YearGroupCte
        {
            get
            {
                var query = new Query("Students as S");
                query.LeftJoin("StudentGroupMemberships as SGM", "SGM.StudentId", "S.Id");
                query.LeftJoin("StudentGroups as SG", "SG.Id", "SGM.StudentGroupId");
                query.Join("YearGroups as Y", "Y.StudentGroupId", "SG.Id");

                query.Select("S.Id as StudentId");
                query.Select("SG.Description as YearGroupName");
                query.Select("Y.Id as YearGroupId");

                return query;
            }
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("People as P", "P.Id", $"{TblAlias}.PersonId");
            query.LeftJoin("SenStatus as SS", "SS.Id", $"{TblAlias}.SenStatusId");
            query.LeftJoin("SenTypes as ST", "ST.Id", $"{TblAlias}.SenTypeId");
            query.LeftJoin("EnrolmentStatus as ES", "ES.Id", $"{TblAlias}.EnrolmentStatusId");
            query.LeftJoin("BoarderStatus as BS", "BS.Id", $"{TblAlias}.BoarderStatusId");

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

            var students = await DbUser.Transaction.Connection
                .QueryAsync<Student, Person, SenStatus, SenType, EnrolmentStatus, BoarderStatus, Student>(sql.Sql,
                    (student, person, senStatus, senType, enrolmentStatus, boarderStatus) =>
                    {
                        student.Person = person;
                        student.SenStatus = senStatus;
                        student.SenType = senType;
                        student.EnrolmentStatus = enrolmentStatus;
                        student.BoarderStatus = boarderStatus;

                        return student;
                    }, sql.NamedBindings, DbUser.Transaction);

            return students;
        }

        private static void ApplySearch(Query query, StudentSearchOptions search, string studentAlias,
            string personAlias, string studentHouseAlias = null, string studentRegGroupAlias = null,
            string studentYearGroupAlias = null)
        {
            // Insert CTEs needed for search

            if (string.IsNullOrWhiteSpace(studentHouseAlias))
            {
                studentHouseAlias = "SH";

                query.With("StudentHouse", HouseCte);
                query.LeftJoin($"StudentHouse as {studentHouseAlias}", $"{studentHouseAlias}.StudentId",
                    $"{studentAlias}.Id");
            }

            if (string.IsNullOrWhiteSpace(studentRegGroupAlias))
            {
                studentRegGroupAlias = "SR";

                query.With("StudentRegGroup", RegGroupCte);
                query.LeftJoin($"StudentRegGroup as {studentRegGroupAlias}", $"{studentRegGroupAlias}.StudentId",
                    $"{studentAlias}.Id");
            }

            if (string.IsNullOrWhiteSpace(studentYearGroupAlias))
            {
                studentYearGroupAlias = "SY";

                query.With("StudentYearGroup", YearGroupCte);
                query.LeftJoin($"StudentYearGroup as {studentYearGroupAlias}", $"{studentYearGroupAlias}.StudentId",
                    $"{studentAlias}.Id");
            }

            search.ApplySearch(query, studentAlias, personAlias, studentHouseAlias, studentRegGroupAlias,
                studentYearGroupAlias);
        }

        public async Task<Student> GetByUserId(Guid userId)
        {
            var query = GetDefaultQuery();

            query.Where("StudentPerson.UserId", userId);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public async Task<Student> GetByPersonId(Guid personId)
        {
            var query = GetDefaultQuery();

            query.Where("Student.PersonId", personId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(StudentSearchOptions searchParams)
        {
            var query = GetDefaultQuery();

            ApplySearch(query, searchParams, TblAlias, "P");

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<StudentSearchResult>> SearchAll(StudentSearchOptions searchOptions)
        {
            var query = GetEmptyQuery();
            query.With("StudentHouse", HouseCte);
            query.With("StudentRegGroup", RegGroupCte);
            query.With("StudentYearGroup", YearGroupCte);

            query.LeftJoin("People as P", "P.Id", "S.PersonId");
            query.LeftJoin("StudentHouse as SH", "SH.StudentId", "S.Id");
            query.LeftJoin("StudentRegGroup as SR", "SR.StudentId", "S.Id");
            query.LeftJoin("StudentYearGroup as SY", "SY.StudentId", "S.Id");

            query.Select("S.Id", "P.FirstName", "P.PreferredFirstName", "P.MiddleName", "P.LastName",
                "P.PreferredLastName", "P.Gender", "SH.HouseName", "SR.RegGroupName", "SY.YearGroupName");
            ApplySearch(query, searchOptions, TblAlias, "P", "SH", "SR", "SY");
            return await ExecuteQuery<StudentSearchResult>(query);
        }

        public async Task<IEnumerable<Student>> GetByContact(Guid contactId, bool reportableOnly)
        {
            var query = GetDefaultQuery();

            query.LeftJoin("StudentContactRelationships as SCR", "SCR.StudentId", "Student.Id");

            query.Where("SCR.ContactId", contactId);

            if (reportableOnly)
            {
                query.Where("SCR.ParentalResponsibility", true);
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<string>> GetUpns(int leaCode, int establishmentNo, int allocationYear)
        {
            var query = new Query("Students AS S");

            query.Select("S.Upn");

            query.WhereLike("S.Upn", $"{leaCode}{establishmentNo}{allocationYear}");

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
            var query = GetDefaultQuery();

            query.Where("Student.GiftedAndTalented", true);

            return await ExecuteQuery(query);
        }

        public async Task Update(Student entity)
        {
            var student = await DbUser.Context.Students.FirstOrDefaultAsync(x => x.Id == entity.Id);

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