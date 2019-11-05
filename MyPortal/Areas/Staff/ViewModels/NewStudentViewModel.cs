using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class NewStudentViewModel 
    {
        public Student Student { get; set; }
        public IDictionary<string, string> Genders { get; set; }
    }
}