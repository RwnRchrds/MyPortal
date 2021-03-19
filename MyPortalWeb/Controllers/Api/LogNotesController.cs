using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Caching;
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
        public LogNotesController(IAppServiceCollection services, IRolePermissionsCache rolePermissionsCache) : base(services, rolePermissionsCache)
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
            }, Permissions.Student.StudentLogNotes.ViewLogNotes);
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
            }, Permissions.Student.StudentLogNotes.ViewLogNotes);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = new LogNoteModel
                {
                    AcademicYearId = (await Services.AcademicYears.GetCurrentAcademicYear(true)).Id,
                        StudentId = model.StudentId,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                var author = await Services.Users.GetUserByPrincipal(User);

                logNote.CreatedById = author.Id;
                logNote.UpdatedById = author.Id;

                await Services.LogNotes.Create(logNote);

                return Ok();
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = new LogNoteModel
                {
                    Id = model.Id,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                var user = await Services.Users.GetUserByPrincipal(User);

                logNote.UpdatedById = user.Id;
                await Services.LogNotes.Update(logNote);

                return Ok();
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
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
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
        }
    }
}
