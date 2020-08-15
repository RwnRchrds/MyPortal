using System.Collections.Generic;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.List;

namespace MyPortal.Logic.Models.Documents
{
    public class DirectoryChildListWrapper
    {
        public DirectoryModel Directory { get; set; }
        public IEnumerable<DirectoryChildListModel> Children { get; set; }
    }
}
