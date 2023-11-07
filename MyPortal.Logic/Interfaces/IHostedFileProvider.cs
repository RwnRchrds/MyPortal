using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Web;

namespace MyPortal.Logic.Interfaces
{
    public interface IHostedFileProvider
    {
        Task<IEnumerable<WebAction>> GetWebActions(string fileId);
        Task<File> CreateFileFromId(string fileId);
    }
}