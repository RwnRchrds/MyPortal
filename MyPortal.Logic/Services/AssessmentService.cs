using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Assessment;
using MyPortal.Logic.Models.Data.Assessment.MarksheetEntry;

using MyPortal.Logic.Models.Requests.Assessment;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services;

public class AssessmentService : BaseUserService, IAssessmentService
{
    public AssessmentService(ICurrentUser user) : base(user)
    {
    }

    public async Task<IEnumerable<ResultModel>> GetPreviousResults(Guid studentId, Guid aspectId, DateTime dateTo)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var results = await unitOfWork.Results.GetPreviousResults(studentId, aspectId, dateTo);

        return results.Select(r => new ResultModel(r)).ToList();
    }

    public async Task<ResultModel> GetResult(Guid resultId)
    {
        await using var unitOfWork = await User.GetConnection();
        var result = await unitOfWork.Results.GetById(resultId);

        if (result == null)
        {
            throw new NotFoundException("Result not found.");
        }

        return new ResultModel(result);
    }

    public async Task<ResultModel> GetResult(Guid studentId, Guid aspectId, Guid resultSetId)
    {
        await using var unitOfWork = await User.GetConnection();
        var result = await unitOfWork.Results.GetResult(studentId, aspectId, resultSetId);

        if (result == null)
        {
            throw new NotFoundException("Result not found.");
        }

        return new ResultModel(result);
    }

    public async Task<IEnumerable<ResultModel>> GetPreviousResults(Guid resultId)
    {
        await using var unitOfWork = await User.GetConnection();
        var result = await unitOfWork.Results.GetById(resultId);

        if (result == null)
        {
            throw new NotFoundException("Result not found.");
        }

        return await GetPreviousResults(result.StudentId, result.AspectId, result.Date);
    }

    public async Task<MarksheetEntryDataModel> GetMarksheet(Guid marksheetId)
    {
        await using var unitOfWork = await User.GetConnection();
        var metadata = await unitOfWork.Marksheets.GetMarksheetDetails(marksheetId);

        if (metadata == null)
        {
            throw new NotFoundException("Marksheet not found.");
        }

        var marksheet = new MarksheetEntryDataModel();

        marksheet.Title = $"{metadata.TemplateName} ({metadata.StudentGroupCode})";

        if (metadata.OwnerId.HasValue)
        {
            marksheet.Title += $" - {metadata.OwnerName}";
        }
            
        marksheet.Completed = metadata.Completed;

        var marksheetColumns = (await unitOfWork.MarksheetColumns.GetByMarksheet(marksheetId))
            .Select(c => new MarksheetColumnModel(c)).ToList();
        var resultData = (await unitOfWork.Results.GetResultDetailsByMarksheet(marksheetId)).ToList();

        await PopulateMarksheetColumns(marksheet, marksheetColumns);
        marksheet.PopulateResults(resultData);

        return marksheet;
    }

    private async Task PopulateMarksheetColumns(MarksheetEntryDataModel marksheet, IEnumerable<MarksheetColumnModel> columnCollection)
    {
        await using var unitOfWork = await User.GetConnection();
        
        Dictionary<Guid, GradeModel[]> gradeSets = new Dictionary<Guid, GradeModel[]>();

        foreach (var columnModel in columnCollection)
        {
            if (columnModel.Aspect.GradeSetId.HasValue && !gradeSets.ContainsKey(columnModel.Aspect.GradeSetId.Value))
            {
                var grades = (await unitOfWork.Grades.GetByGradeSet(columnModel.Aspect.GradeSetId.Value))
                    .Select(g => new GradeModel(g)).ToArray();
                
                if (grades.Any())
                {
                    gradeSets.Add(columnModel.Aspect.GradeSetId.Value, grades);
                }
            }

            var column = new MarksheetColumnDataModel
            {
                Header = columnModel.Aspect.ColumnHeading,
                ResultSetId = columnModel.ResultSetId,
                ResultSetName = columnModel.ResultSet.Name,
                AspectTypeId = columnModel.Aspect.TypeId,
                Order = columnModel.DisplayOrder,
                AspectId = columnModel.AspectId,
                IsReadOnly = columnModel.ResultSet.Locked || columnModel.ReadOnly || marksheet.Completed
            };

            var aspectType = columnModel.Aspect.TypeId;

            if (aspectType == AspectTypes.Grade)
            {
                if (!columnModel.Aspect.GradeSetId.HasValue ||
                    !gradeSets.TryGetValue(columnModel.Aspect.GradeSetId.Value, out var columnGrades))
                {
                    throw new NotFoundException("Grade set not found.");
                }

                column.Grades = columnGrades;
            }
            else if (aspectType == AspectTypes.MarkDecimal || aspectType == AspectTypes.MarkInteger)
            {
                column.MinMark = columnModel.Aspect.MinMark;
                column.MaxMark = columnModel.Aspect.MaxMark;
            }
            
            marksheet.Columns.Add(column);
        }
    }

    public async Task<AspectModel> CreateAspect(AspectRequestModel model)
    {
        Validate(model);

        var aspect = new Aspect
        {
            Id = Guid.NewGuid(),
            TypeId = model.TypeId,
            GradeSetId = model.GradeSetId,
            MinMark = model.MinMark,
            MaxMark = model.MaxMark,
            Name = model.Name,
            ColumnHeading = model.ColumnHeading,
            Private = model.Private
        };

        await using var unitOfWork = await User.GetConnection();
        
        unitOfWork.Aspects.Create(aspect);

        await unitOfWork.SaveChangesAsync();

        return new AspectModel(aspect);
    }

    public async Task UpdateAspect(Guid aspectId, AspectRequestModel model)
    {
        Validate(model);
        
        await using var unitOfWork = await User.GetConnection();
        
        var aspect = await unitOfWork.Aspects.GetById(aspectId);

        if (aspect == null)
        {
            throw new EntityNotFoundException("Aspect not found.");
        }

        aspect.TypeId = model.TypeId;
        aspect.GradeSetId = model.GradeSetId;
        aspect.MinMark = model.MinMark;
        aspect.MaxMark = model.MaxMark;
        aspect.Name = model.Name;
        aspect.ColumnHeading = model.ColumnHeading;
        aspect.Private = model.Private;
    }

    public async Task DeleteAspect(Guid aspectId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var aspect = await unitOfWork.Aspects.GetById(aspectId);

        if (aspect == null)
        {
            throw new NotFoundException("Aspect not found.");
        }

        await unitOfWork.Aspects.Delete(aspectId);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task SaveResults(params ResultSummaryModel[] models)
    {
        List<Aspect> cachedAspects = new List<Aspect>();
        List<ResultSet> cachedResultSets = new List<ResultSet>();
        List<Grade> cachedGrades = new List<Grade>();
        
        await using var unitOfWork = await User.GetConnection();

        foreach (var model in models)
        {
            Validate(model);

            Aspect aspect = cachedAspects.FirstOrDefault(x => x.Id == model.AspectId);

            if (aspect == null)
            {
                aspect = await unitOfWork.Aspects.GetById(model.AspectId);

                if (aspect == null)
                {
                    throw new NotFoundException("Aspect not found.");
                }

                cachedAspects.Add(aspect);
            }

            ResultSet resultSet = cachedResultSets.FirstOrDefault(x => x.Id == model.ResultSetId);

            if (resultSet == null)
            {
                resultSet = await unitOfWork.ResultSets.GetById(model.ResultSetId);

                if (resultSet == null)
                {
                    throw new NotFoundException("Result set not found.");
                }

                cachedResultSets.Add(resultSet);
            }

            if (!resultSet.Active)
            {
                throw new LogicException("The result set is not active.");
            }

            Result result =
                await unitOfWork.Results.GetResult(model.StudentId, model.AspectId, model.ResultSetId);

            bool createNewResult = result == null;

            if (createNewResult)
            {
                result = new Result
                {
                    Id = Guid.NewGuid(),
                    StudentId = model.StudentId,
                    AspectId = model.AspectId,
                    ResultSetId = model.ResultSetId,
                    ColourCode = model.ColourCode,
                    Note = model.Note,
                    Date = DateTime.Now
                };
            }
            else
            {
                result.ColourCode = model.ColourCode;
                result.Note = model.Note;

                // To be discussed, a new result should not be created as this result is just being amended
                // but does the date need to be updated?
                // ^ Do we need audit history to see who changed the result (and reason why)?
                result.Date = DateTime.Now;
            }

            if (aspect.TypeId == AspectTypes.Grade)
            {
                if (!model.GradeId.HasValue)
                {
                    throw new InvalidDataException("A grade was not provided.");
                }

                Grade grade = null;

                grade = cachedGrades.FirstOrDefault(x => x.Id == model.GradeId);

                if (grade == null)
                {
                    grade = await unitOfWork.Grades.GetById(model.GradeId.Value);

                    if (grade == null)
                    {
                        throw new NotFoundException("Grade not found.");
                    }

                    cachedGrades.Add(grade);
                }

                result.GradeId = model.GradeId.Value;
            }

            if (aspect.TypeId == AspectTypes.MarkDecimal || aspect.TypeId == AspectTypes.MarkInteger)
            {
                if (!model.Mark.HasValue)
                {
                    throw new InvalidDataException("A numeric mark was not provided.");
                }

                result.Mark = aspect.TypeId == AspectTypes.MarkInteger
                    ? Convert.ToInt32(model.Mark.Value)
                    : model.Mark.Value;
            }

            if (aspect.TypeId == AspectTypes.Comment)
            {
                if (!string.IsNullOrWhiteSpace(result.Comment))
                {
                    throw new InvalidDataException("A comment was not provided.");
                }

                result.Comment = model.Comment;
            }

            if (createNewResult)
            {
                unitOfWork.Results.Create(result);
            }
            else
            {
                await unitOfWork.Results.Update(result);
            }
        }

        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteResult(Guid resultId)
    {
        await using var unitOfWork = await User.GetConnection();
        
        var result = await unitOfWork.Results.GetById(resultId);

        if (result == null)
        {
            throw new NotFoundException("Result not found.");
        }

        await unitOfWork.Results.Delete(resultId);

        await unitOfWork.SaveChangesAsync();
    }
}