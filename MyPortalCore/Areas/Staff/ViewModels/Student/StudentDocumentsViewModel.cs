using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Requests.Documents;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentDocumentsViewModel
    {
        public StudentModel Student { get; set; }
        public UpdateDocumentModel Document { get; set; }
        public UpdateDirectoryModel Directory { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
    }
}
