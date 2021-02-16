using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface IFileService
    {
        Task<FileDownload> GetDownloadByDocument(Guid documentId);
        Task RemoveFileFromDocument(Guid documentId);
    }
}
