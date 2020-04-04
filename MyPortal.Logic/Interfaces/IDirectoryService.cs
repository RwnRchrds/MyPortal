using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Documents;

namespace MyPortal.Logic.Interfaces
{
    public interface IDirectoryService
    {
        Task<DirectoryChildren> GetChildren(Guid directoryId);
    }
}
