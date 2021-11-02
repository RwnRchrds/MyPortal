using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Documents;
using MyPortal.Logic.Models.Response.Documents;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IDirectoryService
    {
        Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly);
        Task<DirectoryModel> GetById(Guid directoryId);
        Task Create(params CreateDirectoryModel[] directories);
        Task Update(params UpdateDirectoryModel[] directories);
        Task Delete(params Guid[] directoryIds);
        Task<bool> IsAuthorised(UserModel user, Guid directoryId);
    }
}
