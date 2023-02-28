using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.FileProviders;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Documents;

using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Summary;
using MyPortal.Logic.Models.Web;
using MyPortal.Logic.Services;
using MyPortalWeb.Controllers.BaseControllers;
using Configuration = MyPortal.Logic.Configuration;

namespace MyPortalWeb.Controllers.Api
{
    [Authorize]
    [Route("api/documents")]
    public class DocumentsController : BaseApiController
    {
        private readonly IDocumentService _documentService;
        private readonly IPersonService _personService;
        private readonly IStaffMemberService _staffMemberService;

        public DocumentsController(IUserService userService, IDocumentService documentService,
            IPersonService personService, IStaffMemberService staffMemberService) : base(userService)
        {
            _documentService = documentService;
            _personService = personService;
            _staffMemberService = staffMemberService;
        }   

        private IFileService CreateFileService()
        {
            if (Configuration.Instance.FileProvider == FileProvider.Local)
            {
                return new LocalFileService(new LocalFileProvider());
            }
            
            if (Configuration.Instance.FileProvider == FileProvider.Google)
            {
                var accessToken = Request.Headers["file-access-token"];
                
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    throw new UnauthorisedException("No file access token was provided.");
                }
                
                var fileProvider = new GoogleFileProvider();
                return new HostedFileService(fileProvider, accessToken);
            }

            return null;
        }

        private async Task<bool> CanAccessDirectory(Guid directoryId, bool edit)
        {
            var user = await GetLoggedInUser();
            
            if (await _documentService.IsSchoolDirectory(directoryId))
            {
                var publicPermission =
                    edit ? PermissionValue.SchoolEditSchoolDocuments : PermissionValue.SchoolViewSchoolDocuments;

                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll, publicPermission))
                {
                    return true;
                }

                return false;
            }

            if (await _documentService.IsPrivateDirectory(directoryId))
            {
                var dirOwner = await _personService.GetPersonWithTypesByDirectory(directoryId);

                if (dirOwner == null)
                {
                    return User.IsType(UserTypes.Staff);
                }

                if (dirOwner.PersonTypes.StaffId.HasValue)
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
                    
                    if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                            allStaffPermission))
                    {
                        return true;
                    }

                    if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                            ownStaffPermission))
                    {
                        if (dirOwner.Person.Id == user.PersonId)
                        {
                            return true;
                        }
                    }

                    if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
                            managedStaffPermission))
                    {
                        if (user.PersonId.HasValue)
                        {
                            var staffMember = await _staffMemberService.GetByPersonId(dirOwner.Person.Id.Value);
                            var lineManager = await _staffMemberService.GetByPersonId(user.PersonId.Value);

                            if (staffMember is { Id: { } } && lineManager is { Id: { } })
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

                if (dirOwner.PersonTypes.StudentId.HasValue)
                {
                    var studentPermission =
                        edit
                            ? PermissionValue.StudentEditStudentDocuments
                            : PermissionValue.StudentViewStudentDocuments;
                    
                    if (await User.HasPermission(UserService, PermissionRequirement.RequireAll,
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
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentRequestModel model)
        {
            try
            {
                if (await CanAccessDirectory(model.DirectoryId, true))
                {
                    await _documentService.CreateDocument(model);

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
        [Route("{documentId}/file")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile([FromRoute] Guid documentId, [FromBody] FileUploadRequestModel requestModel)
        {
            try
            {
                var fileService = CreateFileService();
                
                if (fileService is LocalFileService localFileService)
                {
                    if (await CanAccessDocument(documentId, true))
                    {
                        requestModel.DocumentId = documentId;
                        
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
        [Route("{documentId}/file")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LinkHostedFile([FromBody] HostedFileRequestModel requestModel)
        {
            try
            {
                var fileService = CreateFileService();
                
                if (fileService is HostedFileService hostedFileService)
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
        [Route("{documentId}/file")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RemoveAttachment([FromRoute] Guid documentId)
        {
            try
            {
                var fileService = CreateFileService();
                
                if (await CanAccessDocument(documentId, true))
                {
                    await fileService.RemoveFileFromDocument(documentId);

                    return Ok();
                }

                return PermissionError();
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        [HttpPut]
        [Route("{documentId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateDocument([FromRoute] Guid documentId, [FromBody] DocumentRequestModel model)
        {
            try
            {
                if (await CanAccessDocument(documentId, true))
                {
                    var userId = User.GetUserId();

                    await _documentService.UpdateDocument(userId, model);

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
        [Route("{documentId}/webActions")]
        [ProducesResponseType(typeof(IEnumerable<WebAction>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetWebActions([FromQuery] Guid documentId)
        {
            try
            {
                var fileService = CreateFileService();
                
                if (fileService is HostedFileService hostedFileService)
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
        [Route("directories/{directoryId}/children")]
        [ProducesResponseType(typeof(DirectoryChildWrapper), 200)]
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

                    var response = new DirectoryChildWrapper
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
        [Route("directories")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDirectory([FromBody] DirectoryRequestModel requestModel)
        {
            try
            {
                if (!requestModel.ParentId.HasValue)
                {
                    return Error(HttpStatusCode.BadRequest, "A parent directory was not provided.");
                }
                
                if (await CanAccessDirectory(requestModel.ParentId.Value, true))
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

        [HttpPut]
        [Route("directories/{directoryId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Update([FromRoute] Guid directoryId, [FromBody] DirectoryRequestModel requestModel)
        {
            try
            {
                if (await CanAccessDirectory(directoryId, true))
                {
                    await _documentService.UpdateDirectory(directoryId, requestModel);

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
