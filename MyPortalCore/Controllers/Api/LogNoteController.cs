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
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Student.LogNotes;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/student/logNote")]
    public class ProfileLogNoteController : BaseApiController
    {
        private readonly ILogNoteService _service;

        public ProfileLogNoteController(ILogNoteService service, IApplicationUserService userService) : base(userService)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        [RequiresPermission(Permissions.Student.LogNotes.View)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            return await Process(async () =>
            {
                var logNote = await _service.GetById(logNoteId);

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
                var logNotes = await _service.GetByStudent(studentId, academicYearId);

                var result = logNotes.Select(_dTMapper.Map<DataGridProfileLogNote>);

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

                await _service.Create(logNote);

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

                await _service.Update(logNote);

                return Ok("Log note updated.");
            });
        }

        [HttpDelete]
        [RequiresPermission(Permissions.Student.LogNotes.Edit)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            return await Process(async () =>
            {
                await _service.Delete(logNoteId);

                return Ok("Log note deleted.");
            });
        }
    }
}
