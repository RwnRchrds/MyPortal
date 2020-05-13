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
    public class AchievementRepository : BaseReadWriteRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(AcademicYear))},
{EntityHelper.GetAllColumns(typeof(AchievementType))},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")}
{EntityHelper.GetAllColumns(typeof(Location))},
{EntityHelper.GetAllColumns(typeof(ApplicationUser))},
{EntityHelper.GetAllColumns(typeof(Person), "RecordedByPerson")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AcademicYear]", "[AcademicYear].[Id]", "[Achievement].[AcademicYearId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AchievementType]", "[AchievementType].[Id]", "[Achievement].[AchievementTypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Achievement].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Location]", "[Location].[Id]", "[Achievement].[LocationId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[Achievement].[RecordedById]", "User")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[RecordedByPerson].[UserId]", "[User].[Id]", "RecordedByPerson")}";
        }

        public async Task<int> GetCountByStudent(int studentId, int academicYearId)
        {
            var sql = $"SELECT COUNT([Id]) FROM {TblName}";

            SqlHelper.Where(ref sql, "[Achievement].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[Achievement].[AcademicYearId] = @AcademicYearId");

            return await ExecuteIntQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }

        public async Task<int> GetPointsByStudent(int studentId, int academicYearId)
        {
            var sql = $"SELECT SUM([Points]) FROM {TblName}";

            SqlHelper.Where(ref sql, "[Achievement].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[Achievement].[AcademicYearId] = @AcademicYearId");

            return await ExecuteIntQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }

        public async Task<IEnumerable<Achievement>> GetByStudent(int studentId, int academicYearId)
        {
            var sql = SelectAllColumns(true);

            SqlHelper.Where(ref sql, "[Achievement].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[Achievement].[AcademicYearId] = @AcademicYearId");

            return await ExecuteQuery(sql, new {StudentId = studentId, AcademicYearId = academicYearId});
        }

        public async Task<int> GetPointsToday()
        {
            var sql = $"SELECT SUM([Points]) FROM {TblName}";

            SqlHelper.Where(ref sql, "[Achievement].[StudentId] = @StudentId");
            SqlHelper.Where(ref sql, "[Achievement].[AcademicYearId] = @AcademicYearId");

            return await ExecuteIntQuery(sql,
                new {DateTime.Today, Tomorrow = DateTime.Today.Date.AddDays(1)});   
        }

        protected override async Task<IEnumerable<Achievement>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync(sql,
                    new[]
                    {
                        typeof(Achievement), typeof(AchievementType), typeof(Student), typeof(Person), typeof(Location),
                        typeof(ApplicationUser), typeof(Person)
                    }, (objects) =>
                    {
                        var achievement = (Achievement) objects[0];

                        achievement.Type = (AchievementType) objects[1];
                        achievement.Student = (Student) objects[2];
                        achievement.Student.Person = (Person) objects[3];
                        achievement.Location = (Location) objects[4];
                        achievement.RecordedBy = (ApplicationUser) objects[5];
                        achievement.RecordedBy.Person = (Person) objects[6];

                        return achievement;
                    }, param);
        }
    }
}
