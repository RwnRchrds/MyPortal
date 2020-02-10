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
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseReadWriteRepository<Student>, IStudentRepository
    {
        private readonly string RelatedColumns = "";

        private readonly string JoinRelated = "";

        public StudentRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated}";

            return await Connection.QueryAsync<Student, Person, Student>(sql, (s, p) =>
            {
                s.Person = p;
                return s;
            });
        }

        public async Task<Student> GetById(Guid id)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[Id] = @StudentId";

            return (await Connection.QueryAsync<Student, Person, Student>(sql, (s, p) =>
            {
                s.Person = p;
                return s;
            }, new {StudentId = id})).Single();
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
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [P].[UserId] = @UserId";

            return (await ExecuteQuery(sql, new {UserId = userId})).SingleOrDefault();
        }

        public async Task<IEnumerable<Student>> GetOnRoll()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[DateStarting] IS NOT NULL AND [Student].[DateStarting] <= @DateToday";
                
            return await Connection.QueryAsync<Student>(sql, new {DateToday = DateTime.Today});
        }

        public async Task<IEnumerable<Student>> GetLeavers()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[DateStarting] IS NOT NULL AND [Student].[DateLeaving] <= @DateToday";

            return await Connection.QueryAsync<Student>(sql, new { DateToday = DateTime.Today });
        }

        public async Task<IEnumerable<Student>> GetFuture()
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[DateStarting] IS NOT NULL AND [Student].[DateStarting] > @DateToday";

            return await Connection.QueryAsync<Student>(sql, new { DateToday = DateTime.Today });
        }

        public async Task<IEnumerable<Student>> GetByRegGroup(int regGroupId)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[RegGroupId] = @RegGroupId";

            return await Connection.QueryAsync<Student>(sql, new { RegGroupId = regGroupId });
        }

        public async Task<IEnumerable<Student>> GetByYearGroup(int yearGroupId)
        {
            var sql = $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[YearGroupId] = @YearGroupId";

            return await Connection.QueryAsync<Student>(sql, new { YearGroupId = yearGroupId });
        }

        public async Task<IEnumerable<Student>> GetByClass(int classId)
        {
            var sql =
                $"SELECT {AllColumns},{RelatedColumns} FROM {TblName} {JoinRelated} WHERE [Student].[Id] IN (SELECT [Enrolment].[StudentId] FROM [dbo].[Enrolment] AS [Enrolment] WHERE [ClassId] = @ClassId)";

            return await Connection.QueryAsync<Student>(sql, new { ClassId = classId });
        }

        public async Task<IEnumerable<Student>> GetGiftedTalented()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetByHouse(int houseId)
        {
            throw new NotImplementedException();
        }

        protected override async Task<IEnumerable<Student>> ExecuteQuery(string sql, object param = null)
        {
            throw new NotImplementedException();
        }
    }
}
