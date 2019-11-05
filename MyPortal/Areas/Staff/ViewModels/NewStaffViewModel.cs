using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class NewStaffViewModel
    {
        public NewStaffViewModel()
        {
            Titles = new List<string> {"Mr", "Miss", "Mrs", "Ms", "Mx", "Prof", "Sir", "Dr", "Lady", "Lord"};
        }

        public StaffMember Staff { get; set; }
        public IEnumerable<string> Titles { get; set; }
    }
}