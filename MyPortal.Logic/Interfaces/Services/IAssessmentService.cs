using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Assessment;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAssessmentService
{
    Task CreateOrUpdateResult(params CreateOrUpdateResultRequestModel[] models);
    Task DeleteResult(params Guid[] resultIds);
}