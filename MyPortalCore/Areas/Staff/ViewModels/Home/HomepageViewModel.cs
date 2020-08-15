using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.List;
using MyPortal.Logic.Models.Person.Tasks;

namespace MyPortalCore.Areas.Staff.ViewModels.Home
{
    public class HomepageViewModel
    {
        public AcademicYearModel SelectedAcademicYear { get; set; }

        public IEnumerable<TaskListModel> Tasks { get; set; }

        public CreateTaskModel Task { get; set; }
        public IEnumerable<SelectListItem> TaskTypes { get; set; }

        public PersonModel Person { get; set; }
    }
}