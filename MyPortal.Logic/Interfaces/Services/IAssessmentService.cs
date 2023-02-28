using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Assessment;
using MyPortal.Logic.Models.Data.Assessment.MarksheetEntry;

using MyPortal.Logic.Models.Requests.Assessment;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAssessmentService : IService
{
    Task<ResultModel> GetResult(Guid resultId);
    Task<ResultModel> GetResult(Guid studentId, Guid aspectId, Guid resultSetId);
    Task<IEnumerable<ResultModel>> GetPreviousResults(Guid resultId);
    Task<IEnumerable<ResultModel>> GetPreviousResults(Guid studentId, Guid aspectId, DateTime dateTo);
    Task<MarksheetEntryDataModel> GetMarksheet(Guid marksheetId);
    Task<AspectModel> CreateAspect(AspectRequestModel aspect);
    Task UpdateAspect(Guid aspectId, AspectRequestModel aspect);
    Task DeleteAspect(Guid aspectId);
    Task SaveResults(params ResultSummaryModel[] models);
    Task DeleteResult(Guid resultId);
}