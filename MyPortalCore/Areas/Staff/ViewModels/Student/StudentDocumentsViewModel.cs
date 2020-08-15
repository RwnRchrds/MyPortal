using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Models.Documents;
using MyPortal.Logic.Models.Entity;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentDocumentsViewModel
    {
        public StudentModel Student { get; set; }
        public CreateDocumentModel Document { get; set; }
        public CreateDirectoryModel Directory { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
    }
}
