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
using Task = System.Threading.Tasks.Task;

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
{EntityHelper.GetAllColumns(typeof(Person), "TutorPerson")}
{EntityHelper.GetAllColumns(typeof(YearGroup))},
{EntityHelper.GetAllColumns(typeof(House))},
{EntityHelper.GetAllColumns(typeof(SenStatus))}";

        JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[StudentPerson].[UserId]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[RegGroup]", "[RegGroup].[Id]", "[Student].[RegGroupId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[RegGroup].[TutorId]", "Tutor")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[Tutor].[PersonId]", "TutorPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[YearGroup]", "[YearGroup].[Id]", "[Student].[YearGroupId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[House]", "[House].[Id]", "[Student].[HouseId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[SenStatus]", "[SenStatus].[Id]", "[Student].[SenStatusId]")}";
        }

        private static void ApplySearch(ref string sql, Student student)
        {
            if (!string.IsNullOrWhiteSpace(student.Person.FirstName))
            {
                SqlHelper.Where(ref sql, "[Person].[FirstName] LIKE @FirstName");
            }

            if (!string.IsNullOrWhiteSpace(student.Person.LastName))
            {
                SqlHelper.Where(ref sql, "[Person].[LastName] LIKE @LastName");
            }

            if (!string.IsNullOrWhiteSpace(student.Person.Gender))
            {
                SqlHelper.Where(ref sql, "[Person].[Gender] = @Gender");
            }

            if (student.Person.Dob != null)
            {
                SqlHelper.Where(ref sql, "[Person].[Dob] = @Dob");
            }

            if (student.RegGroupId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[RegGroupId] = @RegGroupId");
            }

            if (student.YearGroupId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[YearGroupId] = @YearGroupId");
            }

            if (student.HouseId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[HouseId] = @HouseId");
            }

            if (student.SenStatusId != Guid.Empty)
            {
                SqlHelper.Where(ref sql, "[Student].[SenStatusId] = @SenStatusId");
            }
        }

        public async Task Update(Student entity)
        {
            var studentInDb = await Context.Students.FindAsync(entity.Id);

            if (studentInDb == null)
            {
                throw new Exception("Student not found.");
            }

            studentInDb.RegGroupId = entity.RegGroupId;
            studentInDb.YearGroupId = entity.YearGroupId;
            studentInDb.HouseId = entity.HouseId;
            studentInDb.CandidateNumber = entity.CandidateNumber;
            studentInDb.AdmissionNumber = entity.AdmissionNumber;
            studentInDb.DateStarting = entity.DateStarting;
            studentInDb.DateLeaving = entity.DateLeaving;
            studentInDb.FreeSchoolMeals = entity.FreeSchoolMeals;
            studentInDb.GiftedAndTalented = entity.GiftedAndTalented;
            studentInDb.SenStatusId = entity.SenStatusId;
            studentInDb.PupilPremium = entity.PupilPremium;
            studentInDb.Upn = entity.Upn;
            studentInDb.Uci = entity.Uci;
            studentInDb.Deleted = entity.Deleted;
        }

        public async Task<Student> GetByUserId(string userId)
        {
            var sql = SelectAllColumns();
            
            SqlHelper.Where(ref sql, "[StudentPerson].[UserId] = @UserId");

            return (await ExecuteQuery(sql, new {UserId = userId})).SingleOrDefault();
        }

        public async Task<IEnumerable<Student>> GetAll(Student student)
        {
            var sql = SelectAllColumns();
            
            ApplySearch(ref sql, student);
            
            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(student.Person.FirstName)}",
                    LastName = $"{SqlHelper.ParamStartsWith(student.Person.LastName)}", student.Person.Gender,
                    student.Person.Dob, student.RegGroupId, student.YearGroupId, student.HouseId, student.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetOnRoll(Student student)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "([Student].[DateLeaving] IS NULL OR [Student.DateLeaving] > @DateToday)");
            SqlHelper.Where(ref sql, "[Student].[DateStarting] <= @DateToday");
            
            ApplySearch(ref sql, student);

            return await ExecuteQuery(sql,
                new
                {
                    DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(student.Person.FirstName)}",
                    LastName = $"{SqlHelper.ParamStartsWith(student.Person.LastName)}", student.Person.Gender,
                    student.Person.Dob, student.RegGroupId, student.YearGroupId, student.HouseId, student.SenStatusId
                });
        }

        public async Task<IEnumerable<Student>> GetLeavers(Student student)
        {
            var sql = SelectAllColumns();

            SqlHelper.Where(ref sql, "[Student].[DateStarting] IS NOT NULL");
            SqlHelper.Where(ref sql, "[Student].[DateLeaving] <= @DateToday");
            
            ApplySearch(ref sql, student);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(student.Person.FirstName)}",
                LastName = $"{SqlHelper.ParamStartsWith(student.Person.LastName)}", student.Person.Gender,
                student.Person.Dob, student.RegGroupId, student.YearGroupId, student.HouseId, student.SenStatusId
            });
        }

        public async Task<IEnumerable<Student>> GetFuture(Student student)
        {
            var sql = SelectAllColumns();
            
            SqlHelper.Where(ref sql, "[Student].[DateStarting] > @DateToday");
            
            ApplySearch(ref sql, student);

            return await ExecuteQuery(sql, new
            {
                DateToday = DateTime.Today, FirstName = $"{SqlHelper.ParamStartsWith(student.Person.FirstName)}",
                LastName = $"{SqlHelper.ParamStartsWith(student.Person.LastName)}", student.Person.Gender,
                student.Person.Dob, student.RegGroupId, student.YearGroupId, student.HouseId, student.SenStatusId
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
                    typeof(Person), typeof(YearGroup), typeof(House), typeof(SenStatus)
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
                    student.House = (House) objects[7];
                    student.SenStatus = (SenStatus) objects[8];

                    return student;
                }, param);
        }
    }
}
