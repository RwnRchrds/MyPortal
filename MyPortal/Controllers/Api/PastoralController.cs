using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models.Database;

namespace MyPortal.Controllers.Api
{
    public class PastoralController : MyPortalApiController
    {
        #region Reg Groups
        /// <summary>
        /// Creates registration group.
        /// </summary>
        /// <param name="regGroup">The registration group to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/regGroups/create")]
        public IHttpActionResult CreateRegGroup(PastoralRegGroup regGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.PastoralRegGroups.Add(regGroup);
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
            var regGroupInDb = _context.PastoralRegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            _context.PastoralRegGroups.Remove(regGroupInDb);
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
        public PastoralRegGroupDto GetRegGroupById(int id)
        {
            var regGroup = _context.PastoralRegGroups.SingleOrDefault(x => x.Id == id);

            if (regGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>(regGroup);
        }

        /// <summary>
        /// Gets a list of registration groups from the specified year group.
        /// </summary>
        /// <param name="yearGroupId">The ID of the year group to get registration groups.</param>
        /// <returns>Returns a list of DTOs of registration groups from the specified year group.</returns>
        [HttpGet]
        [Route("api/regGroups/byYearGroup/{yearGroupId}")]
        public IEnumerable<PastoralRegGroupDto> GetRegGroupsByYearGroup(int yearGroupId)
        {
            return _context.PastoralRegGroups
                .Where(x => x.YearGroupId == yearGroupId)
                .ToList()
                .Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
        }

        /// <summary>
        /// Gets a list of all registration groups.
        /// </summary>
        /// <returns>Returns a list of DTOs of all registration groups.</returns>
        [HttpGet]
        [Route("api/regGroups/all")]
        public IEnumerable<PastoralRegGroupDto> GetRegGroups()
        {
            return _context.PastoralRegGroups.ToList().OrderBy(x => x.Name).Select(Mapper.Map<PastoralRegGroup, PastoralRegGroupDto>);
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
            var regGroupInDb = _context.PastoralRegGroups.SingleOrDefault(x => x.Id == id);

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
        public IHttpActionResult UpdateRegGroup(PastoralRegGroup regGroup)
        {
            var regGroupInDb = _context.PastoralRegGroups.SingleOrDefault(x => x.Id == regGroup.Id);

            if (regGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Reg group not found");
            }

            regGroupInDb.Name = regGroup.Name;
            regGroupInDb.TutorId = regGroup.TutorId;

            _context.SaveChanges();

            return Ok("Reg group updated");
        }
        #endregion

        #region Year Groups
        [HttpPost]
        [Route("api/yearGroups/create")]
        public IHttpActionResult CreateYearGroup(PastoralYearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            _context.PastoralYearGroups.Add(yearGroup);
            return Ok("Year group added");
        }

        [HttpDelete]
        [Route("api/yearGroups/delete/{id}")]
        public IHttpActionResult DeleteYearGroup(int id)
        {
            var yearGroupInDb = _context.PastoralYearGroups.SingleOrDefault(x => x.Id == id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            _context.PastoralYearGroups.Remove(yearGroupInDb);
            _context.SaveChanges();

            return Ok("Year group deleted");
        }

        [HttpGet]
        [Route("api/yearGroups/fetch")]
        public IEnumerable<PastoralYearGroupDto> GetYearGroups()
        {
            return _context.PastoralYearGroups
                .OrderBy(x => x.Id)
                .ToList()
                .Select(Mapper.Map<PastoralYearGroup, PastoralYearGroupDto>);
        }

        [HttpPost]
        [Route("api/yearGroups/update")]
        public IHttpActionResult UpdateYearGroup(PastoralYearGroup yearGroup)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var yearGroupInDb = _context.PastoralYearGroups.SingleOrDefault(x => x.Id == yearGroup.Id);

            if (yearGroupInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Year group not found");
            }

            yearGroupInDb.Name = yearGroup.Name;
            yearGroupInDb.HeadId = yearGroup.HeadId;

            _context.SaveChanges();

            return Ok("Year group updated");
        }

        #endregion
    }
}
