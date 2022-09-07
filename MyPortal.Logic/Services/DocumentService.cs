using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Enums;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Filters;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly IUserService _userService;
        private readonly IStaffMemberService _staffMemberService;

        public async Task CreateDocument(DocumentRequestModel document)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(document.DirectoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                try
                {
                    var docToAdd = new Document
                    {
                        TypeId = document.TypeId,
                        Title = document.Title,
                        Description = document.Description,
                        CreatedDate = DateTime.Today,
                        DirectoryId = document.DirectoryId,
                        CreatedById = document.CreatedById,
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
        }

        public async Task UpdateDocument(Guid documentId, DocumentRequestModel document)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var documentInDb = await unitOfWork.Documents.GetById(documentId);

                documentInDb.Title = document.Title;
                documentInDb.Description = document.Description;
                documentInDb.Private = document.Private;
                documentInDb.TypeId = document.TypeId;

                await unitOfWork.Documents.Update(documentInDb);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DocumentTypeModel>> GetTypes(DocumentTypeFilter filter)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var documentTypes = await unitOfWork.DocumentTypes.Get(filter);

                return documentTypes.Select(t => new DocumentTypeModel(t)).ToList();
            }
        }

        public async Task DeleteDocument(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await unitOfWork.Documents.Delete(documentId);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<DocumentModel> GetDocumentById(Guid documentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var document = await unitOfWork.Documents.GetById(documentId);

                if (document == null)
                {
                    throw new NotFoundException("Document not found.");
                }

                return new DocumentModel(document);
            }
        }
        
        public async Task<DirectoryModel> GetDirectoryById(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(directoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                return new DirectoryModel(directory);
            }
        }

        public async Task CreateDirectory(DirectoryRequestModel directory)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var parentDirectory = await unitOfWork.Directories.GetById(directory.ParentId);

                if (parentDirectory == null)
                {
                    throw new NotFoundException("Parent directory not found.");
                }

                var dirToAdd = new Directory
                {
                    ParentId = directory.ParentId,
                    Name = directory.Name,
                    Private = directory.Private
                };

                unitOfWork.Directories.Create(dirToAdd);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task UpdateDirectory(Guid directoryId, DirectoryRequestModel directory)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
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
        }

        public async Task DeleteDirectory(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                await unitOfWork.Directories.Delete(directoryId);

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<DirectoryChildrenResponseModel> GetDirectoryChildren(Guid directoryId, bool includeRestricted)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(directoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                var children = new DirectoryChildrenResponseModel();

                var subDirs =
                    await unitOfWork.Directories.GetSubdirectories(directoryId, includeRestricted);

                var files = await unitOfWork.Documents.GetByDirectory(directoryId);

                children.Subdirectories = subDirs.Select(dir => new DirectoryModel(dir));
                children.Files = files.Select(doc => new DocumentModel(doc));

                return children;
            }
        }

        public async Task<bool> IsPrivateDirectory(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
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
        }

        public async Task<bool> IsSchoolDirectory(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
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
}
