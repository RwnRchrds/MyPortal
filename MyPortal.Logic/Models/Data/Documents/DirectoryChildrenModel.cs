using System.Collections.Generic;

namespace MyPortal.Logic.Models.Data.Documents
{
    public class DirectoryChildrenModel
    {
        public virtual IEnumerable<DirectoryModel> Subdirectories { get; set; }
        public virtual IEnumerable<DocumentModel> Files { get; set; }
    }
}