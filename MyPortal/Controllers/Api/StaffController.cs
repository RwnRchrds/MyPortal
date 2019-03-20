using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    public class StaffController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public StaffController()
        {
            _context = new MyPortalDbContext();
        }

        /// <summary>
        /// Adds a document and attaches it to the specified staff member.
        /// </summary>
        /// <param name="data">The StaffDocument object to add.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/documents/add")]
        public IHttpActionResult AddDocument(StaffDocument data)
        {
            var staff = _context.Staff.SingleOrDefault(x => x.Id == data.StaffId);

            var uploaderId = User.Identity.GetUserId();

            var uploader = _context.Staff.SingleOrDefault(x => x.UserId == uploaderId);

            if (staff == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff not found");
            }

            if (uploader == null)
            {
                return Content(HttpStatusCode.BadRequest, "Uploader not found");
            }

            data.Document.IsGeneral = false;

            data.Document.Approved = true;

            data.Document.Date = DateTime.Now;

            data.Document.UploaderId = uploader.Id;

            var isUriValid = Uri.TryCreate(data.Document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            var staffDocument = data;

            var document = staffDocument.Document;

            _context.Documents.Add(document);
            _context.StaffDocuments.Add(staffDocument);

            _context.SaveChanges();

            return Ok("Document added");
        }

        /// <summary>
        /// Adds a staff observation for the specified staff member.
        /// </summary>
        /// <param name="data">The staff observation object to add.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/observations/add")]
        public IHttpActionResult AddObservation(StaffObservation data)
        {
            data.Date = DateTime.Now;

            var currentUserId = User.Identity.GetUserId();

            var userPerson = _context.Staff.SingleOrDefault(x => x.UserId == currentUserId);

            var observer = _context.Staff.SingleOrDefault(x => x.Id == data.ObserverId);

            var observee = _context.Staff.Single(x => x.Id == data.ObserveeId);

            if (observee == null || observer == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            if (observee.Id == userPerson.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot add an observation for yourself");
            }

            data.ObserverId = observer.Id;

            var observationToAdd = data;

            _context.StaffObservations.Add(observationToAdd);
            _context.SaveChanges();

            return Ok("Observation added");
        }

        /// <summary>
        /// Adds a staff member.
        /// </summary>
        /// <param name="staffDto">The DTO of the staff member to add.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/new")]
        public IHttpActionResult CreateStaff(Staff staffDto)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid data");
            }

            var staff = staffDto;
            _context.Staff
                .Add(staff);

            _context.SaveChanges();

            staffDto.Id = staff.Id;

            return Ok("Staff member added");
        }

        /// <summary>
        /// Deletes the specified staff member.
        /// </summary>
        /// <param name="staffId">The ID of the staff member to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/staff/delete/{staffId}")]
        public IHttpActionResult DeleteStaff(int staffId)
        {
            if (_context.Subjects.Any(x => x.LeaderId == staffId))
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete a subject leader");
            }

            if (_context.YearGroups.Any(x => x.HeadId == staffId))
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete a head of year");
            }

            if (_context.RegGroups.Any(x => x.TutorId == staffId))
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete a reg tutor");
            }

            var staffInDb = _context.Staff.Single(x => x.Id == staffId);

            if (staffInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            if (staffInDb.UserId == User.Identity.GetUserId())
            {
                return Content(HttpStatusCode.BadRequest, "Cannot delete the current user");
            }

            var logsWritten = _context.Logs.Where(x => x.AuthorId == staffId);

            var ownCertificates = _context.TrainingCertificates.Where(x => x.StaffId == staffId);

            var ownObservations = _context.StaffObservations.Where(x => x.ObserveeId == staffId);

            var observedObservations = _context.StaffObservations.Where(x => x.ObserverId == staffId);

            var ownDocuments = _context.StaffDocuments.Where(x => x.StaffId == staffId);

            _context.Logs.RemoveRange(logsWritten);
            _context.TrainingCertificates.RemoveRange(ownCertificates);
            _context.StaffObservations.RemoveRange(ownObservations);
            _context.StaffObservations.RemoveRange(observedObservations);

            foreach (var document in ownDocuments)
            {
                var attachment = document.Document;

                _context.StaffDocuments.Remove(document);
                _context.Documents.Remove(attachment);
            }

            _context.Staff.Remove(staffInDb);
            _context.SaveChanges();

            return Ok("Staff member deleted");
        }

        /// <summary>
        /// Edits the specified staff member.
        /// </summary>
        /// <param name="data">The staff object to edit.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/edit")]
        public IHttpActionResult EditStaff(Staff data)
        {
            var staffInDb = _context.Staff.SingleOrDefault(x => x.Id == data.Id);

            if (staffInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            if (_context.Staff.Any(x => x.Code == data.Code) && staffInDb.Code != data.Code)
            {
                return Content(HttpStatusCode.BadRequest, "Staff code has already been used");
            }

            staffInDb.FirstName = data.FirstName;
            staffInDb.LastName = data.LastName;
            staffInDb.Title = data.Title;
            staffInDb.Code = data.Code;
            staffInDb.Email = data.Email;
            staffInDb.JobTitle = data.JobTitle;
            staffInDb.Phone = data.Phone;

            _context.SaveChanges();

            return Ok("Staff member updated");
        }

        /// <summary>
        /// Fetches the specified document.
        /// </summary>
        /// <param name="documentId">The ID of the document to fetch.</param>
        /// <returns>Returns a DTO of the staff document with the specified ID.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/staff/documents/document/{documentId}")]
        public StaffDocumentDto GetDocument(int documentId)
        {
            var document = _context.StaffDocuments.SingleOrDefault(x => x.Id == documentId);

            if (document == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<StaffDocument, StaffDocumentDto>(document);
        }

        // --[STAFF DOCUMENTS]--

        /// <summary>
        /// Fetches documents for the specified staff member.
        /// </summary>
        /// <param name="staffId">The ID of the staff member to fetch.</param>
        /// <returns>Returns a list of DTOs of documents for the specified staff member.</returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/staff/documents/fetch/{staffId}")]
        public IEnumerable<StaffDocumentDto> GetDocuments(int staffId)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == staffId);

            if (staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var documents = _context.StaffDocuments
                .Where(x => x.StaffId == staffId)
                .ToList()
                .Select(Mapper.Map<StaffDocument, StaffDocumentDto>);

            return documents;
        }

        /// <summary>
        /// Gets the staff observation with the specified ID.
        /// </summary>
        /// <param name="observationId"></param>
        /// <returns>Returns a DTO of the staff observation with the specified ID.</returns>
        /// <exception cref="HttpResponseException">Thrown when staff member is not found.</exception>
        [HttpGet]
        [Route("api/staff/observations/observation/{observationId}")]
        public StaffObservationDto GetObservation(int observationId)
        {
            var observation = _context.StaffObservations.SingleOrDefault(x => x.Id == observationId);

            if (observation == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<StaffObservation, StaffObservationDto>(observation);
        }        

        /// <summary>
        /// Gets all observations for the specified staff member.
        /// </summary>
        /// <param name="staffId">The ID of the staff member to fetch observations for.</param>
        /// <returns>Returns a list of DTOs of observations for the specified staff member.</returns>
        /// <exception cref="HttpResponseException">Thrown when staff member is not found.</exception>
        [HttpGet]
        [Route("api/staff/observations/fetch/{staffId}")]
        public IEnumerable<StaffObservationDto> GetObservations(int staffId)
        {
            var staff = _context.Staff.Single(x => x.Id == staffId);

            if (staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var observations = _context.StaffObservations
                .Where(x => x.ObserveeId == staffId)
                .ToList()
                .Select(Mapper.Map<StaffObservation, StaffObservationDto>);

            return observations;
        }

        /// <summary>
        /// Gets a list of all staff.
        /// </summary>
        /// <returns>Returns a list of DTOs of all staff.</returns>
        [Route("api/staff/fetch")]
        public IEnumerable<StaffDto> GetStaff()
        {
            return _context.Staff
                .OrderBy(x => x.LastName)
                .ToList()
                .Select(Mapper.Map<Staff, StaffDto>);
        }        

        /// <summary>
        /// Gets the staff member with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the staff member to fetch.</param>
        /// <returns>Returns a DTO of the specified staff member.</returns>
        /// <exception cref="HttpResponseException">Thrown when the staff member is not found.</exception>
        [Route("api/staff/fetch/{id}")]
        public StaffDto GetStaffMember(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Code == id);

            if (staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Staff, StaffDto>(staff);
        }

        /// <summary>
        /// Deletes the specified document.
        /// </summary>
        /// <param name="documentId">The ID of the document to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/staff/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var staffDocument = _context.StaffDocuments.Single(x => x.Id == documentId);

            if (staffDocument == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                return Content(HttpStatusCode.BadRequest, "No document attached");
            }

            _context.StaffDocuments.Remove(staffDocument);

            _context.Documents.Remove(attachedDocument);

            _context.SaveChanges();

            return Ok("Document deleted");
        }

        /// <summary>
        /// Deletes the specified observation.
        /// </summary>
        /// <param name="observationId">The ID of the observation to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Route("api/staff/observations/remove/{observationId}")]
        public IHttpActionResult RemoveObservation(int observationId)
        {
            var observationToRemove = _context.StaffObservations.Single(x => x.Id == observationId);

            var currentUserId = User.Identity.GetUserId();

            var userStaffProfile = _context.Staff.SingleOrDefault(x => x.UserId == currentUserId);

            if (userStaffProfile == null)
            {
                return Content(HttpStatusCode.NotFound, "Current user profile not found");
            }

            if (observationToRemove == null)
            {
                return Content(HttpStatusCode.NotFound, "Observation not found");
            }

            if (observationToRemove.ObserveeId == userStaffProfile.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot remove an observation for yourself");
            }

            _context.StaffObservations.Remove(observationToRemove);
            _context.SaveChanges();

            return Ok("Observation removed");
        }

        /// <summary>
        /// Checks whether a staff member has any documents.
        /// </summary>
        /// <param name="id">The ID of the staff member to check.</param>
        /// <returns>Returns true if staff member has documents.</returns>
        /// <exception cref="HttpResponseException">Thrown when the staff member is not found.</exception>
        [HttpGet]
        [Route("api/staff/hasDocuments/{id}")]
        public bool StaffHasDocuments(int id)
        {
            var staffInDb = _context.Staff.SingleOrDefault(x => x.Id == id);

            if (staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return staffInDb.StaffDocuments.Any();
        }

        /// <summary>
        /// Checks whether a staff member has written any logs.
        /// </summary>
        /// <param name="id">The ID of the staff member to check.</param>
        /// <returns>Returns true if staff member has logs.</returns>
        /// <exception cref="HttpResponseException">Thrown when the staff member is not found.</exception>
        [HttpGet]
        [Route("api/staff/hasLogs/{id}")]
        public bool StaffHasLogs(int id)
        {
            var staffInDb = _context.Staff.SingleOrDefault(x => x.Id == id);

            if (staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return staffInDb.Logs.Any();
        }

        /// <summary>
        /// Updates the specified document.
        /// </summary>
        /// <param name="data">The document object to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/documents/edit")]
        public IHttpActionResult UpdateDocument(StaffDocument data)
        {
            var staffDocumentInDb = _context.StaffDocuments.Single(x => x.Id == data.Id);

            if (staffDocumentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Upload not found");
            }

            var isUriValid = Uri.TryCreate(data.Document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            staffDocumentInDb.Document.Description = data.Document.Description;
            staffDocumentInDb.Document.Url = data.Document.Url;
            staffDocumentInDb.Document.IsGeneral = false;
            staffDocumentInDb.Document.Approved = true;

            _context.SaveChanges();

            return Ok("Document updated");
        }

        /// <summary>
        /// Updates the specified observation
        /// </summary>
        /// <param name="data">The staff observation object to update.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/staff/observations/update")]
        public IHttpActionResult UpdateObservation(StaffObservation data)
        {
            var observationInDb = _context.StaffObservations.Single(x => x.Id == data.Id);

            var currentUserId = User.Identity.GetUserId();

            var observer = _context.Staff.SingleOrDefault(x => x.Id == data.ObserverId);

            var userStaffProfile = _context.Staff.SingleOrDefault(x => x.UserId == currentUserId);

            if (userStaffProfile == null)
            {
                return Content(HttpStatusCode.NotFound, "Current user profile not found");
            }

            if (observationInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Observation not found");
            }

            if (observationInDb.ObserveeId == userStaffProfile.Id)
            {
                return Content(HttpStatusCode.BadRequest, "Cannot modify an observation for yourself");
            }

            observationInDb.Outcome = data.Outcome;
            observationInDb.ObserverId = observer.Id;

            _context.SaveChanges();

            return Ok("Observation updated");
        }
    }
}