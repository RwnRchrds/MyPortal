using System.Collections.Generic;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/pastoral")]
    public class PastoralController : MyPortalApiController
    {

        [HttpPost]
        [Route("regGroups/create")]
        public IHttpActionResult CreateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            return PrepareResponse(PastoralProcesses.CreateRegGroup(regGroup, _context));
        }

        [HttpDelete]
        [Route("regGroups/delete/{regGroupId:int}")]
        public IHttpActionResult DeleteRegGroup([FromUri] int regGroupId)
        {
            return PrepareResponse(PastoralProcesses.DeleteRegGroup(regGroupId, _context));
        }

        [HttpGet]
        [Route("regGroups/get/byId/{regGroupId:int}")]
        public PastoralRegGroupDto GetRegGroupById([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupById(regGroupId, _context));
        }

        [HttpGet]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}")]
        public IEnumerable<PastoralRegGroupDto> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupsByYearGroup(yearGroupId, _context));
        }
 
        [HttpGet]
        [Route("regGroups/get/all")]
        public IEnumerable<PastoralRegGroupDto> GetAllRegGroups()
        {
            return PrepareResponseObject(PastoralProcesses.GetAllRegGroups(_context));
        }

        [HttpGet]
        [Route("regGroups/hasStudents/{regGroupId:int}")]
        public bool RegGroupHasStudents([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.RegGroupContainsStudents(regGroupId, _context));
        }
 
        [HttpPost]
        [Route("regGroups/update")]
        public IHttpActionResult UpdateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            return PrepareResponse(PastoralProcesses.UpdateRegGroup(regGroup, _context));
        }

        [HttpPost]
        [Route("yearGroups/create")]
        public IHttpActionResult CreateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            return PrepareResponse(PastoralProcesses.CreateYearGroup(yearGroup, _context));
        }

        [HttpDelete]
        [Route("yearGroups/delete/{yearGroupId:int}")]
        public IHttpActionResult DeleteYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponse(PastoralProcesses.DeleteYearGroup(yearGroupId, _context));
        }

        [HttpGet]
        [Route("yearGroups/get/all")]
        public IEnumerable<PastoralYearGroupDto> GetAllYearGroups()
        {
            return PrepareResponseObject(PastoralProcesses.GetAllYearGroups(_context));
        }

        [HttpPost]
        [Route("yearGroups/update")]
        public IHttpActionResult UpdateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            return PrepareResponse(PastoralProcesses.UpdateYearGroup(yearGroup, _context));
        }
    }
}
