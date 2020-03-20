using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ProfileLogNoteController(IProfileLogNoteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("student")]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.View)]
        public async Task<IActionResult> GetByStudent([FromQuery] Guid studentId)
        {
            try
            {
                var logNotes = await _service.GetByStudent(studentId);

                var result = logNotes.Select(_dTMapper.Map<DataGridProfileLogNote>);

                return Ok(result);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
        public async Task<IActionResult> Create([FromBody] ProfileLogNoteDetails logNote)
        {
            try
            {
                await _service.Create(logNote);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
        public async Task<IActionResult> Update([FromBody] ProfileLogNoteDetails logNoteDetails)
        {
            try
            {
                await _service.Update(logNoteDetails);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission(PermissionDictionary.Profiles.LogNotes.Edit)]
        public async Task<IActionResult> Delete([FromQuery] Guid logNoteId)
        {
            try
            {
                await _service.Delete(logNoteId);

                return Ok();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
