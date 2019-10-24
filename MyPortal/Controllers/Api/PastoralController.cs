using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MyPortal.Dtos;
using MyPortal.Attributes;
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
        public async Task<IHttpActionResult> CreateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            try
            {
                await PastoralProcesses.CreateRegGroup(regGroup, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group created");
        }

        [HttpDelete]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/delete/{regGroupId:int}", Name = "ApiPastoralDeleteRegGroup")]
        public async Task<IHttpActionResult> DeleteRegGroup([FromUri] int regGroupId)
        {
            try
            {
                await PastoralProcesses.DeleteRegGroup(regGroupId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byId/{regGroupId:int}", Name = "ApiPastoralGetRegGroupById")]
        public async Task<PastoralRegGroupDto> GetRegGroupById([FromUri] int regGroupId)
        {
            try
            {
                return await PastoralProcesses.GetRegGroupById(regGroupId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}", Name = "ApiPastoralGetRegGroupsByYearGroup")]
        public async Task<IEnumerable<PastoralRegGroupDto>> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                return await PastoralProcesses.GetRegGroupsByYearGroup(yearGroupId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpGet]
        [Route("regGroups/get/all", Name = "ApiPastoralGetAllRegGroups")]
        [RequiresPermission("ViewRegGroups")]
        public async Task<IEnumerable<PastoralRegGroupDto>> GetAllRegGroups()
        {
            try
            {
                return await PastoralProcesses.GetAllRegGroups(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/hasStudents/{regGroupId:int}", Name = "ApiPastoralRegGroupHasStudents")]
        public async Task<bool> RegGroupHasStudents([FromUri] int regGroupId)
        {
            try
            {
                return await PastoralProcesses.RegGroupContainsStudents(regGroupId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/update", Name = "ApiPastoralUpdateRegGroup")]
        public async Task<IHttpActionResult> UpdateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            try
            {
                await PastoralProcesses.UpdateRegGroup(regGroup, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group updated");
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/create", Name = "ApiPastoralCreateYearGroup")]
        public async Task<IHttpActionResult> CreateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            try
            {
                await PastoralProcesses.CreateYearGroup(yearGroup, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group created");
        }

        [HttpDelete]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/delete/{yearGroupId:int}", Name = "ApiPastoralDeleteYearGroup")]
        public async Task<IHttpActionResult> DeleteYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                await PastoralProcesses.DeleteYearGroup(yearGroupId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewYearGroups")]
        [Route("yearGroups/get/all", Name = "ApiPastoralGetAllYearGroups")]
        public async Task<IEnumerable<PastoralYearGroupDto>> GetAllYearGroups()
        {
            try
            {
                return await PastoralProcesses.GetAllYearGroups(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/update", Name = "ApiPastoralUpdateYearGroup")]
        public async Task<IHttpActionResult> UpdateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            try
            {
                await PastoralProcesses.UpdateYearGroup(yearGroup, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group updated");
        }
    }
}
