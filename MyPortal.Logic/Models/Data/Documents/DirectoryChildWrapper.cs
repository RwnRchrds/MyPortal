using System.Collections.Generic;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Models.Data.Documents
{
    public class DirectoryChildWrapper
    {
        public DirectoryModel Directory { get; set; }
        public IEnumerable<DirectoryChildSummaryModel> Children { get; set; }
    }
}