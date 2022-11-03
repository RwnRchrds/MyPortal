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

public class StudentIncidentRepository : BaseReadWriteRepository<StudentIncident>, IStudentIncidentRepository
{
    public StudentIncidentRepository(ApplicationDbContext context, DbTransaction transaction, string tblAlias = null) : base(context, transaction, tblAlias)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
        query.LeftJoin("Incidents as I", "I.Id", $"{TblAlias}.IncidentId");
        query.LeftJoin("BehaviourRoleTypes as BRT", "BRT.Id", $"{TblAlias}.RoleTypeId");
        query.LeftJoin("BehaviourOutcomes as BO", "BO.Id", $"{TblAlias}.OutcomeId");
        query.LeftJoin("BehaviourStatus as BS", "BS.Id", $"{TblAlias}.StatusId");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(Student), "S");
        query.SelectAllColumns(typeof(Incident), "I");
        query.SelectAllColumns(typeof(BehaviourRoleType), "BRT");
        query.SelectAllColumns(typeof(BehaviourOutcome), "BO");
        query.SelectAllColumns(typeof(BehaviourStatus), "BS");

        return query;
    }

    protected override async Task<IEnumerable<StudentIncident>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var studentIncidents =
            await Transaction.Connection
                .QueryAsync<StudentIncident, Student, Incident, BehaviourRoleType, BehaviourOutcome, BehaviourStatus,
                    StudentIncident>(sql.Sql,
                    (studentIncident, student, incident, roleType, outcome, status) =>
                    {
                        studentIncident.Student = student;
                        studentIncident.Incident = incident;
                        studentIncident.RoleType = roleType;
                        studentIncident.Outcome = outcome;
                        studentIncident.Status = status;

                        return studentIncident;
                    }, sql.NamedBindings, Transaction);

        return studentIncidents;
    }
    
    public async Task<IEnumerable<StudentIncident>> GetByStudent(Guid studentId, Guid academicYearId)
    {
        var query = GenerateQuery();

        query.Where("S.Id", studentId);
        query.Where("I.AcademicYearId", academicYearId);
    
        return await ExecuteQuery(query);
    }

    public async Task<IEnumerable<StudentIncident>> GetByIncident(Guid incidentId)
    {
        var query = GenerateQuery();

        query.Where("I.Id", incidentId);

        return await ExecuteQuery(query);
    }

    public async Task<int> GetCountByStudent(Guid studentId, Guid academicYearId)
    {
        var query = GenerateQuery().AsCount();
    
        query.Where($"{TblAlias}.StudentId", studentId);
        query.Where("I.AcademicYearId", academicYearId);
    
        return await ExecuteQueryIntResult(query) ?? 0;
    }

    public async Task<int> GetCountByIncident(Guid incidentId)
    {
        var query = GenerateQuery().AsCount();

        query.Where("I.Id", incidentId);

        return await ExecuteQueryIntResult(query) ?? 0;
    }

    public async Task<int> GetPointsByStudent(Guid studentId, Guid academicYearId)
    {
        var query = GenerateEmptyQuery().AsSum($"{TblAlias}.Points");
    
        query.Where("I.StudentId", studentId);
        query.Where("I.AcademicYearId", academicYearId);
    
        return await ExecuteQueryIntResult(query) ?? 0;
    }

    public async Task Update(StudentIncident entity)
    {
        var studentIncident = await Context.StudentIncidents.FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (studentIncident == null)
        {
            throw new EntityNotFoundException("Student incident not found.");
        }

        studentIncident.RoleTypeId = entity.RoleTypeId;
        studentIncident.OutcomeId = entity.OutcomeId;
        studentIncident.StatusId = entity.StatusId;
        studentIncident.Points = entity.Points;
    }
}