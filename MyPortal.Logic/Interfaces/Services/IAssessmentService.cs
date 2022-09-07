using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Assessment;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAssessmentService
{
    Task CreateAspect(AspectRequestModel aspect);
    Task UpdateAspect(Guid aspectId, AspectRequestModel aspect);
    Task DeleteAspect(Guid aspectId);
    Task SaveResults(params ResultRequestModel[] results);
    Task DeleteResult(Guid resultId);
}