using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Exceptions;
using MyPortal.Logic.Models.Requests.Documents;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class DirectoryService : BaseService, IDirectoryService
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IDocumentRepository _documentRepository;

        public DirectoryService(ApplicationDbContext context)
        {
            _directoryRepository = new DirectoryRepository(context);
            _documentRepository = new DocumentRepository(context);
        }

        public async Task<DirectoryModel> GetById(Guid directoryId)
        {
            var directory = await _directoryRepository.GetById(directoryId);

            if (directory == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            return BusinessMapper.Map<DirectoryModel>(directory);
        }

        public async Task Create(params DirectoryModel[] directories)
        {
            foreach (var directory in directories)
            {
                if (directory.ParentId == null)
                {
                    throw new InvalidDataException("Parent directory not specified.");
                }

                var parentDirectory = await _directoryRepository.GetById(directory.ParentId.Value);

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
                
                _directoryRepository.Create(dirToAdd);
            }

            await _directoryRepository.SaveChanges();
        }

        public async Task Update(params DirectoryModel[] directories)
        {
            foreach (var directory in directories)
            {
                var dirInDb = await _directoryRepository.GetByIdWithTracking(directory.Id);

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

            await _directoryRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] directoryIds)
        {
            foreach (var directoryId in directoryIds)
            {
                await _directoryRepository.Delete(directoryId);
            }

            await _directoryRepository.SaveChanges();
        }

        public async Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly)
        {
            var directory = await _directoryRepository.GetById(directoryId);

            if (directory == null)
            {
                throw new NotFoundException("Directory not found.");
            }

            var children = new DirectoryChildren();

            var subDirs = await _directoryRepository.GetSubdirectories(directoryId, includeStaffOnly);

            var files = await _documentRepository.GetByDirectory(directoryId);

            children.Subdirectories = subDirs.Select(BusinessMapper.Map<DirectoryModel>);
            children.Files = files.Select(BusinessMapper.Map<DocumentModel>);

            return children;
        }

        public async Task<bool> IsAuthorised(UserModel user, Guid directoryId)
        {
            var directory = await _directoryRepository.GetById(directoryId);

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

        public override void Dispose()
        {
            _directoryRepository.Dispose();
            _documentRepository.Dispose();
        }
    }
}
