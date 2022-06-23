using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;
using MyPortal.Logic.Models.Summary;
using MyPortal.Logic.Models.Web;
using MyPortal.Logic.Services;
using MyPortalWeb.Controllers.BaseControllers;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : BaseApiController
    {
        private IPersonService _personService;
        private IStaffMemberService _staffMemberService;
        private IDocumentService _documentService;
        private IFileService _fileService;

        public DocumentsController(IUserService userService, IRoleService roleService, IDocumentService documentService,
            IFileService fileService, IPersonService personService, IStaffMemberService staffMemberService)
            : base(userService, roleService)
        {
            _documentService = documentService;
            _fileService = fileService;
            _personService = personService;
            _staffMemberService = staffMemberService;
        }

        private async Task<bool> CanAccessDirectory(Guid directoryId, bool edit)
        {
            var user = await GetLoggedInUser();

            if (await _documentService.DirectoryIsRestricted(directoryId))
            {
                if (user.UserType != UserTypes.Staff)
                {
                    return false;
                }
            }

            if (await _documentService.DirectoryIsPublic(directoryId))
            {
                var publicPermission =
                    edit ? PermissionValue.SchoolEditSchoolDocuments : PermissionValue.SchoolViewSchoolDocuments;

                if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll, publicPermission))
                {
                    return true;
                }

                return false;
            }

            if (await _documentService.DirectoryIsPrivate(directoryId))
            {
                var dirOwner = await _personService.GetPersonWithTypesByDirectory(directoryId);

                if (dirOwner.PersonTypes.IsStaff)
                {
                    var allStaffPermission =
                        edit
                            ? PermissionValue.PeopleEditAllStaffDocuments
                            : PermissionValue.PeopleViewAllStaffDocuments;

                    var ownStaffPermission =
                        edit
                            ? PermissionValue.PeopleEditOwnStaffDocuments
                            : PermissionValue.PeopleViewOwnStaffDocuments;

                    var managedStaffPermission =
                        edit
                            ? PermissionValue.PeopleEditManagedStaffDocuments
                            : PermissionValue.PeopleViewManagedStaffDocuments;
                    
                    if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                            allStaffPermission))
                    {
                        return true;
                    }

                    if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                            ownStaffPermission))
                    {
                        if (dirOwner.Person.Id == user.PersonId)
                        {
                            return true;
                        }
                    }

                    if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                            managedStaffPermission))
                    {
                        if (user.PersonId.HasValue)
                        {
                            var staffMember = await _staffMemberService.GetByPersonId(dirOwner.Person.Id.Value);
                            var lineManager = await _staffMemberService.GetByPersonId(user.PersonId.Value);

                            if (staffMember != null && lineManager != null)
                            {
                                if (await _staffMemberService.IsLineManager(staffMember.Id.Value, lineManager.Id.Value))
                                {
                                    return true;
                                }
                            }
                        }
                    }

                    return false;
                }

                if (dirOwner.PersonTypes.IsStudent)
                {
                    var studentPermission =
                        edit
                            ? PermissionValue.StudentEditStudentDocuments
                            : PermissionValue.StudentViewStudentDocuments;
                    
                    if (await User.HasPermission(RoleService, PermissionRequirement.RequireAll,
                            studentPermission))
                    {
                        return true;
                    }
                }

                return user.UserType == UserTypes.Staff || user.PersonId == dirOwner.Person.Id;
            }

            return true;
        }
        
        private async Task<bool> CanAccessDocument(Guid documentId, bool edit)
        {
            var user = await GetLoggedInUser();
            
            var document = await _documentService.GetDocumentById(documentId);

            if (await CanAccessDirectory(document.DirectoryId, edit))
            {
                if (document.Private)
                {
                    return user.UserType == UserTypes.Staff;
                }

                return true;
            }

            return false;
        }

        [HttpGet]
        [Route("{documentId}")]
        [ProducesResponseType(typeof(DocumentModel), 200)]
        public async Task<IActionResult> GetDocumentById([FromRoute] Guid documentId)
        {
            try
            {
                if (await CanAccessDocument(documentId, false))
                {
                    var document = await _documentService.GetDocumentById(documentId);

                    return Ok(document);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDocument([FromBody] CreateDocumentRequestModel model)
        {
            try
            {
                if (await CanAccessDirectory(model.DirectoryId, true))
                {
                    var user = await GetLoggedInUser();
                
                    await _documentService.CreateDocument(user.Id.Value, model);

                    return Ok();   
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("file/upload")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile([FromBody] UploadAttachmentRequestModel requestModel)
        {
            try
            {
                if (_fileService is LocalFileService localFileService)
                {
                    if (await CanAccessDocument(requestModel.DocumentId, true))
                    {
                        await localFileService.UploadFileToDocument(requestModel);

                        return Ok();
                    }

                    return PermissionError();
                }

                return BadRequest(
                    "MyPortal is currently configured to use a 3rd party file provider. Please use LinkHostedFile endpoint instead.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("file/linkHosted")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LinkHostedFile([FromBody] LinkHostedFileRequestModel requestModel)
        {
            try
            {
                if (_fileService is HostedFileService hostedFileService)
                {
                    if (await CanAccessDocument(requestModel.DocumentId, true))
                    {
                        await hostedFileService.AttachFileToDocument(requestModel.DocumentId, requestModel.FileId);

                        return Ok();
                    }

                    return PermissionError();
                }

                return BadRequest(
                    "MyPortal is currently configured to host files locally. Please use UploadFile endpoint instead.");
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("file/{documentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttachment([FromRoute] Guid documentId)
        {
            try
            {
                if (await CanAccessDocument(documentId, true))
                {
                    await _fileService.RemoveFileFromDocument(documentId);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentRequestModel model)
        {
            try
            {
                if (await CanAccessDocument(model.Id, true))
                {
                    await _documentService.UpdateDocument(model);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("{documentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteDocument([FromRoute] Guid documentId)
        {
            try
            {
                if (await CanAccessDocument(documentId, true))
                {
                    await _documentService.DeleteDocument(documentId);

                    return Ok();   
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("file/{documentId}")]
        [ProducesResponseType(typeof(Stream), 200)]
        public async Task<IActionResult> DownloadFile([FromRoute] Guid documentId)
        {
            try
            {
                if (await CanAccessDocument(documentId, false))
                {
                    var download = await _fileService.GetDownloadByDocument(documentId);

                    return File(download.FileStream, download.ContentType, download.FileName);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("webActions/{documentId}")]
        [ProducesResponseType(typeof(IEnumerable<WebAction>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWebActions([FromRoute] Guid documentId)
        {
            try
            {
                if (_fileService is HostedFileService hostedFileService)
                {
                    if (await CanAccessDocument(documentId, false))
                    {
                        var webActions = await hostedFileService.GetWebActionsByDocument(documentId);

                        return Ok(webActions);
                    }

                    return PermissionError();
                }

                return Ok(new List<WebAction>());
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
        
        [HttpGet]
        [Route("directories/children/{directoryId}")]
        [ProducesResponseType(typeof(DirectoryChildListWrapperResponseModel), 200)]
        public async Task<IActionResult> GetDirectoryChildren([FromRoute] Guid directoryId)
        {
            try
            {
                if (await CanAccessDirectory(directoryId, false))
                {
                    var user = await GetLoggedInUser();
                
                    var directory = await _documentService.GetDirectoryById(directoryId);

                    var children =
                        await _documentService.GetDirectoryChildren(directoryId, user.UserType == UserTypes.Staff);

                    var childList = new List<DirectoryChildSummaryModel>();

                    childList.AddRange(children.Subdirectories.Select(x => x.GetListModel()));
                    childList.AddRange(children.Files.Select(x => x.GetListModel()));

                    var response = new DirectoryChildListWrapperResponseModel
                    {
                        Directory = directory,
                        Children = childList
                    };

                    return Ok(response);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("directories/create")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDirectory([FromBody] CreateDirectoryRequestModel requestModel)
        {
            try
            {
                if (await CanAccessDirectory(requestModel.ParentId, true))
                {
                    await _documentService.CreateDirectory(requestModel);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPost]
        [Route("directories/update")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromBody] UpdateDirectoryRequestModel requestModel)
        {
            try
            {
                if (await CanAccessDirectory(requestModel.Id, true))
                {
                    await _documentService.UpdateDirectory(requestModel);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpDelete]
        [Route("directories/{directoryId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromRoute] Guid directoryId)
        {
            try
            {
                if (await CanAccessDirectory(directoryId, true))
                {
                    await _documentService.DeleteDirectory(directoryId);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpGet]
        [Route("directories/{directoryId}")]
        [ProducesResponseType(typeof(DirectoryModel), 200)]
        public async Task<IActionResult> GetById([FromRoute] Guid directoryId)
        {
            try
            {
                if (await CanAccessDirectory(directoryId, false))
                {
                    var directory = await _documentService.GetDirectoryById(directoryId);

                    return Ok(directory);
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }
    }
}
