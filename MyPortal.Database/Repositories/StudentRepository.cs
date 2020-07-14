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
using MyPortal.Database.Models.Identity;
using MyPortal.Database.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(ApplicationUser), "User");
            query.SelectAll(typeof(RegGroup));
            query.SelectAll(typeof(StaffMember), "Tutor");
            query.SelectAll(typeof(Person), "TutorPerson");
            query.SelectAll(typeof(YearGroup));
            query.SelectAll(typeof(StaffMember), "HeadOfYear");
            query.SelectAll(typeof(Person), "HeadOfYearPerson");
            query.SelectAll(typeof(House));
            query.SelectAll(typeof(SenStatus));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("AspNetUsers as User", "User.Id", "StudentPerson.UserId");
            query.LeftJoin("RegGroup", "RegGroup.Id", "Student.RegGroupId");
            query.LeftJoin("StaffMember as Tutor", "Tutor.Id", "RegGroup.TutorId");
            query.LeftJoin("Person as TutorPerson", "TutorPerson.Id", "Tutor.PersonId");
            query.LeftJoin("YearGroup", "YearGroup.Id", "Student.YearGroupId");
            query.LeftJoin("StaffMember as HeadOfYear", "HeadOfYear.Id", "YearGroup.HeadId");
            query.LeftJoin("Person as HeadOfYearPerson", "HeadOfYearPerson.Id", "HeadOfYear.PersonId");
            query.LeftJoin("House", "House.Id", "Student.HouseId");
            query.LeftJoin("SenStatus", "SenStatus.Id", "Student.SenStatusId");
        }

        private static void ApplySearch(Query query, StudentSearchOptions search)
        {
            if (search.Status == StudentStatus.OnRoll)
            {
                query.Where(q =>
                    q.WhereNull("Student.DateLeaving").OrWhereDate("Student.DateLeaving", ">", DateTime.Today));
            }

            else if (search.Status == StudentStatus.Leavers)
            {
                query.WhereDate("Student.DateLeaving", "<=", DateTime.Today);
            }

            else if (search.Status == StudentStatus.Future)
            {
                query.WhereDate("Student.DateStarting", ">", DateTime.Today);
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
            var query = SelectAllColumns();

            query.Where("StudentPerson.UserId", userId);

            return (await ExecuteQuery(query)).SingleOrDefault();
        }

        public async Task<IEnumerable<Student>> GetByCurriculumGroup(Guid groupId)
        {
            var query = SelectAllColumns();

            query.LeftJoin("CurriculumGroupMembership as Membership", "Membership.StudentId", "Student.Id");

            query.Where("Membership.GroupId", groupId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Student>> GetByRegGroup(Guid regGroupId)
        {
            var query = SelectAllColumns();

            query.Where("RegGroup.Id", regGroupId);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Student>> GetByYearGroup(Guid yearGroupId)
        {
            var query = SelectAllColumns();

            query.Where("YearGroup.Id", yearGroupId);

            return await ExecuteQuery(query);
        }

        public async Task<Student> GetByPersonId(Guid personId)
        {
            var query = SelectAllColumns();

            query.Where("Student.PersonId", personId);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(StudentSearchOptions searchParams)
        {
            var query = SelectAllColumns();
            
            ApplySearch(query, searchParams);
            
            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var query = SelectAllColumns();

            query.Where("Student.GiftedAndTalented", true);

            return await ExecuteQuery(query);
        }

        protected override async Task<IEnumerable<Student>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync(sql.Sql,
                new[]
                {
                    typeof(Student), typeof(Person), typeof(ApplicationUser), typeof(RegGroup), typeof(StaffMember),
                    typeof(Person), typeof(YearGroup), typeof(StaffMember), typeof(Person), typeof(House), typeof(SenStatus)
                },
                objects =>
                {
                    var student = (Student) objects[0];

                    student.Person = (Person) objects[1];
                    student.Person.User = (ApplicationUser) objects[2];
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
