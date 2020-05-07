using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Services
{
    public class DirectoryService : BaseService, IDirectoryService
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly IDocumentRepository _documentRepository;

        public DirectoryService(IDirectoryRepository directoryRepository, IDocumentRepository documentRepository)
        {
            _directoryRepository = directoryRepository;
            _documentRepository = documentRepository;
        }

        public async Task<DirectoryChildren> GetChildren(Guid directoryId)
        {
            var children = new DirectoryChildren();

            var subDirs = await _directoryRepository.GetSubdirectories(directoryId);

            var files = await _documentRepository.GetByDirectory(directoryId);

            children.Subdirectories = subDirs.Select(_businessMapper.Map<DirectoryModel>);
            children.Files = files.Select(_businessMapper.Map<DocumentModel>);

            return children;
        }
    }
}
