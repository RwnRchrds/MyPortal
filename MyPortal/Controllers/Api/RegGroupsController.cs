using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class RegGroupsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public RegGroupsController()
        {
            _context = new MyPortalDbContext();
        }

        public RegGroupsController(MyPortalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates registration group.
        /// </summary>
        /// <param name="regGroup">The registration group to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/regGroups/create")]
        public IHttpActionResult CreateRegGroup(RegGroup regGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.RegGroups.Add(regGroup);
            _context.SaveChanges();

            return Ok("Reg group created");
        }

        /// <summary>
        /// Deletes the specified registration group.
        /// </summary>
        /// <param name="id">The ID of the registration group to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/regGroups/delete/{id}")]
        public IHttpActionResult DeleteRegGroup(int id)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            _context.RegGroups.Remove(regGroupInDb);
            _context.SaveChanges();

            return Ok("Reg group deleted");
        }

        /// <summary>
        /// Gets registration group with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the registration group.</param>
        /// <returns>Returns a DTO of the specified registration group.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/regGroups/byId/{id}")]
        public RegGroupDto GetRegGroupById(int id)
        {
            var regGroup = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<RegGroup, RegGroupDto>(regGroup);
        }

        /// <summary>
        /// Gets a list of registration groups from the specified year group.
        /// </summary>
        /// <param name="yearGroup">The ID of the year group to get registration groups.</param>
        /// <returns>Returns a list of DTOs of registration groups from the specified year group.</returns>
        [HttpGet]
        [Route("api/regGroups/byYearGroup/{yearGroup}")]
        public IEnumerable<RegGroupDto> GetRegGroupsByYearGroup(int yearGroup)
        {
            return _context.RegGroups
                .Where(x => x.YearGroupId == yearGroup)
                .ToList()
                .Select(Mapper.Map<RegGroup, RegGroupDto>);
        }

        /// <summary>
        /// Gets a list of all registration groups.
        /// </summary>
        /// <returns>Returns a list of DTOs of all registration groups.</returns>
        [HttpGet]
        [Route("api/regGroups/all")]
        public IEnumerable<RegGroupDto> GetRegGroups()
        {
            return _context.RegGroups.ToList().Select(Mapper.Map<RegGroup, RegGroupDto>);
        }

        /// <summary>
        /// Checks whether a a registration group has any assigned students.
        /// </summary>
        /// <param name="id">The ID of the registration group to check.</param>
        /// <returns>Returns true if the registration group contains students.</returns>
        /// <exception cref="HttpResponseException">Thrown when the registration group is not found.</exception>
        [HttpGet]
        [Route("api/regGroups/hasStudents/{id}")]
        public bool RegGroupHasStudents(int id)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroupInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return regGroupInDb.Students.Any();
        }

        /// <summary>
        /// Updates the specified registration group.
        /// </summary>
        /// <param name="regGroup">The registration group to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/regGroups/update")]
        public IHttpActionResult UpdateRegGroup(RegGroup regGroup)
        {
            var regGroupInDb = _context.RegGroups.SingleOrDefault(x => x.Id == regGroup.Id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            _context.SaveChanges();

            return Ok("Reg group updated");
        }
    }
}