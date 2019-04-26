using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

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

        /// <summary>
        /// Adds a general document to the database.
        /// </summary>
        /// <param name="document">The document to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("api/documents/add")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult AddDocument(CoreDocument document)
        {
            var IsUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!IsUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            var uploader = new CoreStaffMember();

            var uploaderId = document.UploaderId;

            if (uploaderId == 0)
            {
                var userId = User.Identity.GetUserId();
                uploader = _context.CoreStaff.SingleOrDefault(x => x.UserId == userId);
                if (uploader == null)
                {
                    return Content(HttpStatusCode.BadRequest, "User does not have a personnel profile");
                }
            }

            if (uploaderId != 0)
            {
                uploader = _context.CoreStaff.SingleOrDefault(x => x.Id == uploaderId);
            }

            if (uploader == null)
            {
                return Content(HttpStatusCode.NotFound, "Staff member not found");
            }

            document.UploaderId = uploader.Id;

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            _context.CoreDocuments.Add(document);
            _context.SaveChanges();

            return Ok("Document added");
        }

        /// <summary>
        /// Gets approved documents only from the database
        /// </summary>
        /// <returns>Returns a list of DTOs of approved documents.</returns>
        [HttpGet]
        [Route("api/documents/approved")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<CoreDocumentDto> GetApprovedDocuments()
        {
            return _context.CoreDocuments
                .Where(x => x.IsGeneral && x.Approved)
                .ToList()
                .Select(Mapper.Map<CoreDocument, CoreDocumentDto>);
        }

        /// <summary>
        /// Gets the document with the specified ID from the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to fetch.</param>
        /// <returns>Returns a DTO of the specified document.</returns>
        /// <exception cref="HttpResponseException">Thrown when document is not found.</exception>
        [HttpGet]
        [Route("api/documents/document/{documentId}")]
        public CoreDocumentDto GetDocument(int documentId)
        {
            var document = _context.CoreDocuments
                .Single(x => x.Id == documentId);

            if (document == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<CoreDocument, CoreDocumentDto>(document);
        }

        /// <summary>
        /// Gets all documents from the database, including those that are unapproved.
        /// </summary>
        /// <returns>Returns a list of DTOs of all documents.</returns>
        [HttpGet]
        [Route("api/documents")]
        [Authorize(Roles = "SeniorStaff")]
        public IEnumerable<CoreDocumentDto> GetDocuments()
        {
            return _context.CoreDocuments
                .Where(x => x.IsGeneral)
                .ToList()
                .Select(Mapper.Map<CoreDocument, CoreDocumentDto>);
        }

        /// <summary>
        /// Deletes the specified document from the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/documents/remove/{documentId}")]
        public IHttpActionResult RemoveDocument(int documentId)
        {
            var documentToRemove = _context.CoreDocuments.SingleOrDefault(x => x.Id == documentId);

            if (documentToRemove == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }

            _context.CoreDocuments.Remove(documentToRemove);
            _context.SaveChanges();

            return Ok("Document removed");
        }

        /// <summary>
        /// Updates the document in the database.
        /// </summary>
        /// <param name="data">The document to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("api/documents/edit")]
        public IHttpActionResult UpdateDocument(CoreDocument data)
        {
            var documentInDb = _context.CoreDocuments.SingleOrDefault(x => x.Id == data.Id);

            if (documentInDb == null)
            {
                return Content(HttpStatusCode.NotFound, "Document not found");
            }

            var isUriValid = Uri.TryCreate(data.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return Content(HttpStatusCode.BadRequest, "The URL entered is not valid");
            }

            documentInDb.Description = data.Description;
            documentInDb.Url = data.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = data.Approved;

            _context.SaveChanges();

            return Ok("Document updated");
        }
    }
}