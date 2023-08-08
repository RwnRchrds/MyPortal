using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Assessment;
using MyPortal.Logic.Models.Requests.Assessment;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api;

[Microsoft.AspNetCore.Components.Route("api/assessment")]
public sealed class AssessmentController : PersonalDataController
{
    private readonly IAssessmentService _assessmentService;

    public AssessmentController(IUserService userService, IPersonService personService, IStudentService studentService,
        IAssessmentService assessmentService) 
        : base(userService, personService, studentService)
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
            var student = await StudentService.GetStudentById(model.StudentId);

            if (await CanAccessPerson(student.PersonId))
            {
                var resultHistory =
                    await _assessmentService.GetPreviousResults(model.StudentId, model.AspectId, model.DateTo);

                return Ok(resultHistory);
            }

            return PermissionError();
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