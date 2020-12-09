using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context, "Student")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(User), "User");
            query.SelectAllColumns(typeof(RegGroup), "RegGroup");
            query.SelectAllColumns(typeof(StaffMember), "Tutor");
            query.SelectAllColumns(typeof(Person), "TutorPerson");
            query.SelectAllColumns(typeof(YearGroup), "YearGroup");
            query.SelectAllColumns(typeof(StaffMember), "HeadOfYear");
            query.SelectAllColumns(typeof(Person), "HeadOfYearPerson");
            query.SelectAllColumns(typeof(House), "House");
            query.SelectAllColumns(typeof(SenStatus), "SenStatus");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Users as User", "User.PersonId", "StudentPerson.Id");
            query.LeftJoin("RegGroups as RegGroup", "RegGroup.Id", "Student.RegGroupId");
            query.LeftJoin("StaffMembers as Tutor", "Tutor.Id", "RegGroup.TutorId");
            query.LeftJoin("People as TutorPerson", "TutorPerson.Id", "Tutor.PersonId");
            query.LeftJoin("YearGroups as YearGroup", "YearGroup.Id", "Student.YearGroupId");
            query.LeftJoin("StaffMembers as HeadOfYear", "HeadOfYear.Id", "YearGroup.HeadId");
            query.LeftJoin("People as HeadOfYearPerson", "HeadOfYearPerson.Id", "HeadOfYear.PersonId");
            query.LeftJoin("Houses as House", "House.Id", "Student.HouseId");
            query.LeftJoin("SenStatus", "SenStatus.Id", "Student.SenStatusId");
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
                query.WhereStarts( "StudentPerson.FirstName", search.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(search.LastName))
            {
                query.WhereStarts("StudentPerson.LastName", search.LastName);
            }

            if (!string.IsNullOrWhiteSpace(search.Gender))
            {
                query.Where("StudentPerson.Gender", search.Gender);
            }

            if (search.Dob != null)
            {
                query.WhereDate("StudentPerson.Dob", search.Dob.Value);
            }

            if (search.CurriculumGroupId != null)
            {
                query.LeftJoin("CurriculumGroupMembership as Membership", "Membership.StudentId", "Student.Id");

                query.Where("Membership.GroupId", search.CurriculumGroupId);
            }

            if (search.RegGroupId != null)
            {
                query.Where("Student.RegGroupId", search.RegGroupId.Value);
            }

            if (search.YearGroupId != null)
            {
                query.Where("Student.YearGroupId", search.YearGroupId.Value);
            }

            if (search.HouseId != null)
            {
                query.Where("Student.HouseId", search.HouseId.Value);
            }

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

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var query = GenerateQuery();

            query.Where("Student.GiftedAndTalented", true);

            return await ExecuteQuery(query);
        }

        protected override async Task<IEnumerable<Student>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync(sql.Sql,
                new[]
                {
                    typeof(Student), typeof(Person), typeof(User), typeof(RegGroup), typeof(StaffMember),
                    typeof(Person), typeof(YearGroup), typeof(StaffMember), typeof(Person), typeof(House), typeof(SenStatus)
                },
                objects =>
                {
                    var student = (Student) objects[0];

                    student.Person = (Person) objects[1];
                    student.Person.User = (User) objects[2];
                    student.RegGroup = (RegGroup) objects[3];
                    student.RegGroup.Tutor = (StaffMember) objects[4];
                    student.RegGroup.Tutor.Person = (Person) objects[5];
                    student.YearGroup = (YearGroup) objects[6];
                    
                    if (objects[7] != null)
                    {
                        student.YearGroup.HeadOfYear = (StaffMember) objects[7];
                        student.YearGroup.HeadOfYear.Person = (Person) objects[8];
                    }
                    
                    student.House = (House) objects[9];
                    student.SenStatus = (SenStatus) objects[10];

                    return student;
                }, sql.NamedBindings);
        }
    }
}
