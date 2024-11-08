using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Requests.Assessment;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api;

[Microsoft.AspNetCore.Components.Route("api/assessment")]
public sealed class AssessmentController : BaseApiController
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentController(IAssessmentService assessmentService)
    {
        _assessmentService = assessmentService;
    }

    [HttpGet]
    [Route("results/history")]
    [Permission(PermissionValue.AssessmentViewResults)]
    public async Task<IActionResult> GetPreviousResults([FromQuery] ResultHistoryRequestModel model)
    {
        try
        {
            var resultHistory =
                await _assessmentService.GetPreviousResults(model.StudentId, model.AspectId, model.DateTo);

            return Ok(resultHistory);
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }

    [HttpPut]
    [Route("results")]
    [Permission(PermissionValue.AssessmentEditResults)]
    public async Task<IActionResult> SaveResults([FromBody] UpdateResultsRequestModel model)
    {
        try
        {
            await _assessmentService.SaveResults(model.Results);

            return Ok();
        }
        catch (Exception e)
        {
            return HandleException(e);
        }
    }
}