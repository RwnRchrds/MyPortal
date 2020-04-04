using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Business;

namespace MyPortal.Logic.Models.Documents
{
    public class DirectoryChildren
    {
        public virtual IEnumerable<DirectoryModel> Subdirectories { get; set; }
        public virtual IEnumerable<DocumentModel> Files { get; set; }
    }
}
