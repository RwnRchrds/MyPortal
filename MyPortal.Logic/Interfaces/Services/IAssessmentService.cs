using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Assessment;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAssessmentService
{
    Task CreateAspect(params CreateAspectRequestModel[] models);
    Task DeleteAspect(params Guid[] aspectIds);
    Task CreateOrUpdateResult(params CreateOrUpdateResultRequestModel[] models);
    Task DeleteResult(params Guid[] resultIds);
}