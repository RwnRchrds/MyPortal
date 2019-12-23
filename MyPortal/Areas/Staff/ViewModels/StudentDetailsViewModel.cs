using System.Collections.Generic;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class StudentDetailsViewModel
    {
        public StudentDto Student { get; set; }
        public PhoneNumberDto PhoneNumber { get; set; }
        public EmailAddressDto EmailAddress { get; set; }
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