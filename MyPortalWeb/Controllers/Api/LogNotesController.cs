using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Permissions;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/student/logNotes")]
    public class LogNotesController : StudentApiController
    {
        private readonly ILogNoteService _logNoteService;

        public LogNotesController(IUserService userService, IAcademicYearService academicYearService, IStudentService studentService, ILogNoteService logNoteService) : base(userService, academicYearService, studentService)
        {
            _logNoteService = logNoteService;
        }

        [HttpGet]
        [Route("id")]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = await _logNoteService.GetById(logNoteId);

                if (await AuthenticateStudent(logNote.StudentId))
                {
                    return Ok(logNote);
                }

                return Forbid();
            }, Permissions.Student.StudentLogNotes.ViewLogNotes);
        }

        [HttpGet]
        [Route("types")]
        public async Task<IActionResult> GetTypes()
        {
            return await ProcessAsync(async () =>
            {
                var logNoteTypes = await _logNoteService.GetTypes();

                return Ok(logNoteTypes);
            });
        }

        [HttpGet]
        [Route("student")]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudent(studentId))
                {
                    var logNotes = await _logNoteService.GetByStudent(studentId, academicYearId);

                    var result = logNotes;

                    return Ok(result);
                }

                return Forbid();
            }, Permissions.Student.StudentLogNotes.ViewLogNotes);
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = new LogNoteModel
                {
                    StudentId = model.StudentId,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                var author = await UserService.GetUserByPrincipal(User);

                logNote.CreatedById = author.Id;
                logNote.UpdatedById = author.Id;

                await _logNoteService.Create(logNote);

                return Ok("Log note created successfully.");
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UpdateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = new LogNoteModel
                {
                    Id = model.Id,
                    StudentId = model.StudentId,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                var user = await UserService.GetUserByPrincipal(User);

                logNote.UpdatedById = user.Id;
                await _logNoteService.Update(logNote);

                return Ok("Log note updated successfully.");
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                await _logNoteService.Delete(logNoteId);

                return Ok("Log note deleted successfully.");
            }, Permissions.Student.StudentLogNotes.EditLogNotes);
        }

        public override void Dispose()
        {
            _logNoteService.Dispose();
        }
    }
}
