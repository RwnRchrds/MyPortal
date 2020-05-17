using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class DirectoryChildListWrapper
    {
        public DirectoryModel Directory { get; set; }
        public IEnumerable<DirectoryChildListModel> Children { get; set; }
    }
}
