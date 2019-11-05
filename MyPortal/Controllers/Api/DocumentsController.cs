using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.ApplicationInsights.WindowsServer;
using Microsoft.AspNet.Identity;
using MyPortal.Dtos;
using MyPortal.Attributes;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.Dtos.GridDtos;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/documents")]
    public class DocumentsController : MyPortalApiController
    {
        private readonly DocumentService _service;
        
        public DocumentsController()
        {
            _service = new DocumentService(UnitOfWork);
        }
            
        [HttpPost]
        [RequiresPermission("EditDocuments")]
        [Route("create", Name = "ApiDocumentsCreateDocument")]
        public async Task<IHttpActionResult> CreateDocument([FromBody] Document document)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreateDocument(document, userId);
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
                var documents = await _service.GetApprovedGeneralDocuments();

                return documents.Select(Mapper.Map<Document, DocumentDto>);
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
                var documents = await _service.GetApprovedGeneralDocuments();

                var list = documents.Select(Mapper.Map<Document, GridDocumentDto>);

                return PrepareDataGridObject(list, dm);
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
                var document = await _service.GetDocumentById(documentId);

                return Mapper.Map<Document, DocumentDto>(document);
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
                var documents = await _service.GetAllGeneralDocuments();

                return documents.Select(Mapper.Map<Document, DocumentDto>);
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
                var documents = await _service.GetAllGeneralDocuments();

                var list = documents.Select(Mapper.Map<Document, GridDocumentDto>);

                return PrepareDataGridObject(list, dm);
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
                var documents = await _service.GetPersonalDocuments(personId);

                var list = documents.Select(Mapper.Map<PersonDocument, GridPersonDocumentDto>);

                return PrepareDataGridObject(list, dm);
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
                await _service.DeleteDocument(documentId);
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
                await _service.UpdateDocument(document);
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
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreatePersonalDocument(document, userId);
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
                var document = await _service.GetPersonalDocumentById(documentId);

                return Mapper.Map<PersonDocument, PersonDocumentDto>(document);
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
                var documents = await _service.GetPersonalDocuments(personId);

                return documents.Select(Mapper.Map<PersonDocument, PersonDocumentDto>);
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
                await _service.DeletePersonalDocument(documentId);
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
                await _service.UpdatePersonalDocument(document);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }

            return Ok("Document updated");
        }
    }
}