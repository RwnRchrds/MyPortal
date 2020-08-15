using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Student.LogNotes;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/student/logNote")]
    public class LogNoteController : BaseApiController
    {
        private readonly ILogNoteService _logNoteService;
        private readonly IStudentService _studentService;

        public LogNoteController(ILogNoteService logNoteService, IStudentService studentService, IApplicationUserService userService) : base(userService)
        {
            _logNoteService = logNoteService;
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize]
        [Route("get")]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = await _logNoteService.GetById(logNoteId);

                if (await AuthenticateStudentResource(_studentService, logNote.StudentId))
                {
                    return Ok(logNote);
                }

                return Forbid();
            });
        }

        [HttpGet]
        [Authorize]
        [Route("getByStudent")]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid academicYearId)
        {
            return await ProcessAsync(async () =>
            {
                if (await AuthenticateStudentResource(_studentService, studentId))
                {
                    var logNotes = await _logNoteService.GetByStudent(studentId, academicYearId);

                    var result = logNotes;

                    return Ok(result);
                }

                return Forbid();
            });
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        public async Task<IActionResult> Create([FromForm] CreateLogNoteModel model)
        {
            return await ProcessAsync(async () =>
            {
                var logNote = new LogNoteModel
                {
                    StudentId = model.StudentId,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                var author = await _userService.GetUserByPrincipal(User);

                if (author.SelectedAcademicYearId == null)
                {
                    throw new Exception("No academic year is selected.");
                }

                logNote.CreatedById = author.Id;
                logNote.UpdatedById = author.Id;
                logNote.AcademicYearId = author.SelectedAcademicYearId.Value;

                await _logNoteService.Create(logNote);

                return Ok("Log note created successfully.");
            });
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        public async Task<IActionResult> Update([FromForm] UpdateLogNoteModel model)
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

                var user = await _userService.GetUserByPrincipal(User);

                logNote.UpdatedById = user.Id;
                await _logNoteService.Update(logNote);

                return Ok("Log note updated successfully.");
            });
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            return await ProcessAsync(async () =>
            {
                await _logNoteService.Delete(logNoteId);

                return Ok("Log note deleted successfully.");
            });
        }

        public override void Dispose()
        {
            _logNoteService.Dispose();
        }
    }
}
