using System.Collections.Generic;
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
        [Route("create")]
        public IHttpActionResult AddDocument([FromBody] Document document)
        {
            var userId = User.Identity.GetUserId();
            return PrepareResponse(DocumentProcesses.CreateDocument(document, userId, _context));
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/approved")]
        public IEnumerable<DocumentDto> GetApprovedGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/dataGrid/approved")]
        public IHttpActionResult GetApprovedGeneralDocumentsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetApprovedGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("get/byId/{documentId:int}")]
        public DocumentDto GetDocumentById([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetDocumentById(documentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/all")]
        public IEnumerable<DocumentDto> GetAllGeneralDocuments()
        {
            return PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments(_context));
        }

        [HttpPost]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/dataGrid/all")]
        public IHttpActionResult GetAllGeneralDocumentsForDataGrid([FromBody] DataManagerRequest dm)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetAllGeneralDocuments_DataGrid(_context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpPost]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/dataGrid/{personId}")]
        public IHttpActionResult GetDocumentsByPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            var documents = PrepareResponseObject(DocumentProcesses.GetPersonalDocuments_DataGrid(personId, _context));

            return PrepareDataGridObject(documents, dm);
        }

        [HttpDelete]
        [RequiresPermission("EditDocuments")]
        [Route("delete/{documentId:int}")]
        public IHttpActionResult RemoveDocument([FromUri] int documentId)
        {
            return PrepareResponse(DocumentProcesses.DeleteDocument(documentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditDocuments")]
        [Route("update")]
        public IHttpActionResult UpdateDocument([FromBody] Document document)
        {
            return PrepareResponse(DocumentProcesses.UpdateDocument(document, _context));
        }

        [HttpPost]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/create")]
        public IHttpActionResult AddDocument([FromBody] PersonDocument document)
        {
            var uploaderId = User.Identity.GetUserId();
            return PrepareResponse(DocumentProcesses.CreatePersonalDocument(document, uploaderId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/byId/{documentId:int}")]
        public PersonDocumentDto GetPersonalDocument([FromUri] int documentId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocumentById(documentId, _context));
        }

        [HttpGet]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/{personId:int}")]
        public IEnumerable<PersonDocumentDto> GetPersonalDocumentsForPerson([FromUri] int personId)
        {
            return PrepareResponseObject(DocumentProcesses.GetPersonalDocuments(personId, _context));
        }

        [HttpDelete]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/delete/{documentId:int}")]
        public IHttpActionResult DeletePersonalDocument([FromUri] int documentId)
        {
            return PrepareResponse(DocumentProcesses.DeletePersonalDocument(documentId, _context));
        }

        [HttpPost]
        [RequiresPermission("EditPersonalDocuments")]
        [Route("personal/update")]
        public IHttpActionResult UpdateDocument([FromBody] PersonDocument document)
        {
            return PrepareResponse(DocumentProcesses.UpdatePersonalDocument(document, _context));
        }
    }
}