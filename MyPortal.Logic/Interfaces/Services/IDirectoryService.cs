using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDirectoryService : IService
    {
        Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly);
        Task<DirectoryModel> GetById(Guid directoryId);
        Task Create(params DirectoryModel[] directories);
        Task Update(params DirectoryModel[] directories);
        Task Delete(params Guid[] directoryIds);
        Task<bool> IsAuthorised(UserModel user, Guid directoryId);
    }
}
