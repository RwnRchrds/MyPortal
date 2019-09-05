using System.Collections.Generic;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/pastoral")]
    public class PastoralController : MyPortalApiController
    {

        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/create", Name = "ApiPastoralCreateRegGroup")]
        public IHttpActionResult CreateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            return PrepareResponse(PastoralProcesses.CreateRegGroup(regGroup, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/delete/{regGroupId:int}", Name = "ApiPastoralDeleteRegGroup")]
        public IHttpActionResult DeleteRegGroup([FromUri] int regGroupId)
        {
            return PrepareResponse(PastoralProcesses.DeleteRegGroup(regGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byId/{regGroupId:int}", Name = "ApiPastoralGetRegGroupById")]
        public PastoralRegGroupDto GetRegGroupById([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupById(regGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}", Name = "ApiPastoralGetRegGroupsByYearGroup")]
        public IEnumerable<PastoralRegGroupDto> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupsByYearGroup(yearGroupId, _context));
        }
 
        [HttpGet]
        [Route("regGroups/get/all", Name = "ApiPastoralGetAllRegGroups")]
        [RequiresPermission("ViewRegGroups")]
        public IEnumerable<PastoralRegGroupDto> GetAllRegGroups()
        {
            return PrepareResponseObject(PastoralProcesses.GetAllRegGroups(_context));
        }

        [HttpGet]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/hasStudents/{regGroupId:int}", Name = "ApiPastoralRegGroupHasStudents")]
        public bool RegGroupHasStudents([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.RegGroupContainsStudents(regGroupId, _context));
        }
 
        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/update", Name = "ApiPastoralUpdateRegGroup")]
        public IHttpActionResult UpdateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            return PrepareResponse(PastoralProcesses.UpdateRegGroup(regGroup, _context));
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/create", Name = "ApiPastoralCreateYearGroup")]
        public IHttpActionResult CreateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            return PrepareResponse(PastoralProcesses.CreateYearGroup(yearGroup, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/delete/{yearGroupId:int}", Name = "ApiPastoralDeleteYearGroup")]
        public IHttpActionResult DeleteYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponse(PastoralProcesses.DeleteYearGroup(yearGroupId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewYearGroups")]
        [Route("yearGroups/get/all", Name = "ApiPastoralGetAllYearGroups")]
        public IEnumerable<PastoralYearGroupDto> GetAllYearGroups()
        {
            return PrepareResponseObject(PastoralProcesses.GetAllYearGroups(_context));
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/update", Name = "ApiPastoralUpdateYearGroup")]
        public IHttpActionResult UpdateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            return PrepareResponse(PastoralProcesses.UpdateYearGroup(yearGroup, _context));
        }
    }
}
