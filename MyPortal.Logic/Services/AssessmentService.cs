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
    public async Task CreateOrUpdateResult(params CreateOrUpdateResultRequestModel[] models)
    {
        List<Aspect> cachedAspects = new List<Aspect>();
        List<ResultSet> cachedResultSets = new List<ResultSet>();
        List<Grade> cachedGrades = new List<Grade>();

        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var model in models)
            {
                Aspect aspect = cachedAspects.FirstOrDefault(x => x.Id == model.AspectId) ?? await unitOfWork.Aspects.GetById(model.AspectId);

                if (aspect == null)
                {
                    throw new NotFoundException("Aspect not found.");
                }

                ResultSet resultSet = cachedResultSets.FirstOrDefault(x => x.Id == model.ResultSetId) ?? await unitOfWork.ResultSets.GetById(model.ResultSetId);

                if (resultSet == null)
                {
                    throw new NotFoundException("Result set not found.");
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

                    grade = cachedGrades.FirstOrDefault(x => x.Id == model.GradeId) ??
                            await unitOfWork.Grades.GetById(model.GradeId.Value);

                    if (grade == null)
                    {
                        throw new NotFoundException("Grade not found.");
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