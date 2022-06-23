using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Assessment;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services;

public class AssessmentService : BaseService, IAssessmentService
{
    public async Task CreateAspect(params CreateAspectRequestModel[] models)
    {
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var model in models)
            {
                if (model.TypeId == AspectTypes.Grade && model.GradeSetId == null)
                {
                    throw new InvalidDataException($"A grade set was not provided for {model.Name}.");
                }

                if (model.TypeId == AspectTypes.MarkDecimal || model.TypeId == AspectTypes.MarkInteger)
                {
                    if (model.MinMark == null)
                    {
                        throw new InvalidDataException($"A minimum mark was not provided for {model.Name}");
                    }

                    if (model.MaxMark == null)
                    {
                        throw new InvalidDataException($"A maximum mark was not provided for {model.Name}");
                    }
                }

                var aspect = new Aspect
                {
                    TypeId = model.TypeId,
                    GradeSetId = model.GradeSetId,
                    MinMark = model.MinMark,
                    MaxMark = model.MaxMark,
                    Name = model.Name,
                    ColumnHeading = model.ColumnHeading,
                    Private = model.Private
                };

                unitOfWork.Aspects.Create(aspect);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeleteAspect(params Guid[] aspectIds)
    {
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var aspectId in aspectIds)
            {
                var aspect = await unitOfWork.Aspects.GetById(aspectId);

                if (aspect == null)
                {
                    throw new NotFoundException("Aspect not found.");
                }

                await unitOfWork.Aspects.Delete(aspectId);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task CreateOrUpdateResult(params CreateOrUpdateResultRequestModel[] models)
    {
        List<Aspect> cachedAspects = new List<Aspect>();
        List<ResultSet> cachedResultSets = new List<ResultSet>();
        List<Grade> cachedGrades = new List<Grade>();

        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var model in models)
            {
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
                    await unitOfWork.Results.Get(model.StudentId, model.AspectId, model.ResultSetId);

                bool createNewResult = result == null;

                if (createNewResult)
                {
                    result = new Result
                    {
                        StudentId = model.StudentId,
                        AspectId = model.AspectId,
                        ResultSetId = model.ResultSetId,
                        ColourCode = model.ColourCode,
                        Note = model.Note,
                        Date = model.Date
                    };
                }
                else
                {
                    result.ColourCode = model.ColourCode;
                    result.Note = model.Note;
                    result.Date = model.Date;
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
    }

    public async Task DeleteResult(params Guid[] resultIds)
    {
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var resultId in resultIds)
            {
                var result = await unitOfWork.Results.GetById(resultId);

                if (result == null)
                {
                    throw new NotFoundException("Result not found.");
                }

                await unitOfWork.Results.Delete(resultId);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}