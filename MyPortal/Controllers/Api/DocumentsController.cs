using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Processes;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/documents")]
    public class DocumentsController : MyPortalApiController
    {
        /// <summary>
        /// Adds a general document to the database.
        /// </summary>
        /// <param name="document">The document to add to the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult AddDocument([FromBody] Document document)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(DocumentProcesses.CreateDocument(document, userId, _context));
        }

        /// <summary>
        /// Gets approved documents only from the database
        /// </summary>
        /// <returns>Returns a list of DTOs of approved documents.</returns>
        [HttpGet]
        [Route("general/approved")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<DocumentDto> GetApprovedGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments(_context));
        }

        /// <summary>
        /// Gets the document with the specified ID from the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to fetch.</param>
        /// <returns>Returns a DTO of the specified document.</returns>
        /// <exception cref="HttpResponseException">Thrown when document is not found.</exception>
        [HttpGet]
        [Route("get/byId/{documentId:int}")]
        public DocumentDto GetDocumentById([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetDocumentById(documentId, _context));
        }

        /// <summary>
        /// Gets all documents from the database, including those that are unapproved.
        /// </summary>
        /// <returns>Returns a list of DTOs of all documents.</returns>
        [HttpGet]
        [Route("general/all")]
        [Authorize(Roles = "SeniorStaff")]
        public IEnumerable<DocumentDto> GetAllGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments(_context));
        }

        [HttpPost]
        [Route("personal/get/dataGrid/{personId}")]
        [Authorize]
        public IHttpActionResult GetDocumentsForPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetPersonalDocumentsForDataGrid(personId, _context));

            return PrepareDataGridObject(documents, dm);
        }

        /// <summary>
        /// Deletes the specified document from the database.
        /// </summary>
        /// <param name="documentId">The ID of the document to delete.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("delete/{documentId:int}")]
        public IHttpActionResult RemoveDocument([FromUri] int documentId)
        {
            return PrepareResponse(DocumentProcesses.DeleteDocument(documentId, _context));
        }

        /// <summary>
        /// Updates the document in the database.
        /// </summary>
        /// <param name="document">The document to update in the database.</param>
        /// <returns>Returns NegotiatedContentResult stating whether the action was successful.</returns>
        [HttpPost]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("update")]
        public IHttpActionResult UpdateDocument([FromBody] Document document)
        {
            return PrepareResponse(DocumentProcesses.UpdateDocument(document, _context));
        }

        [HttpPost]
        [Route("personal/create")]
        public IHttpActionResult AddDocument([FromBody] PersonDocument document)
        {
            var uploaderId = User.Identity.GetUserId();
            return PrepareResponse(DocumentProcesses.CreatePersonalDocument(document, uploaderId, _context));
        }

        [HttpGet]
        [Route("personal/get/byId/{documentId:int}")]
        public PersonDocumentDto GetPersonalDocument([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocumentById(documentId, _context));
        }

        [HttpGet]
        [Route("personal/get/{personId:int}")]
        public IEnumerable<PersonDocumentDto> GetPersonalDocumentsForPerson([FromUri] int personId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocuments(personId, _context));
        }

        [HttpDelete]
        [Route("personal/delete/{documentId:int}")]
        public IHttpActionResult DeletePersonalDocument([FromUri] int documentId)
        {
            return PrepareResponse(DocumentProcesses.DeletePersonalDocument(documentId, _context));
        }

        [HttpPost]
        [Route("personal/update")]
        public IHttpActionResult UpdateDocument([FromBody] PersonDocument document)
        {
            return PrepareResponse(DocumentProcesses.UpdatePersonalDocument(document, _context));
        }
    }
}