using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Services;
using MyPortal.Services;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/pastoral")]
    public class PastoralController : MyPortalApiController
    {
        private readonly PastoralService _service;

        public PastoralController()
        {
            _service = new PastoralService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
        
        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/create", Name = "ApiCreateRegGroup")]
        public async Task<IHttpActionResult> CreateRegGroup([FromBody] RegGroupDto regGroup)
        {
            try
            {
                await _service.CreateRegGroup(regGroup);

                return Ok("Reg group created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/delete/{regGroupId:int}", Name = "ApiDeleteRegGroup")]
        public async Task<IHttpActionResult> DeleteRegGroup([FromUri] int regGroupId)
        {
            try
            {
                await _service.DeleteRegGroup(regGroupId);

                return Ok("Reg group deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byId/{regGroupId:int}", Name = "ApiGetRegGroupById")]
        public async Task<RegGroupDto> GetRegGroupById([FromUri] int regGroupId)
        {
            try
            {
                return await _service.GetRegGroupById(regGroupId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}", Name = "ApiGetRegGroupsByYearGroup")]
        public async Task<IEnumerable<RegGroupDto>> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                return await _service.GetRegGroupsByYearGroup(yearGroupId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpGet]
        [Route("regGroups/get/all", Name = "ApiGetAllRegGroups")]
        [RequiresPermission("ViewRegGroups")]
        public async Task<IEnumerable<RegGroupDto>> GetAllRegGroups()
        {
            try
            {
                return await _service.GetAllRegGroups();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/update", Name = "ApiUpdateRegGroup")]
        public async Task<IHttpActionResult> UpdateRegGroup([FromBody] RegGroupDto regGroup)
        {
            try
            {
                await _service.UpdateRegGroup(regGroup);

                return Ok("Reg group updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/create", Name = "ApiCreateYearGroup")]
        public async Task<IHttpActionResult> CreateYearGroup([FromBody] YearGroupDto yearGroup)
        {
            try
            {
                await _service.CreateYearGroup(yearGroup);

                return Ok("Year group created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/delete/{yearGroupId:int}", Name = "ApiDeleteYearGroup")]
        public async Task<IHttpActionResult> DeleteYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                await _service.DeleteYearGroup(yearGroupId);

                return Ok("Year group deleted");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewYearGroups")]
        [Route("yearGroups/get/all", Name = "ApiGetAllYearGroups")]
        public async Task<IEnumerable<YearGroupDto>> GetAllYearGroups()
        {
            try
            {
                return await _service.GetAllYearGroups();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/update", Name = "ApiUpdateYearGroup")]
        public async Task<IHttpActionResult> UpdateYearGroup([FromBody] YearGroupDto yearGroup)
        {
            try
            {
                await _service.UpdateYearGroup(yearGroup);

                return Ok("Year group updated");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
