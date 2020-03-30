﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Authorisation.Attributes;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Details;

namespace MyPortalCore.Controllers.Api
{
    [Route("api/student/logNote")]
    public class ProfileLogNoteController : BaseApiController
    {
        private readonly IProfileLogNoteService _service;

        public ProfileLogNoteController(IProfileLogNoteService service, IApplicationUserService userService) : base(userService)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.View)]
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
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.View)]
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
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
        public async Task<IActionResult> Create([FromForm] ProfileLogNoteDetails logNote)
        {
            return await Process(async () =>
            {
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
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
        public async Task<IActionResult> Update([FromForm] ProfileLogNoteDetails logNoteDetails)
        {
            return await Process(async () =>
            {
                if (logNoteDetails.Id == Guid.Empty)
                {
                    return BadRequest("Invalid Data.");
                }

                await _service.Update(logNoteDetails);

                return Ok("Log note updated.");
            });
        }

        [HttpDelete]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
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
