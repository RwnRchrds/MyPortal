using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Models.Database;
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
        public async Task<IHttpActionResult> CreateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            try
            {
                await _service.CreateRegGroup(regGroup);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group created");
        }

        [HttpDelete]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/delete/{regGroupId:int}", Name = "ApiDeleteRegGroup")]
        public async Task<IHttpActionResult> DeleteRegGroup([FromUri] int regGroupId)
        {
            try
            {
                await _service.DeleteRegGroup(regGroupId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byId/{regGroupId:int}", Name = "ApiGetRegGroupById")]
        public async Task<PastoralRegGroupDto> GetRegGroupById([FromUri] int regGroupId)
        {
            try
            {
                var regGroup = await _service.GetRegGroupById(regGroupId);

                return Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>(regGroup);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewRegGroups")]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}", Name = "ApiGetRegGroupsByYearGroup")]
        public async Task<IEnumerable<PastoralRegGroupDto>> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                var regGroups = await _service.GetRegGroupsByYearGroup(yearGroupId);

                return regGroups.Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }
 
        [HttpGet]
        [Route("regGroups/get/all", Name = "ApiGetAllRegGroups")]
        [RequiresPermission("ViewRegGroups")]
        public async Task<IEnumerable<PastoralRegGroupDto>> GetAllRegGroups()
        {
            try
            {
                var regGroups = await _service.GetAllRegGroups();

                return regGroups.Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditRegGroups")]
        [Route("regGroups/update", Name = "ApiUpdateRegGroup")]
        public async Task<IHttpActionResult> UpdateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            try
            {
                await _service.UpdateRegGroup(regGroup);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Reg group updated");
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/create", Name = "ApiCreateYearGroup")]
        public async Task<IHttpActionResult> CreateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            try
            {
                await _service.CreateYearGroup(yearGroup);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group created");
        }

        [HttpDelete]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/delete/{yearGroupId:int}", Name = "ApiDeleteYearGroup")]
        public async Task<IHttpActionResult> DeleteYearGroup([FromUri] int yearGroupId)
        {
            try
            {
                await _service.DeleteYearGroup(yearGroupId);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group deleted");
        }

        [HttpGet]
        [RequiresPermission("ViewYearGroups")]
        [Route("yearGroups/get/all", Name = "ApiGetAllYearGroups")]
        public async Task<IEnumerable<PastoralYearGroupDto>> GetAllYearGroups()
        {
            try
            {
                var yearGroups = await _service.GetAllYearGroups();

                return yearGroups.Select(Mapper.Map<PastoralYearGroup, PastoralYearGroupDto>);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditYearGroups")]
        [Route("yearGroups/update", Name = "ApiUpdateYearGroup")]
        public async Task<IHttpActionResult> UpdateYearGroup([FromBody] PastoralYearGroup yearGroup)
        {
            try
            {
                await _service.UpdateYearGroup(yearGroup);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Year group updated");
        }
    }
}
