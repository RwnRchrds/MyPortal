using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/student/logNotes")]
    public class LogNotesController : StudentApiController
    {
        public LogNotesController(IAppServiceCollection services) : base(services)
        {
        }

        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(LogNoteModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = await Services.LogNotes.GetById(logNoteId);

                if (await AuthoriseStudent(logNote.StudentId))
                {
                    return Ok(logNote);
                }

                return Forbid();
            }, PermissionValue.StudentViewStudentLogNotes);
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<LogNoteTypeModel>), 200)]
        public async Task<IActionResult> GetTypes()
        {
            return await ProcessAsync(async () =>
            {
                var logNoteTypes = await Services.LogNotes.GetTypes();

                return Ok(logNoteTypes);
            });
        }

        [HttpGet]
        [Route("student")]
        [ProducesResponseType(typeof(IEnumerable<LogNoteModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthoriseStudent(studentId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await Services.AcademicYears.GetCurrentAcademicYear(true)).Id;
                    }

                    var logNotes = await Services.LogNotes.GetByStudent(studentId, academicYearId.Value);

                    var result = logNotes;

                    return Ok(result);
                }

                return Forbid();
            }, PermissionValue.StudentViewStudentLogNotes);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var author = await Services.Users.GetUserByPrincipal(User);

                model.CreatedById = author.Id.Value;

                await Services.LogNotes.Create(model);

                return Ok();
            }, PermissionValue.StudentEditStudentLogNotes);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                await Services.LogNotes.Update(model);

                return Ok();
            }, PermissionValue.StudentEditStudentLogNotes);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                await Services.LogNotes.Delete(logNoteId);

                return Ok();
            }, PermissionValue.StudentEditStudentLogNotes);
        }
    }
}
