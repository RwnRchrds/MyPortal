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
using MyPortal.Database.Search;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        public StudentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Person), "StudentPerson")},
{EntityHelper.GetUserProperties("User")},
{EntityHelper.GetPropertyNames(typeof(RegGroup))},
{EntityHelper.GetPropertyNames(typeof(StaffMember), "Tutor")},
{EntityHelper.GetPropertyNames(typeof(Person), "TutorPerson")},
{EntityHelper.GetPropertyNames(typeof(YearGroup))},
{EntityHelper.GetPropertyNames(typeof(StaffMember), "HeadOfYear")},
{EntityHelper.GetPropertyNames(typeof(Person), "HeadOfYearPerson")},
{EntityHelper.GetPropertyNames(typeof(House))},
{EntityHelper.GetPropertyNames(typeof(SenStatus))}";

        (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[StudentPerson].[UserId]", "User")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[RegGroup]", "[RegGroup].[Id]", "[Student].[RegGroupId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[Tutor].[Id]", "[RegGroup].[TutorId]", "Tutor")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[TutorPerson].[Id]", "[Tutor].[PersonId]", "TutorPerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[Student].[YearGroupId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[HeadOfYear].[Id]", "[YearGroup].[HeadId]", "HeadOfYear")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[HeadOfYearPerson].[Id]", "[HeadOfYear].[PersonId]", "HeadOfYearPerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[House]", "[House].[Id]", "[Student].[HouseId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[SenStatus]", "[SenStatus].[Id]", "[Student].[SenStatusId]")}";
        }

        private static void ApplySearch(Query query, StudentSearch search)
        {
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
            var sql = SelectAllColumns();
            
            QueryHelper.Where(ref sql, "[StudentPerson].[UserId] = @UserId");

            return (await ExecuteQuery(sql, new {UserId = userId})).SingleOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(StudentSearch searchParams)
        {
            var sql = SelectAllColumns();
            
            ApplySearch(ref sql, searchParams);
            
            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{QueryHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                    LastName = $"{QueryHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                    searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetOnRoll(Student searchParams)
        {
            var sql = SelectAllColumns();

            QueryHelper.Where(ref sql, "([Student].[DateLeaving] IS NULL OR [Student].[DateLeaving] > @DateToday)");
            QueryHelper.Where(ref sql, "[Student].[DateStarting] <= @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{QueryHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                    LastName = $"{QueryHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                    searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetLeavers(Student searchParams)
        {
            var sql = SelectAllColumns();

            QueryHelper.Where(ref sql, "[Student].[DateStarting] IS NOT NULL");
            QueryHelper.Where(ref sql, "[Student].[DateLeaving] <= @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{QueryHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                LastName = $"{QueryHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
            });
        }

        public async Task<IEnumerable<Student>> GetFuture(Student searchParams)
        {
            var sql = SelectAllColumns();
            
            QueryHelper.Where(ref sql, "[Student].[DateStarting] > @DateToday");
            
            ApplySearch(ref sql, searchParams);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{QueryHelper.ParamStartsWith(searchParams.Person.FirstName)}",
                LastName = $"{QueryHelper.ParamStartsWith(searchParams.Person.LastName)}", searchParams.Person.Gender,
                searchParams.Person.Dob, searchParams.RegGroupId, searchParams.YearGroupId, searchParams.HouseId, searchParams.SenStatusId
            });
        }

        public async Task<IEnumerable<Student>> GetByClass(int classId)
        {
            var sql =
                $"{SelectAllColumns()} {QueryHelper.Join(JoinType.InnerJoin, "[dbo].[Class]", "[Class].[Id]", "@ClassId")}";

            return await ExecuteQuery(sql, new { ClassId = classId });
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            var sql = SelectAllColumns();

            QueryHelper.Where(ref sql, "[Student].[GiftedAndTalented] = 1");

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
