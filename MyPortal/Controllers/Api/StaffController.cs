using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Misc;

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

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public IEnumerable<StaffDto> GetStaff()
        {
            return _context.Staff
                .ToList()
                .Select(Mapper.Map<Staff, StaffDto>);
        }

        public StaffDto GetStaffMember(string id)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staff == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Staff, StaffDto>(staff);
        }

        [HttpPost]
        public StaffDto CreateStaff(StaffDto staffDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var staff = Mapper.Map<StaffDto, Staff>(staffDto);
            _context.Staff
                .Add(staff);

            _context.SaveChanges();

            staffDto.Id = staff.Id;

            return staffDto;
        }

        [HttpGet]
        [Route("api/staff/documents/fetch/{staffId}")]
        public IEnumerable<StaffDocumentDto> GetDocuments(string staffId)
        {
            var staff = _context.Staff.SingleOrDefault(s => s.Id == staffId);

            if (staff == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var documents = _context.StaffDocuments
                .Where(x => x.Staff == staffId)
                .ToList()
                .Select(Mapper.Map<StaffDocument, StaffDocumentDto>);

            return documents;
        }

        [HttpPost]
        [Route("api/staff/documents/add")]
        public IHttpActionResult AddDocument(StaffDocumentUpload data)
        {
            var staff = _context.Staff.SingleOrDefault(x => x.Id == data.StaffId);

            if (staff == null)
                return Content(HttpStatusCode.NotFound, "Staff not found");

            var document = data.Document;

            document.IsGeneral = false;

            document.Date = DateTime.Now;

            var isUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            _context.Documents.Add(document);
            _context.SaveChanges();

            var staffDocument = new StaffDocument()
            {
                Document = document.Id,
                Staff = data.StaffId
            };

            _context.StaffDocuments.Add(staffDocument);
            _context.SaveChanges();

            return Ok("Document added");
        }

        [HttpDelete]
        [Route("api/staff/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var staffDocument = _context.StaffDocuments.Single(x => x.Id == documentId);

            if (staffDocument == null)
                return Content(HttpStatusCode.NotFound, "Document not found");

            var attachedDocument = staffDocument.Document1;

            if (attachedDocument == null)
                return Content(HttpStatusCode.BadRequest, "No document attached");

            _context.StaffDocuments.Remove(staffDocument);

            _context.Documents.Remove(attachedDocument);

            _context.SaveChanges();

            return Ok("Document deleted");
        }

        [HttpPost]
        [Route("api/staff/documents/edit")]
        public IHttpActionResult UpdateDocument(DocumentDto data)
        {
            var documentInDb = _context.Documents.Single(x => x.Id == data.Id);

            if (documentInDb == null)
                return Content(HttpStatusCode.NotFound, "Upload not found");

            var isUriValid = Uri.TryCreate(data.Url, UriKind.Absolute, out var uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            documentInDb.Description = data.Description;
            documentInDb.Url = data.Url;
            documentInDb.IsGeneral = false;

            _context.SaveChanges();

            return Ok("Document updated");
        }
    }
}