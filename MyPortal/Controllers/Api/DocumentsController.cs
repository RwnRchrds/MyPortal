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
    public class DocumentsController : ApiController
    {
        private readonly MyPortalDbContext _context;

        public DocumentsController()
        {
            _context = new MyPortalDbContext();
        }

        public DocumentsController(MyPortalDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("api/documents/add")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult AddDocument(Document document)
        {
            var IsUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!IsUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            var uploader = new Staff();

            var uploaderId = document.UploaderId;

            if (uploaderId == 0)
            {
                var userId = User.Identity.GetUserId();
                uploader = _context.Staff.SingleOrDefault(x => x.UserId == userId);
                if (uploader == null)
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
            }

            if (uploaderId != 0) uploader = _context.Staff.SingleOrDefault(x => x.Id == uploaderId);

            if (uploader == null) return Content(HttpStatusCode.NotFound, "Staff member not found");

            document.UploaderId = uploader.Id;

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            _context.Documents.Add(document);
            _context.SaveChanges();

            return Ok("Document added");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Gets approved general documents only
        [HttpGet]
        [Route("api/documents/approved")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<DocumentDto> GetApprovedDocuments()
        {
            return _context.Documents
                .Where(x => x.IsGeneral && x.Approved)
                .ToList()
                .Select(Mapper.Map<Document, DocumentDto>);
        }

        [HttpGet]
        [Route("api/documents/document/{documentId}")]
        public DocumentDto GetDocument(int documentId)
        {
            var document = _context.Documents
                .Single(x => x.Id == documentId);

            if (document == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);   
            }                

            return Mapper.Map<Document, DocumentDto>(document);
        }

        //Returns all (general) documents --> Including unapproved
        [HttpGet]
        [Route("api/documents")]
        [Authorize(Roles = "SeniorStaff")]
        public IEnumerable<DocumentDto> GetDocuments()
        {
            return _context.Documents
                .Where(x => x.IsGeneral)
                .ToList()
                .Select(Mapper.Map<Document, DocumentDto>);
        }

        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var documentToRemove = _context.Documents.SingleOrDefault(x => x.Id == documentId);

            if (documentToRemove == null)
                return Content(HttpStatusCode.NotFound, "Document not found");

            _context.Documents.Remove(documentToRemove);
            _context.SaveChanges();

            return Ok("Document removed");
        }

        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/documents/edit")]
        public IHttpActionResult UpdateDocument(Document data)
        {
            var documentInDb = _context.Documents.SingleOrDefault(x => x.Id == data.Id);

            if (documentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }                

            var isUriValid = Uri.TryCreate(data.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");

            documentInDb.Description = data.Description;
            documentInDb.Url = data.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = data.Approved;

            _context.SaveChanges();

            return Ok("Document updated");
        }
    }
}