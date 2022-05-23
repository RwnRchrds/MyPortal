using System;
using System.Security.Claims;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Assessment;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services;

public class ExamService : BaseService, IExamService
{
    public async Task CreateResultEmbargo(params ResultEmbargoRequestModel[] models)
    {
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var model in models)
            {
                if (model.EndDate < DateTime.Now)
                {
                    throw new InvalidDataException("End date cannot be in the past.");
                }

                var embargo = new ExamResultEmbargo
                {
                    ResultSetId = model.ResultSetId,
                    EndTime = model.EndDate
                };
                
                unitOfWork.ExamResultEmbargoes.Create(embargo);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task UpdateResultEmbargo(params ResultEmbargoRequestModel[] models)
    {
        using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
        {
            foreach (var model in models)
            {
                if (model.EndDate < DateTime.Now)
                {
                    throw new InvalidDataException("End date cannot be in the past.");
                }

                var embargoInDb = await unitOfWork.ExamResultEmbargoes.GetByResultSetId(model.ResultSetId);

                if (embargoInDb == null)
                {
                    throw new NotFoundException("Exam result embargo not found.");
                }

                embargoInDb.EndTime = model.EndDate;

                await unitOfWork.ExamResultEmbargoes.Update(embargoInDb);
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}