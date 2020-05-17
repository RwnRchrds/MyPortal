using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortal.Logic.Interfaces
{
    public interface IDirectoryService : IService
    {
        Task<DirectoryChildren> GetChildren(Guid directoryId, bool includeStaffOnly);
        Task<DirectoryModel> GetById(Guid directoryId);
    }
}
