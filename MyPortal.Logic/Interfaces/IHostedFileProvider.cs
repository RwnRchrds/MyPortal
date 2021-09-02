using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Web;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.Interfaces
{
    public interface IHostedFileProvider : IDisposable
    {
        Task<IEnumerable<WebAction>> GetWebActions(string fileId); 
        Task<File> GetFileById(string fileId);
        Task<Stream> DownloadFileToStream(string fileId);
    }
}
