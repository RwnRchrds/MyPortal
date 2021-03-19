using System;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Response.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DirectoryService : BaseService, IDirectoryService
    {
        public async Task<DirectoryModel> GetById(Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(directoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                return BusinessMapper.Map<DirectoryModel>(directory);
            }
        }

        public async Task Create(params DirectoryModel[] directories)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var directory in directories)
                {
                    if (directory.ParentId == null)
                    {
                        throw new InvalidDataException("Parent directory not specified.");
                    }

                    var parentDirectory = await unitOfWork.Directories.GetById(directory.ParentId.Value);

                    if (parentDirectory == null)
                    {
                        throw new NotFoundException("Parent directory not found.");
                    }

                    var dirToAdd = new Directory
                    {
                        ParentId = directory.ParentId,
                        Name = directory.Name,
                        Private = parentDirectory.Private || directory.Private,
                        StaffOnly = directory.StaffOnly
                    };

                    unitOfWork.Directories.Create(dirToAdd);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Update(params DirectoryModel[] directories)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var directory in directories)
                {
                    var dirInDb = await unitOfWork.Directories.GetByIdForEditing(directory.Id);

                    if (!string.IsNullOrWhiteSpace(directory.Name))
                    {
                        dirInDb.Name = directory.Name;
                    }

                    dirInDb.Private = directory.Private;
                    dirInDb.StaffOnly = directory.StaffOnly;

                    // Cannot move root directories or remove parent from child directories
                    if (dirInDb.ParentId != null && directory.ParentId != null)
                    {
                        dirInDb.ParentId = directory.ParentId;
                    }
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Delete(params Guid[] directoryIds)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var directoryId in directoryIds)
                {
                    await unitOfWork.Directories.Delete(directoryId);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(directoryId);

                if (directory == null)
                {
                    throw new NotFoundException("Directory not found.");
                }

                var children = new DirectoryChildren();

                var subDirs = await unitOfWork.Directories.GetSubdirectories(directoryId, includeStaffOnly);

                var files = await unitOfWork.Documents.GetByDirectory(directoryId);

                children.Subdirectories = subDirs.Select(BusinessMapper.Map<DirectoryModel>);
                children.Files = files.Select(BusinessMapper.Map<DocumentModel>);

                return children;
            }
        }

        public async Task<bool> IsAuthorised(UserModel user, Guid directoryId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var directory = await unitOfWork.Directories.GetById(directoryId);

                if (directory.StaffOnly)
                {
                    return user.UserType == UserTypes.Staff;
                }

                if (directory.Private)
                {
                    if (user.UserType == UserTypes.Staff || user.Person?.DirectoryId == directory.Id ||
                        directory.ParentId != null && await IsAuthorised(user, directory.ParentId.Value))
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }
        }
    }
}
