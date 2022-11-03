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

namespace MyPortal.Database.Repositories;

public class StudentAchievementRepository : BaseReadWriteRepository<StudentAchievement>, IStudentAchievementRepository
{
    public StudentAchievementRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
        query.LeftJoin("Achievements as A", "A.Id", $"{TblAlias}.AchievementId");
        query.LeftJoin("AchievementOutcomes as AO", "AO.Id", $"{TblAlias}.OutcomeId");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(Student), "S");
        query.SelectAllColumns(typeof(Achievement), "A");
        query.SelectAllColumns(typeof(AchievementOutcome), "AO");

        return query;
    }

    protected override async Task<IEnumerable<StudentAchievement>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var studentAchievements =
            await Transaction.Connection
                .QueryAsync<StudentAchievement, Student, Achievement, AchievementOutcome, StudentAchievement>(sql.Sql,
                    (sa, student, achievement, outcome) =>
                    {
                        sa.Student = student;
                        sa.Achievement = achievement;
                        sa.Outcome = outcome;

                        return sa;
                    }, sql.NamedBindings, Transaction);

        return studentAchievements;
    }
    
    public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
    {
        var sql = GenerateEmptyQuery().AsCount();

        sql.Where($"{TblAlias}.StudentId", "=", studentId);
        sql.Where("A.AcademicYearId", "=", academicYearId);

        return await ExecuteQueryIntResult(sql) ?? 0;
    }

    public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
    {
        var sql = GenerateEmptyQuery().AsSum($"{TblAlias}.Points");
        sql.Where($"{TblAlias}.StudentId", studentId);
        sql.Where("A.AcademicYearId", academicYearId);

        return await ExecuteQueryIntResult(sql) ?? 0;
    }

    public async Task<IEnumerable<StudentAchievement>> GetByStudent(Guid studentId, Guid academicYearId)
    {
        var query = GenerateQuery();

        query.Where($"{TblAlias}.StudentId" ,"=", studentId);
        query.Where("A.AcademicYearId", "=", academicYearId);

        return await ExecuteQuery(query);
    }

    public async Task<int> GetPointsToday()
    {
        var sql = GenerateEmptyQuery().AsSum($"{TblAlias}.Points");

        var dateToday = DateTime.Today;

        sql.WhereDatePart("day", "A.CreatedDate", "=", dateToday.Day);

        sql.WhereDatePart("month", "A.CreatedDate", "=", dateToday.Month);

        sql.WhereDatePart("year", "A.CreatedDate", "=", dateToday.Year);

        return await ExecuteQueryIntResult(sql) ?? 0;
    }

    public async Task Update(StudentAchievement entity)
    {
        var sa = await Context.StudentAchievements.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (sa == null)
        {
            throw new EntityNotFoundException("Student achievement not found");
        }

        sa.OutcomeId = entity.OutcomeId;
        sa.Points = entity.Points;
    }
}