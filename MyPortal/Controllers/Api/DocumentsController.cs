using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Models.Attributes;
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
        [RequiresPermission("EditDocuments")]
        [Route("create", Name = "ApiDocumentsCreateDocument")]
        public async Task<IHttpActionResult> CreateDocument([FromBody] Document document)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                await DocumentProcesses.CreateDocument(document, userId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok( "Document created");
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/approved", Name = "ApiDocumentsGetApprovedDocuments")]
        public IEnumerable<DocumentDto> GetApprovedGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/dataGrid/approved", Name = "ApiDocumentsGetApprovedDocumentsDataGrid")]
        public IHttpActionResult GetApprovedGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("get/byId/{documentId:int}", Name = "ApiDocumentsGetDocumentById")]
        public DocumentDto GetDocumentById([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetDocumentById(documentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/all", Name = "ApiDocumentsGetAllDocuments")]
        public IEnumerable<DocumentDto> GetAllGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/dataGrid/all", Name = "ApiDocumentsGetAllDocumentsDataGrid")]
        public IHttpActionResult GetAllGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpPost]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/dataGrid/{personId}", Name = "ApiDocumentsGetPersonalDocumentsByPersonDataGrid")]
        public IHttpActionResult GetDocumentsByPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetPersonalDocuments_DataGrid(personId, _context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpDelete]
        [RequiresPermission("EditDocuments")]
        [Route("delete/{documentId:int}", Name = "ApiDocumentsDeleteDocument")]
        public async Task<IHttpActionResult> DeleteDocument([FromUri] int documentId)
        {
            try
            {
                await DocumentProcesses.DeleteDocument(documentId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document deleted");
        }

        [HttpPost]
        [RequiresPermission("EditDocuments")]
        [Route("update", Name = "ApiDocumentsUpdateDocument")]
        public IHttpActionResult UpdateDocument([FromBody] Document document)
        {
            return PrepareResponse(DocumentProcesses.UpdateDocument(document, _context));
        }

        [HttpPost]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/create", Name = "ApiDocumentsCreatePersonalDocument")]
        public async Task<IHttpActionResult> CreatePersonalDocument([FromBody] PersonDocument document)
        {
            var uploaderId = User.Identity.GetUserId();
            try
            {
                await DocumentProcesses.CreatePersonalDocument(document, uploaderId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document created");
        }

        [HttpGet]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/byId/{documentId:int}", Name = "ApiDocumentsGetPersonalDocumentById")]
        public PersonDocumentDto GetPersonalDocumentById([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocumentById(documentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/{personId:int}", Name = "ApiDocumentsGetPersonalDocumentsByPerson")]
        public IEnumerable<PersonDocumentDto> GetPersonalDocumentsByPerson([FromUri] int personId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocuments(personId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/delete/{documentId:int}", Name = "ApiDocumentsDeletePersonalDocument")]
        public async Task<IHttpActionResult> DeletePersonalDocument([FromUri] int documentId)
        {
            try
            {
                await DocumentProcesses.DeletePersonalDocument(documentId, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document deleted");
        }

        [HttpPost]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/update", Name = "ApiDocumentsUpdatePersonalDocument")]
        public IHttpActionResult UpdatePersonalDocument([FromBody] PersonDocument document)
        {
            return PrepareResponse(DocumentProcesses.UpdatePersonalDocument(document, _context));
        }
    }
}