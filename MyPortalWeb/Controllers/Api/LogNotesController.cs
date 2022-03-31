using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/student/logNotes")]
    public class LogNotesController : PersonalDataController
    {
        private ILogNoteService _logNoteService;
        private IAcademicYearService _academicYearService;

        public LogNotesController(IStudentService studentService, IPersonService personService,
            IUserService userService, IRoleService roleService,
            ILogNoteService logNoteService, IAcademicYearService academicYearService) : base(studentService,
            personService, userService, roleService)
        {
            _logNoteService = logNoteService;
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Route("{logNoteId}")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(LogNoteModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            try
            {
                var logNote = await _logNoteService.GetById(logNoteId);

                var student = await StudentService.GetById(logNote.StudentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    return Ok(logNote);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<LogNoteTypeModel>), 200)]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                var logNoteTypes = await _logNoteService.GetTypes();

                return Ok(logNoteTypes);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("student/{studentId}")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(IEnumerable<LogNoteModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                var student = await StudentService.GetById(studentId);

                if (await CanAccessPerson(student.PersonId))
                {
                    if (academicYearId == null || academicYearId == Guid.Empty)
                    {
                        academicYearId = (await _academicYearService.GetCurrentAcademicYear(true)).Id;
                    }

                    var logNotes = await _logNoteService.GetByStudent(studentId, academicYearId.Value);

                    var result = logNotes;

                    return Ok(result);
                }

                return Forbid();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.UserType.Staff)]
        [Permission(PermissionValue.StudentEditStudentLogNotes)]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Create([FromBody] CreateLogNoteModel model)
        {
            try
            {
                var user = await GetLoggedInUser();
                
                await _logNoteService.Create(user.Id.Value, model);

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
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateLogNoteModel model)
        {
            try
            {
                await _logNoteService.Update(model);

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
        [Route("delete")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            try
            {
                await _logNoteService.Delete(logNoteId);

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
