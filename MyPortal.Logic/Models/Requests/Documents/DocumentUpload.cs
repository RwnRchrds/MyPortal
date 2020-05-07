using System.IO;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class DocumentUpload
    {
        public DocumentModel Details { get; set; }
        public FileStream FileStream { get; set; }
    }
}
