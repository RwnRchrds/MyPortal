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
        public async Task<IEnumerable<DocumentDto>> GetApprovedGeneralDocuments()
        {
            try
            {
                return await DocumentProcesses.GetApprovedGeneralDocuments(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/dataGrid/approved", Name = "ApiDocumentsGetApprovedDocumentsDataGrid")]
        public async Task<IHttpActionResult> GetApprovedGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var documents = await DocumentProcesses.GetApprovedGeneralDocumentsDataGrid(_context);

                return PrepareDataGridObject(documents, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("get/byId/{documentId:int}", Name = "ApiDocumentsGetDocumentById")]
        public async Task<DocumentDto> GetDocumentById([FromUri] int documentId)
        {
            try
            {
                return await DocumentProcesses.GetDocumentById(documentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/all", Name = "ApiDocumentsGetAllDocuments")]
        public async Task<IEnumerable<DocumentDto>> GetAllGeneralDocuments()
        {
            try
            {
                return await DocumentProcesses.GetAllGeneralDocuments(_context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/dataGrid/all", Name = "ApiDocumentsGetAllDocumentsDataGrid")]
        public async Task<IHttpActionResult> GetAllGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var documents = await DocumentProcesses.GetAllGeneralDocumentsDataGrid(_context);

                return PrepareDataGridObject(documents, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/dataGrid/{personId}", Name = "ApiDocumentsGetPersonalDocumentsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetDocumentsByPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            try
            {
                var documents = await DocumentProcesses.GetPersonalDocumentsDataGrid(personId, _context);

                return PrepareDataGridObject(documents, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
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
        public async Task<IHttpActionResult> UpdateDocument([FromBody] Document document)
        {
            try
            {
                await DocumentProcesses.UpdateDocument(document, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Update document");
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
        public async Task<PersonDocumentDto> GetPersonalDocumentById([FromUri] int documentId)
        {
            try
            {
                return await DocumentProcesses.GetPersonalDocumentById(documentId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewPersonalDocuments")]
        [Route("personal/get/{personId:int}", Name = "ApiDocumentsGetPersonalDocumentsByPerson")]
        public async Task<IEnumerable<PersonDocumentDto>> GetPersonalDocumentsByPerson([FromUri] int personId)
        {
            try
            {
                return await DocumentProcesses.GetPersonalDocuments(personId, _context);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
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
        public async Task<IHttpActionResult> UpdatePersonalDocument([FromBody] PersonDocument document)
        {
            try
            {
                await DocumentProcesses.UpdatePersonalDocument(document, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document updated");
        }
    }
}