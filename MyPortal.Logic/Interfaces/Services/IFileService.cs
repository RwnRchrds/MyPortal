using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IFileService : IService
    {
        Task RemoveFileFromDocument(Guid documentId);
    }
}
