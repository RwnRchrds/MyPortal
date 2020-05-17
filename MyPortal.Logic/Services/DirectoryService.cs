using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Exceptions;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Services
{
    public class DirectoryService : BaseService, IDirectoryService
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IDocumentRepository _documentRepository;

        public DirectoryService(IDirectoryRepository directoryRepository, IDocumentRepository documentRepository) : base("Directory")
        {
            _directoryRepository = directoryRepository;
            _documentRepository = documentRepository;
        }

        public async Task<DirectoryModel> GetById(Guid directoryId)
        {
            var directory = await _directoryRepository.GetById(directoryId);

            if (directory == null)
            {
                NotFound();
            }

            return _businessMapper.Map<DirectoryModel>(directory);
        }

        public async Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly)
        {
            var directory = await _directoryRepository.GetById(directoryId);

            if (directory == null)
            {
                NotFound();
            }

            var children = new DirectoryChildren();

            var subDirs = await _directoryRepository.GetSubdirectories(directoryId, includeStaffOnly);

            var files = await _documentRepository.GetByDirectory(directoryId);

            children.Subdirectories = subDirs.Select(_businessMapper.Map<DirectoryModel>);
            children.Files = files.Select(_businessMapper.Map<DocumentModel>);

            return children;
        }

        public async Task<bool> IsAuthorised(UserModel user, Guid directoryId)
        {
            var directory = await _directoryRepository.GetById(directoryId);

            if (directory.Private && (user.UserType == UserTypes.Staff || user.Person?.DirectoryId == directory.Id))
            {
                return true;
            }

            if (directory.StaffOnly && user.UserType == UserTypes.Staff)
            {
                return true;
            }

            return false;
        }

        public override void Dispose()
        {
            _directoryRepository.Dispose();
            _documentRepository.Dispose();
        }
    }
}
