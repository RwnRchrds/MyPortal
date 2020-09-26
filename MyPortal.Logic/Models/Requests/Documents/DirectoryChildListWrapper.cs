using System.Collections.Generic;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class DirectoryChildListWrapper
    {
        public DirectoryModel Directory { get; set; }
        public IEnumerable<DirectoryChildListModel> Children { get; set; }
    }
}
