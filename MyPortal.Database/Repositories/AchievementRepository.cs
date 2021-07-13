using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AchievementRepository : BaseReadWriteRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
           
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AchievementTypes as AT", "AT.Id", $"{TblAlias}.AchievementTypeId");
            query.LeftJoin("Locations as L", "L.Id", $"{TblAlias}.LocationId");
            query.LeftJoin("AcademicYears as AY", "AY.Id", $"{TblAlias}.AcademicYearId");
            query.LeftJoin("Users as U", "U.Id", $"{TblAlias}.CreatedById");
            query.LeftJoin("Student as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("AchievementOutcomes as AO", "AO.Id", $"{TblAlias}.OutcomeId");
            
            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AchievementType), "AT");
            query.SelectAllColumns(typeof(Location), "L");
            query.SelectAllColumns(typeof(AcademicYear), "AY");
            query.SelectAllColumns(typeof(User), "U");
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(AchievementOutcome), "AO");

            return query;
        }

        protected override async Task<IEnumerable<Achievement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var achievements = await Transaction.Connection
                .QueryAsync<Achievement, AchievementType, Location, AcademicYear, User, Student, Achievement>(
                    sql.Sql,
                    (achievement, type, location, year, user, student) =>
                    {
                        achievement.Type = type;
                        achievement.Location = location;
                        achievement.AcademicYear = year;
                        achievement.CreatedBy = user;
                        achievement.Student = student;

                        return achievement;
                    }, sql.NamedBindings, Transaction);

            return achievements;
        }

        public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = GenerateEmptyQuery().AsCount();

            sql.Where($"{TblAlias}.StudentId", "=", studentId);
            sql.Where($"{TblAlias}.AcademicYearId", "=", academicYearId);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
        {
            var sql = GenerateEmptyQuery().AsSum("Achievement.Points");
            sql.Where($"{TblAlias}.StudentId", studentId);
            sql.Where($"{TblAlias}.AcademicYearId", academicYearId);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        public async Task<IEnumerable<Achievement>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId" ,"=", studentId);
            query.Where($"{TblAlias}.AcademicYearId", "=", academicYearId);

            return await ExecuteQuery(query);
        }

        public async Task<int> GetPointsToday()
        {
            var sql = GenerateEmptyQuery().AsSum($"{TblAlias}.Points");

            var dateToday = DateTime.Today;

            sql.WhereDatePart("day", $"{TblAlias}.CreatedDate", "=", dateToday.Day);

            sql.WhereDatePart("month", $"{TblAlias}.CreatedDate", "=", dateToday.Month);

            sql.WhereDatePart("year", $"{TblAlias}.CreatedDate", "=", dateToday.Year);

            return await ExecuteQueryIntResult(sql) ?? 0;
        }

        public async Task Update(Achievement entity)
        {
            var achievement = await Context.Achievements.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (achievement == null)
            {
                throw new EntityNotFoundException("Achievement not found.");
            }

            achievement.AchievementTypeId = entity.AchievementTypeId;
            achievement.Comments = entity.Comments;
            achievement.LocationId = entity.LocationId;
            achievement.OutcomeId = entity.OutcomeId;
            achievement.Points = entity.Points;
        }
    }
}
