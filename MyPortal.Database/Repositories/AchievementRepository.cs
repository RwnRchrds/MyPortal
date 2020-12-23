using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories
{
    public class AchievementRepository : BaseReadWriteRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(ApplicationDbContext context) : base(context, "Achievement")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AcademicYear), "AcademicYear");
            query.SelectAllColumns(typeof(AchievementType), "AchievementType");
            query.SelectAllColumns(typeof(AchievementOutcome), "AchievementOutcome");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(Location), "Location");
            query.SelectAllColumns(typeof(User), "RecordedBy");
            query.SelectAllColumns(typeof(Person), "RecordedByPerson");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin($"AcademicYears as AcademicYear", "AcademicYear.Id", "Achievement.AcademicYearId");
            query.LeftJoin($"AchievementTypes as AchievementType", "AchievementType.Id", "Achievement.AchievementTypeId");
            query.LeftJoin($"AchievementOutcomes as AchievementOutcome", "AchievementOutcome.Id", "Achievement.OutcomeId");
            query.LeftJoin($"Students as Student", "Student.Id", "Achievement.StudentId");
            query.LeftJoin($"People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin($"Locations as Location", "Location.Id", "Achievement.LocationId");
            query.LeftJoin($"AspNetUsers as RecordedBy", "RecordedBy.Id", "Achievement.RecordedById");
            query.LeftJoin($"People as RecordedByPerson", "RecordedByPerson.UserId", "RecordedBy.Id");
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = new Query(TblName).AsCount();

            sql.Where("Achievement.StudentId", "=", studentId);
            sql.Where("Achievement.AcademicYearId", "=", academicYearId);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = new Query(TblName).AsSum("Achievement.Points");

            sql.Where("Achievement.StudentId", "=", studentId);
            sql.Where("Achievement.AcademicYearId", "=", academicYearId);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        public async Task<IEnumerable<Achievement>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = GenerateQuery();

            sql.Where("Achievement.StudentId" ,"=", studentId);
            sql.Where("Achievement.AcademicYearId", "=", academicYearId);

            return await ExecuteQuery(sql);
        }

        public async Task<int> GetPointsToday()
        {
            var sql = new Query(TblName).AsSum("Achievement.Points");

            var dateToday = DateTime.Today;

            sql.WhereDatePart("day", "Achievement.CreatedDate", "=", dateToday.Day);

            sql.WhereDatePart("month", "Achievement.CreatedDate", "=", dateToday.Month);

            sql.WhereDatePart("year", "Achievement.CreatedDate", "=", dateToday.Year);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        protected override async Task<IEnumerable<Achievement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync(sql.Sql,
                    new[]
                    {
                        typeof(Achievement), typeof(AcademicYear), typeof(AchievementType), typeof(AchievementOutcome), typeof(Student), typeof(Person), typeof(Location),
                        typeof(User), typeof(Person)
                    }, (objects) =>
                    {
                        var achievement = (Achievement) objects[0];

                        achievement.AcademicYear = (AcademicYear) objects[1];
                        achievement.Type = (AchievementType) objects[2];
                        achievement.Outcome = (AchievementOutcome) objects[3];
                        achievement.Student = (Student) objects[4];
                        achievement.Student.Person = (Person) objects[5];
                        achievement.Location = (Location) objects[6];
                        achievement.RecordedBy = (User) objects[7];
                        achievement.RecordedBy.Person = (Person) objects[8];

                        return achievement;
                    }, sql.NamedBindings);
        }
    }
}
