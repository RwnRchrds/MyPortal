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
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IHttpActionResult AddDocument([FromBody] Document document)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(DocumentProcesses.CreateDocument(document, userId, _context));
        }
        
        [HttpGet]
        [Route("general/get/approved")]
        [Authorize(Roles = "Staff, SeniorStaff")]
        public IEnumerable<DocumentDto> GetApprovedGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments(_context));
        }

        [HttpPost]
        [Route("general/get/dataGrid/approved")]
        [Authorize(Roles = "Staff")]
        public IHttpActionResult GetApprovedGeneralDocumentsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }
        
        [HttpGet]
        [Route("get/byId/{documentId:int}")]
        public DocumentDto GetDocumentById([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetDocumentById(documentId, _context));
        }

        [HttpGet]
        [Route("general/get/all")]
        [Authorize(Roles = "SeniorStaff")]
        public IEnumerable<DocumentDto> GetAllGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments(_context));
        }

        [HttpPost]
        [Route("general/get/dataGrid/all")]
        [Authorize(Roles = "SeniorStaff")]
        public IHttpActionResult GetAllGeneralDocumentsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpPost]
        [Route("personal/get/dataGrid/{personId}")]
        [Authorize]
        public IHttpActionResult GetDocumentsByPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetPersonalDocuments_DataGrid(personId, _context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpDelete]
        [Authorize(Roles = "Staff, SeniorStaff")]
        [Route("delete/{documentId:int}")]
        public IHttpActionResult RemoveDocument([FromUri] int documentId)
        {
            return PrepareResponse(DocumentProcesses.DeleteDocument(documentId, _context));
        }

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