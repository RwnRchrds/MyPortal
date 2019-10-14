using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class AssessmentProcesses
    {
        public static async Task CreateResult(AssessmentResult result, MyPortalDbContext context, bool commitImmediately = true)
        {
            if (!ValidationProcesses.ModelIsValid(result))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (! await context.AssessmentGrades.AnyAsync(x => x.GradeSetId == result.Aspect.GradeSetId && x.Code == result.Grade))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Grade does not exist");
            }

            var resultInDb = await context.AssessmentResults.SingleOrDefaultAsync(x =>
                x.StudentId == result.StudentId && x.AspectId == result.AspectId && x.ResultSetId == result.ResultSetId);

            if (resultInDb != null)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Result already exists");
            }

            context.AssessmentResults.Add(result);

            if (commitImmediately)
            {
                await context.SaveChangesAsync();
            }
        }

        public static async Task CreateResultSet(AssessmentResultSet resultSet, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(resultSet))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var currentRsExists = await context.AssessmentResultSets.AnyAsync(x => x.IsCurrent);

            if (!currentRsExists)
            {
                resultSet.IsCurrent = true;
            }

            context.AssessmentResultSets.Add(resultSet);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteResultSet(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = await context.AssessmentResultSets.SingleOrDefaultAsync(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Result set is marked as current");
            }

            context.AssessmentResultSets.Remove(resultSet);
            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<AssessmentResultSetDto>> GetAllResultSets(MyPortalDbContext context)
        {
            var resultSets = await GetAllResultSetsModel(context);

            return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
        }

        public static async Task<IEnumerable<GridAssessmentResultSetDto>> GetAllResultSetsDataGrid(MyPortalDbContext context)
        {
            var resultSets = await GetAllResultSetsModel(context);

            return resultSets.Select(Mapper.Map<AssessmentResultSet, GridAssessmentResultSetDto>);
        }

        public static async Task<IEnumerable<AssessmentResultSet>> GetAllResultSetsModel(MyPortalDbContext context)
        {
            return await context.AssessmentResultSets.OrderBy(x => x.Name).ToListAsync();
        }

        public static async Task<AssessmentResultDto> GetResultById(int resultId, MyPortalDbContext context)
        {
            var result = await context.AssessmentResults.SingleOrDefaultAsync(x => x.Id == resultId);

            return Mapper.Map<AssessmentResult, AssessmentResultDto>(result);
        }

        public static async Task<IEnumerable<AssessmentResult>> GetResultsByStudentModel(int studentId, int resultSetId, MyPortalDbContext context)
        {
            var results = await context.AssessmentResults
                .Where(x => x.StudentId == studentId && x.ResultSetId == resultSetId).ToListAsync();

            return results;
        }

        public static async Task<IEnumerable<AssessmentResultDto>> GetResultsByStudent(int studentId, int resultSetId,
            MyPortalDbContext context)
        {
            var results = await GetResultsByStudentModel(studentId, resultSetId, context);

            return results.Select(Mapper.Map<AssessmentResult, AssessmentResultDto>);
        }

        public static async Task<IEnumerable<GridAssessmentResultDto>> GetResultsByStudentDataGrid(int studentId, int resultSetId,
            MyPortalDbContext context)
        {
            var results = await GetResultsByStudentModel(studentId, resultSetId, context);

            return results.Select(Mapper.Map<AssessmentResult, GridAssessmentResultDto>);
        }

        public static async Task<AssessmentResultSetDto> GetResultSetById(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = await GetResultSetByIdModel(resultSetId, context);

            return Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>(resultSet);
        }

        public static async Task<AssessmentResultSet> GetResultSetByIdModel(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = await context.AssessmentResultSets.SingleOrDefaultAsync(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            return resultSet;
        }

        public static async Task<IEnumerable<AssessmentResultSetDto>> GetResultSetsByStudent(int studentId,
            MyPortalDbContext context)
        {
            var resultSets = await GetResultSetsByStudentModel(studentId, context);

            return resultSets.Select(Mapper.Map<AssessmentResultSet, AssessmentResultSetDto>);
        }

        public static async Task<IEnumerable<AssessmentResultSet>> GetResultSetsByStudentModel(int studentId,
            MyPortalDbContext context)
        {
            if (!await context.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            var resultSets = await context.AssessmentResultSets.Where(x => x.Results.Any(s => s.StudentId == studentId))
                .ToListAsync();

            return resultSets;
        }

        public static async Task<bool> ResultSetContainsResults(int resultSetId, MyPortalDbContext context)
        {
            if (!await context.AssessmentResultSets.AnyAsync(x => x.Id == resultSetId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            return await context.AssessmentResults.AnyAsync(x => x.ResultSetId == resultSetId);
        }

        public static async Task SetResultSetAsCurrent(int resultSetId, MyPortalDbContext context)
        {
            var resultSet = await context.AssessmentResultSets.SingleOrDefaultAsync(x => x.Id == resultSetId);

            if (resultSet == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            if (resultSet.IsCurrent)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Result set is already marked as current");
            }

            var currentResultSet = await context.AssessmentResultSets.SingleOrDefaultAsync(x => x.IsCurrent);

            if (currentResultSet != null)
            {
                currentResultSet.IsCurrent = false;
            }

            resultSet.IsCurrent = true;

            await context.SaveChangesAsync();
        }

        public static async Task UpdateResultSet(AssessmentResultSet resultSet, MyPortalDbContext context)
        {
            var resultSetInDb = await context.AssessmentResultSets.SingleOrDefaultAsync(x => x.Id == resultSet.Id);

            if (resultSetInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Result set not found");
            }

            resultSetInDb.Name = resultSet.Name;

            await context.SaveChangesAsync();
        }
    }
}