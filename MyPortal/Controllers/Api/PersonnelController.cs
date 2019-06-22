using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal.Controllers.Api
{
    public class PersonnelController : MyPortalApiController
    {
        #region Training Certificates
        [HttpPost]
        [Route("api/staff/certificates/create")]
        public IHttpActionResult CreateTrainingCertificate(PersonnelTrainingCertificate trainingCertificateDto)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var userId = User.Identity.GetUserId();

            var userPerson = PeopleProcesses.GetStaffFromUserId(userId, _context);

            if (trainingCertificateDto.StaffId == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot add a certificate for yourself");
            }

            var cert = trainingCertificateDto;

            _context.PersonnelTrainingCertificates.Add(cert);
            _context.SaveChanges();

            return Ok("Certificate added");
        }

        [HttpDelete]
        [Route("api/staff/certificates/delete/{staff}/{course}")]
        public IHttpActionResult DeleteCertificate(int staff, int course)
        {
            var certInDb =
                _context.PersonnelTrainingCertificates.SingleOrDefault(l => l.StaffId == staff && l.CourseId == course);

            if (certInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Certificate not found");
            }

            var userId = User.Identity.GetUserId();

            var userPerson = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (staff == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot remove a certificate for yourself");
            }

            _context.PersonnelTrainingCertificates.Remove(certInDb);
            _context.SaveChanges();

            return Ok("Certificate deleted");
        }


        [HttpGet]
        [Route("api/staff/certificates/fetch/{staffId}/{courseId}")]
        public PersonnelTrainingCertificateDto GetCertificate(int staffId, int courseId)
        {
            var certInDb = _context.PersonnelTrainingCertificates.Single(x => x.StaffId == staffId && x.CourseId == courseId);

            if (certInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>(certInDb);
        }

        [HttpGet]
        [Route("api/staff/certificates/fetch/{staff}")]
        public IEnumerable<PersonnelTrainingCertificateDto> GetCertificates(int staff)
        {
            var staffInDb = _context.StaffMembers.Single(x => x.Id == staff);

            if (staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return _context.PersonnelTrainingCertificates
                .Where(c => c.StaffId == staff)
                .ToList()
                .Select(Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>);
        }

        [HttpPost]
        [Route("api/staff/certificates/update")]
        public IHttpActionResult UpdateCertificate(PersonnelTrainingCertificate data)
        {
            var certInDb =
                _context.PersonnelTrainingCertificates.Single(x => x.StaffId == data.StaffId && x.CourseId == data.CourseId);

            if (certInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userId = User.Identity.GetUserId();

            var userPerson = _context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (userPerson != null && data.StaffId == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify a certificate for yourself");
            }

            certInDb.StatusId = data.StatusId;

            _context.SaveChanges();

            return Ok("Certificate updated");
        }

        #endregion

        #region Training Courses
        [HttpDelete]
        [Route("api/courses/remove/{courseId}")]
        public IHttpActionResult DeleteCourse(int courseId)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Training course not found");
            }

            if (courseInDb.PersonnelTrainingCertificates.Any())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete course that has issued certificates");
            }

            _context.PersonnelTrainingCourses.Remove(courseInDb);
            _context.SaveChanges();

            return Ok("Training course deleted");
        }


        [HttpGet]
        [Route("api/courses/fetch/{courseId}")]
        public PersonnelTrainingCourseDto GetCourse(int courseId)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>(courseInDb);
        }

        [HttpGet]
        [Route("api/courses")]
        public IEnumerable<PersonnelTrainingCourseDto> GetCourses()
        {
            return _context.PersonnelTrainingCourses
                .ToList()
                .Select(Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>);
        }

        [HttpPost]
        [Route("api/courses/edit")]
        public IHttpActionResult UpdateCourse(PersonnelTrainingCourse course)
        {
            var courseInDb = _context.PersonnelTrainingCourses.Single(x => x.Id == course.Id);

            if (courseInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Training course not found");
            }

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            _context.SaveChanges();

            return Ok("Training course updated");
        }
        #endregion
    }
}
