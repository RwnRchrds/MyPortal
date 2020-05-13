using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")},
{EntityHelper.GetUserColumns("User")},
{EntityHelper.GetAllColumns(typeof(RegGroup))},
{EntityHelper.GetAllColumns(typeof(StaffMember), "Tutor")},
{EntityHelper.GetAllColumns(typeof(Person), "TutorPerson")},
{EntityHelper.GetAllColumns(typeof(YearGroup))},
{EntityHelper.GetAllColumns(typeof(StaffMember), "HeadOfYear")},
{EntityHelper.GetAllColumns(typeof(Person), "HeadOfYearPerson")},
{EntityHelper.GetAllColumns(typeof(House))},
{EntityHelper.GetAllColumns(typeof(SenStatus))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[StudentPerson].[UserId]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[RegGroup]", "[RegGroup].[Id]", "[Student].[RegGroupId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Tutor].[Id]", "[RegGroup].[TutorId]", "Tutor")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[TutorPerson].[Id]", "[Tutor].[PersonId]", "TutorPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[Student].[YearGroupId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[HeadOfYear].[Id]", "[YearGroup].[HeadId]", "HeadOfYear")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[HeadOfYearPerson].[Id]", "[HeadOfYear].[PersonId]", "HeadOfYearPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[House]", "[House].[Id]", "[Student].[HouseId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SenStatus]", "[SenStatus].[Id]", "[Student].[SenStatusId]")}";
        }

        private static void ApplySearch(ref string sql, Student student)
        {
            if (!string.IsNullOrWhiteSpace(student.Person.FirstName))
            {
                SqlHelper.Where(ref sql, "[StudentPerson].[FirstName] LIKE @FirstName");
            }

            if (!string.IsNullOrWhiteSpace(student.Person.LastName))
            {
                SqlHelper.Where(ref sql, "[StudentPerson].[LastName] LIKE @LastName");
            }

            if (!string.IsNullOrWhiteSpace(student.Person.Gender))
            {
                SqlHelper.Where(ref sql, "[StudentPerson].[Gender] = @Gender");
            }

            if (student.Person.Dob != null && student.Person.Dob != new DateTime())
            {
                SqlHelper.Where(ref sql, "[StudentPerson].[Dob] = @Dob");
            }

            if (student.RegGroupId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[RegGroupId] = @RegGroupId");
            }

            if (student.YearGroupId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[YearGroupId] = @YearGroupId");
            }

            if (student.HouseId != null && student.HouseId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[HouseId] = @HouseId");
            }

            if (student.SenStatusId != null && student.SenStatusId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[SenStatusId] = @SenStatusId");
            }
        }

        public async Task<Student> GetByUserId(string userId)
        {
            var sql = SelectAllColumns();
            
            SqlHelper.Where(ref sql, "[StudentPerson].[UserId] = @UserId");

            return (await ExecuteQuery(sql, new {UserId = userId})).SingleOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(Student searchParams)
        {
            var sql = SelectAllColumns();
            
            ApplySearch(ref sql, searchParams);
            
            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                    LastName = $"{SqlHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                    searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetOnRoll(Student searchParams)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "([Student].[DateLeaving] IS NULL OR [Student].[DateLeaving] > @DateToday)");
            SqlHelper.Where(ref sql, "[Student].[DateStarting] <= @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                    LastName = $"{SqlHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                    searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetLeavers(Student searchParams)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Student].[DateStarting] IS NOT NULL");
            SqlHelper.Where(ref sql, "[Student].[DateLeaving] <= @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                LastName = $"{SqlHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
            });
        }

        public async Task<IEnumerable<Student>> GetFuture(Student searchParams)
        {
            var sql = SelectAllColumns();
            
            SqlHelper.Where(ref sql, "[Student].[DateStarting] > @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                LastName = $"{SqlHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
            });
        }

        public async Task<IEnumerable<Student>> GetByClass(int classId)
        {
            var sql =
                $"{SelectAllColumns()} {SqlHelper.Join(JoinType.InnerJoin, "[dbo].[Class]", "[Class].[Id]", "@ClassId")}";

            return await ExecuteQuery(sql, new { ClassId = classId });
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Student].[GiftedAndTalented] = 1");

            return await ExecuteQuery(sql);
        }

        protected override async Task<IEnumerable<Student>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync(sql,
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
                }, param);
        }
    }
}
