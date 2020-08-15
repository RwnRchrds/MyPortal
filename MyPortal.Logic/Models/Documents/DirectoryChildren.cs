using System.Collections.Generic;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Documents
{
    public class DirectoryChildren
    {
        public virtual IEnumerable<DirectoryModel> Subdirectories { get; set; }
        public virtual IEnumerable<DocumentModel> Files { get; set; }
    }
}
