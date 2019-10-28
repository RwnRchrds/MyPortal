using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Models.Database;
using MyPortal.Services;
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
                await DocumentService.CreateDocument(document, userId, _context);
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
                return await DocumentService.GetApprovedGeneralDocumentsDto(_context);
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
                var documents = await DocumentService.GetApprovedGeneralDocumentsDataGrid(_context);

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
                return await DocumentService.GetDocumentById(documentId, _context);
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
                return await DocumentService.GetAllGeneralDocumentsDto(_context);
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
                var documents = await DocumentService.GetAllGeneralDocumentsDataGrid(_context);

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
                var documents = await DocumentService.GetPersonalDocumentsDataGrid(personId, _context);

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
                await DocumentService.DeleteDocument(documentId, _context);
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
                await DocumentService.UpdateDocument(document, _context);
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
                await DocumentService.CreatePersonalDocument(document, uploaderId, _context);
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
                return await DocumentService.GetPersonalDocumentById(documentId, _context);
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
                return await DocumentService.GetPersonalDocuments(personId, _context);
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
                await DocumentService.DeletePersonalDocument(documentId, _context);
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
                await DocumentService.UpdatePersonalDocument(document, _context);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document updated");
        }
    }
}