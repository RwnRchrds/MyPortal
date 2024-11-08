using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Documents;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseServiceWithAccessControl, IDocumentService, IDocumentAccessController
    {
        private readonly IStaffMemberService _staffMemberService;
        
        public DocumentService(ISessionUser user, IUserService userService, IPersonService personService,
            IStudentService studentService, IStaffMemberService staffMemberService)
            : base(user, userService, personService, studentService)
        {
            _staffMemberService = staffMemberService;
        }

        private async Task<bool> CanAccessDirectory(Guid directoryId, bool edit)
        {
            var user = await UserService.GetCurrentUser();

            if (await IsSchoolDirectory(directoryId))
            {
                var publicPermission =
                    edit ? PermissionValue.SchoolEditSchoolDocuments : PermissionValue.SchoolViewSchoolDocuments;

                if (await User.HasPermission(UserService, PermissionRequirement.RequireAll, publicPermission))
                {
                    return true;
                }

                return false;
            }

            if (await IsPrivateDirectory(directoryId))
            {
                var dirOwner = await PersonService.GetPersonWithTypesByDirectory(directoryId);

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
                        if (user.PersonId.HasValue && dirOwner.Person.Id.HasValue)
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
            var user = await UserService.GetCurrentUser();

            await using var unitOfWork = await User.GetConnection();
            
            var document = await unitOfWork.Documents.GetById(documentId);

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

        public async Task VerifyDirectoryAccess(Guid directoryId, bool edit)
        {
            if (!await CanAccessDirectory(directoryId, edit))
            {
                var action = edit ? "edit" : "view";
                
                throw new PermissionException($"You do not have permission to {action} this directory.");
            }
        }
        
        public async Task VerifyDocumentAccess(Guid documentId, bool edit)
        {
            if (!await CanAccessDocument(documentId, edit))
            {
                var action = edit ? "edit" : "view";
                
                throw new PermissionException($"You do not have permission to {action} this document.");
            }
        }

        public async Task CreateDocument(DocumentRequestModel document)
        {
            Validate(document);
            
            await VerifyDirectoryAccess(document.DirectoryId, true);

            var userId = User.GetUserId();

            if (userId != null)
            {
                await using var unitOfWork = await User.GetConnection();

                var directory = await unitOfWork.Directories.GetById(document.DirectoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                try
                {
                    var docToAdd = new Document
                    {
                        Id = Guid.NewGuid(),
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        CreatedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        CreatedById = userId.Value,
                        Deleted = false,
                        Private = document.Private
                    };

                    unitOfWork.Documents.Create(docToAdd);
                }
                catch (Exception e)
                {
                    throw e.GetBaseException();
                }

                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw Unauthenticated();
            }
        }

        public async Task UpdateDocument(Guid documentId, DocumentRequestModel document)
        {
            Validate(document);

            await VerifyDocumentAccess(documentId, true);

            await using var unitOfWork = await User.GetConnection();

            var documentInDb = await unitOfWork.Documents.GetById(documentId);

            documentInDb.Title = document.Title;
            documentInDb.Description = document.Description;
            documentInDb.Private = document.Private;
            documentInDb.TypeId = document.TypeId;

            await unitOfWork.Documents.Update(documentInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            await using var unitOfWork = await User.GetConnection();

            var documentTypes = await unitOfWork.DocumentTypes.Get(filter);

            return documentTypes.Select(t => new DocumentTypeModel(t)).ToList();
        }

        public async Task DeleteDocument(Guid documentId)
        {
            await VerifyDocumentAccess(documentId, true);
            
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.Documents.Delete(documentId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            await using var unitOfWork = await User.GetConnection();

            var document = await unitOfWork.Documents.GetById(documentId);

            await VerifyDocumentAccess(document.DirectoryId, false);

            if (document == null)
            {
                throw new NotFoundException("Document not found.");
            }

            return new DocumentModel(document);
        }

        public async Task<DirectoryModel> GetDirectoryById(Guid directoryId)
        {
            await VerifyDirectoryAccess(directoryId, false);
            
            await using var unitOfWork = await User.GetConnection();

            var directory = await unitOfWork.Directories.GetById(directoryId);

            if (directory == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            return new DirectoryModel(directory);
        }

        public async Task CreateDirectory(DirectoryRequestModel directory)
        {
            Validate(directory);

            if (directory.ParentId == null)
            {
                throw new LogicException("A parent directory was not provided.");
            }

            await using var unitOfWork = await User.GetConnection();

            var parentDirectory = await unitOfWork.Directories.GetById(directory.ParentId.Value);

            if (parentDirectory == null)
            {
                throw new NotFoundException("Parent directory not found.");
            }
            
            await VerifyDirectoryAccess(parentDirectory.Id, true);

            var dirToAdd = new Directory
            {
                Id = Guid.NewGuid(),
                ParentId = directory.ParentId,
                Name = directory.Name,
                Private = directory.Private
            };

            unitOfWork.Directories.Create(dirToAdd);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateDirectory(Guid directoryId, DirectoryRequestModel directory)
        {
            Validate(directory);

            await VerifyDirectoryAccess(directoryId, true);

            await using var unitOfWork = await User.GetConnection();

            var dirInDb = await unitOfWork.Directories.GetById(directoryId);

            if (!string.IsNullOrWhiteSpace(directory.Name))
            {
                dirInDb.Name = directory.Name;
            }

            dirInDb.Private = directory.Private;

            // Cannot move root directories or remove parent from child directories
            if (dirInDb.ParentId != null && directory.ParentId != null)
            {
                dirInDb.ParentId = directory.ParentId;
            }

            await unitOfWork.Directories.Update(dirInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDirectory(Guid directoryId)
        {
            await VerifyDirectoryAccess(directoryId, true);
            
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.Directories.Delete(directoryId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<DirectoryChildrenModel> GetDirectoryChildren(Guid directoryId)
        {
            await VerifyDirectoryAccess(directoryId, false);
            
            await using var unitOfWork = await User.GetConnection();

            var directory = await unitOfWork.Directories.GetById(directoryId);

            if (directory == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            var children = new DirectoryChildrenModel();

            var includeRestricted = User.IsType(UserTypes.Staff);

            var subDirs =
                await unitOfWork.Directories.GetSubdirectories(directoryId, includeRestricted);

            var files = await unitOfWork.Documents.GetByDirectory(directoryId);

            children.Subdirectories = subDirs.Select(dir => new DirectoryModel(dir));
            children.Files = files.Select(doc => new DocumentModel(doc));

            return children;
        }

        public async Task<bool> IsPrivateDirectory(Guid directoryId)
        {
            await using var unitOfWork = await User.GetConnection();

            var dir = await unitOfWork.Directories.GetById(directoryId);

            if (dir == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            if (dir.Private)
            {
                return true;
            }

            if (dir.ParentId == null)
            {
                return false;
            }

            return await IsPrivateDirectory(dir.ParentId.Value);
        }

        public async Task<bool> IsSchoolDirectory(Guid directoryId)
        {
            await using var unitOfWork = await User.GetConnection();

            var dir = await unitOfWork.Directories.GetById(directoryId);

            if (dir == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            if (dir.ParentId == Directories.School)
            {
                return true;
            }

            if (dir.ParentId == null)
            {
                return false;
            }

            return await IsSchoolDirectory(dir.ParentId.Value);
        }
    }
}