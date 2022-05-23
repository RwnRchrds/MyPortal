using System.Collections.Generic;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Response.Documents
{
    public class DirectoryChildListWrapperResponseModel
    {
        public DirectoryModel Directory { get; set; }
        public IEnumerable<DirectoryChildSummaryModel> Children { get; set; }
    }
}
