using System.Collections.Generic;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public IDictionary<int, string> YearGroups { get; set; }
        public IDictionary<int, string> RegGroups { get; set; }
        public IDictionary<int, string> Houses { get; set; }
        public IDictionary<int, string> ResultSets { get; set; }
        public IDictionary<int, string> Subjects { get; set; }
        public IDictionary<string, string> Genders { get; set; }
        public IDictionary<int, string> PhoneNumberTypes { get; set; }
        public IDictionary<int, string> EmailAddressTypes { get; set; }
    }
}