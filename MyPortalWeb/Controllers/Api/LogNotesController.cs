﻿using System;
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
using MyPortalWeb.Attributes;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/student/logNotes")]
    public class LogNotesController : StudentDataController
    {
        private ILogNoteService _logNoteService;
        private IAcademicYearService _academicYearService;

        public LogNotesController(IStudentService studentService, IUserService userService, IRoleService roleService,
            ILogNoteService logNoteService, IAcademicYearService academicYearService) : base(studentService,
            userService, roleService)
        {
            _logNoteService = logNoteService;
            _academicYearService = academicYearService;
        }

        [HttpGet]
        [Route("id")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(LogNoteModel), 200)]
        public async Task<IActionResult> GetById([FromQuery] Guid logNoteId)
        {
            try
            {
                var logNote = await _logNoteService.GetById(logNoteId);

                if (await AuthoriseStudent(logNote.StudentId))
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
        [Route("student")]
        [Permission(PermissionValue.StudentViewStudentLogNotes)]
        [ProducesResponseType(typeof(IEnumerable<LogNoteModel>), 200)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId, [FromQuery] Guid? academicYearId)
        {
            try
            {
                if (await AuthoriseStudent(studentId))
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
                var author = await UserService.GetUserByPrincipal(User);

                model.CreatedById = author.Id.Value;

                await _logNoteService.Create(model);

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
