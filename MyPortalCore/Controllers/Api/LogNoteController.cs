using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using MyPortal.Logic.Models.Summary;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/student/logNote")]
    public class LogNoteController : BaseApiController
    {
        private readonly ILogNoteService _logNoteService;

        public LogNoteController(ILogNoteService logNoteService, IApplicationUserService userService) : base(userService)
        {
            _logNoteService = logNoteService;
        }

        [HttpGet]
        [Route("get")]
        [RequiresPermission(Permissions.Student.LogNotes.View)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            return await Process(async () =>
            {
                var logNote = await _logNoteService.GetById(logNoteId);

                return Ok(logNote);
            });
        }

        [HttpGet]
        [Route("student")]
        [RequiresPermission(Permissions.Student.LogNotes.View)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid academicYearId)
        {
            return await Process(async () =>
            {
                var logNotes = await _logNoteService.GetByStudent(studentId, academicYearId);

                var result = logNotes.Select(_dTMapper.Map<LogNoteListModel>);

                return Ok(result);
            });
        }

        [HttpPost]
        [RequiresPermission(Permissions.Student.LogNotes.Edit)]
        public async Task<IActionResult> Create([FromForm] CreateLogNoteModel model)
        {
            return await Process(async () =>
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

                logNote.AuthorId = author.Id;
                logNote.AcademicYearId = (Guid) author.SelectedAcademicYearId;

                await _logNoteService.Create(logNote);

                return Ok("Log note created.");
            });
        }

        [HttpPut]
        [RequiresPermission(Permissions.Student.LogNotes.Edit)]
        public async Task<IActionResult> Update([FromForm] UpdateLogNoteModel model)
        {
            return await Process(async () =>
            {
                var logNote = new LogNoteModel
                {
                    Id = model.Id,
                    StudentId = model.StudentId,
                    TypeId = model.TypeId,
                    Message = model.Message
                };

                await _logNoteService.Update(logNote);

                return Ok("Log note updated.");
            });
        }

        [HttpDelete]
        [RequiresPermission(Permissions.Student.LogNotes.Edit)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            return await Process(async () =>
            {
                await _logNoteService.Delete(logNoteId);

                return Ok("Log note deleted.");
            });
        }

        public override void Dispose()
        {
            _logNoteService.Dispose();
        }
    }
}
