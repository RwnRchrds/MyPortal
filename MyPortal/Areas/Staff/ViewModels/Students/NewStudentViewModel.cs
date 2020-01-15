using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels.Students
{
    public class NewStudentViewModel
    {
        public StudentDto Student { get; set; }
        public IDictionary<string, string> Genders { get; set; }
    }
}