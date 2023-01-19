using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyPortal.Logic.Models.DocumentProvision;
using MyPortal.Logic.Models.Web;
using File = MyPortal.Database.Models.Entity.File;

namespace MyPortal.Logic.Interfaces
{
    public interface IHostedFileProvider
    {
        Task<IEnumerable<WebAction>> GetWebActions(string accessToken, string fileId); 
        Task<File> CreateFileFromId(string accessToken, string fileId);
    }
}
