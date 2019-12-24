using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MyPortal.Attributes.Filters;
using MyPortal.Attributes.HttpAuthorise;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Services;
using Syncfusion.EJ2.Base;

namespace MyPortal.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/documents")]
    [ValidateModel]
    public class DocumentsController : MyPortalApiController
    {
        private readonly DocumentService _service;
        
        public DocumentsController()
        {
            _service = new DocumentService();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
            
        [HttpPost]
        [RequiresPermission("EditDocuments")]
        [Route("create", Name = "ApiCreateDocument")]
        public async Task<IHttpActionResult> CreateDocument([FromBody] DocumentDto document)
        {
            try
            {
                using (var staffService = new StaffMemberService())
                {
                    var userId = User.Identity.GetUserId();

                    var uploader = staffService.GetStaffMemberByUserId(userId);

                    document.UploaderId = uploader.Id;
                }

                await _service.CreateDocument(document);

                return Ok("Document created");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/approved", Name = "ApiGetApprovedDocuments")]
        public async Task<IEnumerable<DocumentDto>> GetApprovedGeneralDocuments()
        {
            try
            {
                return await _service.GetApprovedGeneralDocuments();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewApprovedDocuments")]
        [Route("general/get/dataGrid/approved", Name = "ApiGetApprovedDocumentsDataGrid")]
        public async Task<IHttpActionResult> GetApprovedGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var documents = await _service.GetApprovedGeneralDocuments();

                var list = documents.Select(_mapping.Map<DataGridDocumentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [RequiresPermission("ViewApprovedDocuments, ViewAllDocuments")]
        [Route("get/byId/{documentId:int}", Name = "ApiGetDocumentById")]
        public async Task<DocumentDto> GetDocumentById([FromUri] int documentId)
        {
            try
            {
                return await _service.GetDocumentById(documentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/all", Name = "ApiGetAllDocuments")]
        public async Task<IEnumerable<DocumentDto>> GetAllGeneralDocuments()
        {
            try
            {
                return await _service.GetAllGeneralDocuments();
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewAllDocuments")]
        [Route("general/get/dataGrid/all", Name = "ApiGetAllDocumentsDataGrid")]
        public async Task<IHttpActionResult> GetAllGeneralDocumentsDataGrid([FromBody] DataManagerRequest dm)
        {
            try
            {
                var documents = await _service.GetAllGeneralDocuments();

                var list = documents.Select(_mapping.Map<DataGridDocumentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("ViewPersonAttachments")]
        [Route("personal/get/dataGrid/{personId}", Name = "ApiGetAttachmentsByPersonDataGrid")]
        public async Task<IHttpActionResult> GetAttachmentsByPersonDataGrid([FromBody] DataManagerRequest dm, [FromUri] int personId)
        {
            try
            {
                var documents = await _service.GetPersonAttachments(personId);

                var list = documents.Select(_mapping.Map<DataGridPersonAttachmentDto>);

                return PrepareDataGridObject(list, dm);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditDocuments")]
        [Route("delete/{documentId:int}", Name = "ApiDeleteDocument")]
        public async Task<IHttpActionResult> DeleteDocument([FromUri] int documentId)
        {
            try
            {
                await _service.DeleteDocument(documentId);

                return Ok("Document deleted.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditDocuments")]
        [Route("update", Name = "ApiUpdateDocument")]
        public async Task<IHttpActionResult> UpdateDocument([FromBody] DocumentDto document)
        {
            try
            {
                await _service.UpdateDocument(document);

                return Ok("Document updated.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditPersonAttachments")]
        [Route("personal/create", Name = "ApiCreatePersonAttachment")]
        public async Task<IHttpActionResult> CreatePersonAttachment([FromBody] PersonAttachmentDto document)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _service.CreatePersonAttachment(document);

                return Ok("Document created.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewPersonAttachments")]
        [Route("personal/get/byId/{documentId:int}", Name = "ApiGetPersonAttachmentById")]
        public async Task<PersonAttachmentDto> GetPersonAttachmentById([FromUri] int documentId)
        {
            try
            {
                return await _service.GetPersonAttachmentById(documentId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpGet]
        [RequiresPermission("ViewPersonAttachments")]
        [Route("personal/get/{personId:int}", Name = "ApiGetAttachmentsByPerson")]
        public async Task<IEnumerable<PersonAttachmentDto>> GetAttachmentsByPerson([FromUri] int personId)
        {
            try
            {
                return await _service.GetPersonAttachments(personId);
            }
            catch (Exception e)
            {
                throw GetException(e);
            }
        }

        [HttpDelete]
        [RequiresPermission("EditPersonAttachments")]
        [Route("personal/delete/{documentId:int}", Name = "ApiDeletePersonAttachment")]
        public async Task<IHttpActionResult> DeletePersonAttachment([FromUri] int documentId)
        {
            try
            {
                await _service.DeletePersonAttachment(documentId);

                return Ok("Document deleted.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [RequiresPermission("EditPersonAttachments")]
        [Route("personal/update", Name = "ApiUpdatePersonAttachment")]
        public async Task<IHttpActionResult> UpdatePersonAttachment([FromBody] PersonAttachmentDto document)
        {
            try
            {
                await _service.UpdatePersonAttachment(document);

                return Ok("Attachment updated.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}