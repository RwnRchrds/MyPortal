using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    [RoutePrefix("api/pastoral")]
    public class PastoralController : MyPortalApiController
    {
        /// <summary>
        /// Creates registration group.
        /// </summary>
        /// <param name="regGroup">The registration group to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("regGroups/create")]
        public IHttpActionResult CreateRegGroup([FromBody] PastoralRegGroup regGroup)
        {
            return PrepareResponse(PastoralProcesses.CreateRegGroup(regGroup, _context));
        }

        /// <summary>
        /// Deletes the specified registration group.
        /// </summary>
        /// <param name="regGroupId">The ID of the registration group to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("regGroups/delete/{regGroupId:int}")]
        public IHttpActionResult DeleteRegGroup([FromUri] int regGroupId)
        {
            return PrepareResponse(PastoralProcesses.DeleteRegGroup(regGroupId, _context));
        }

        /// <summary>
        /// Gets registration group with the specified ID.
        /// </summary>
        /// <param name="regGroupId">The ID of the registration group.</param>
        /// <returns>Returns a DTO of the specified registration group.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("regGroups/get/byId/{regGroupId:int}")]
        public PastoralRegGroupDto GetRegGroupById([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupById(regGroupId, _context));
        }

        /// <summary>
        /// Gets a list of registration groups from the specified year group.
        /// </summary>
        /// <param name="yearGroupId">The ID of the year group to get registration groups.</param>
        /// <returns>Returns a list of DTOs of registration groups from the specified year group.</returns>
        [HttpGet]
        [Route("regGroups/get/byYearGroup/{yearGroupId:int}")]
        public IEnumerable<PastoralRegGroupDto> GetRegGroupsByYearGroup([FromUri] int yearGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.GetRegGroupsByYearGroup(yearGroupId, _context));
        }

        /// <summary>
        /// Gets a list of all registration groups.
        /// </summary>
        /// <returns>Returns a list of DTOs of all registration groups.</returns>
        [HttpGet]
        [Route("regGroups/get/all")]
        public IEnumerable<PastoralRegGroupDto> GetAllRegGroups()
        {
            return PrepareResponseObject(PastoralProcesses.GetAllRegGroups(_context));
        }

        /// <summary>
        /// Checks whether a a registration group has any assigned students.
        /// </summary>
        /// <param name="regGroupId">The ID of the registration group to check.</param>
        /// <returns>Returns true if the registration group contains students.</returns>
        /// <exception cref="HttpResponseException">Thrown when the registration group is not found.</exception>
        [HttpGet]
        [Route("regGroups/hasStudents/{regGroupId:int}")]
        public bool RegGroupHasStudents([FromUri] int regGroupId)
        {
            return PrepareResponseObject(PastoralProcesses.RegGroupContainsStudents(regGroupId, _context));
        }

        /// <summary>
        /// Updates the specified registration group.
        /// </summary>
        /// <param name="regGroup">The registration group to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
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
