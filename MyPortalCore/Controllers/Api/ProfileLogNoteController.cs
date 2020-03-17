using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;

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

        [Route("getByStudent/{studentId}")]
        public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId)
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
    }
}
