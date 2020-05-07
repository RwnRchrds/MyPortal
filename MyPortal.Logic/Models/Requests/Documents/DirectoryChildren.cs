using System.Collections.Generic;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class DirectoryChildren
    {
        public virtual IEnumerable<DirectoryModel> Subdirectories { get; set; }
        public virtual IEnumerable<DocumentModel> Files { get; set; }
    }
}
