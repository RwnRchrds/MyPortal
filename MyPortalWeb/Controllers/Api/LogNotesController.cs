using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    public class LogNotesController : PersonalDataController
    {
        private readonly ILogNoteService _logNoteService;
        private readonly IAcademicYearService _academicYearService;

        public LogNotesController(IUserService userService, IPersonService personService,
            IStudentService studentService, ILogNoteService logNoteService, IAcademicYearService academicYearService) 
            : base(userService, personService, studentService)
        {
            _academicYearService = academicYearService;
            _logNoteService = logNoteService;
        }

        [HttpGet]
        [Route("api/logNotes/{logNoteId}")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(LogNoteModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            try
            {
                var logNote = await _logNoteService.GetLogNoteById(logNoteId);

                if (logNote.Private)
                {
                    var viewRestricted = User.IsType(UserTypes.Staff);

                    if (!viewRestricted)
                    {
                        return PermissionError();
                    }
                }

                var student = await StudentService.GetStudentById(logNote.StudentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    return Ok(logNote);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("api/logNotes/types")]
        [ProducesResponseType(typeof(IEnumerable<LogNoteTypeModel>), 200)]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                var logNoteTypes = await _logNoteService.GetLogNoteTypes();

                return Ok(logNoteTypes);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("api/students/{studentId}/logNotes")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(IEnumerable<LogNoteModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetStudentById(studentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await _academicYearService.GetCurrentAcademicYear(true)).Id;
                    }

                    var viewRestricted = User.IsType(UserTypes.Staff);
                    
                    var logNotes = await _logNoteService.GetLogNotesByStudent(studentId, academicYearId.Value, viewRestricted);

                    var result = logNotes;

                    return Ok(result);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentEditStudentLogNotes)]
        [Route("api/logNotes")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] LogNoteRequestModel requestModel)
        {
            try
            {
                await _logNoteService.CreateLogNote(requestModel);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentEditStudentLogNotes)]
        [Route("api/logNotes/{logNoteId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromRoute] Guid logNoteId, [FromBody] LogNoteRequestModel requestModel)
        {
            try
            {
                await _logNoteService.UpdateLogNote(logNoteId, requestModel);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentEditStudentLogNotes)]
        [Route("api/logNotes/{logNoteId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid logNoteId)
        {
            try
            {
                await _logNoteService.DeleteLogNote(logNoteId);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
