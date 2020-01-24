using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private const string TblName = "[dbo].[Student] AS [S]";

        public const string AllColumns =
            @"[S].[Id],[S].[PersonId],[S].[RegGroupId],[S].[YearGroupId],[S].[HouseId],[S].[CandidateNumber],[S].[AdmissionNumber],[S].[DateStarting],[S].[DateLeaving],[S].[AccountBalance],[S].[FreeSchoolMeals],[S].[GiftedAndTalented],[S].[SenStatusId],[S].[PupilPremium],[S].[Upn],[S].[Uci],[S].[Deleted]";

        private const string JoinPeople = @"JOIN [dbo].[Person] AS [P] ON [P].[Id] = [S].[PersonId]";

        private const string JoinEnrolments = @"LEFT JOIN [dbo].[Enrolment] AS [E] ON [E].[StudentId] = [S].[Id]";

        public StudentRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople}";

            return await Connection.QueryAsync<Student, Person, Student>(sql, (s, p) =>
            {
                s.Person = p;
                return s;
            });
        }

        public async Task<Student> GetById(int id)
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[Id] = @StudentId";

            return (await Connection.QueryAsync<Student, Person, Student>(sql, (s, p) =>
            {
                s.Person = p;
                return s;
            }, new {StudentId = id})).Single();
        }

        public void Create(Student entity)
        {
            Context.Students.Add(entity);
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

        public async Task Delete(int id)
        {
            var studentInDb = await Context.Students.FindAsync(id);

            if (studentInDb == null)
            {
                throw new Exception("Student not found.");
            }

            Context.Students.Remove(studentInDb);
        }

        public async Task<Student> GetByUserId(string userId)
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [P].[UserId] = @UserId";

            return await Connection.QuerySingleOrDefaultAsync<Student>(sql, new {UserId = userId});
        }

        public async Task<IEnumerable<Student>> GetOnRoll()
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[DateStarting] IS NOT NULL AND [S].[DateStarting] <= @DateToday";
                
            return await Connection.QueryAsync<Student>(sql, new {DateToday = DateTime.Today});
        }

        public async Task<IEnumerable<Student>> GetLeavers()
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[DateStarting] IS NOT NULL AND [S].[DateLeaving] <= @DateToday";

            return await Connection.QueryAsync<Student>(sql, new { DateToday = DateTime.Today });
        }

        public async Task<IEnumerable<Student>> GetFuture()
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[DateStarting] IS NOT NULL AND [S].[DateStarting] > @DateToday";

            return await Connection.QueryAsync<Student>(sql, new { DateToday = DateTime.Today });
        }

        public async Task<IEnumerable<Student>> GetByRegGroup(int regGroupId)
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[RegGroupId] = @RegGroupId";

            return await Connection.QueryAsync<Student>(sql, new { RegGroupId = regGroupId });
        }

        public async Task<IEnumerable<Student>> GetByYearGroup(int yearGroupId)
        {
            var sql = $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[YearGroupId] = @YearGroupId";

            return await Connection.QueryAsync<Student>(sql, new { YearGroupId = yearGroupId });
        }

        public async Task<IEnumerable<Student>> GetByClass(int classId)
        {
            var sql =
                $"SELECT {AllColumns},{PersonRepository.AllColumns} FROM {TblName} {JoinPeople} WHERE [S].[Id] IN (SELECT [E].[StudentId] FROM [dbo].[Enrolment] AS [E] WHERE [ClassId] = @ClassId)";

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
    }
}
