using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IFileService : IService
    {
        Task RemoveFileFromDocument(Guid documentId);
    }
}