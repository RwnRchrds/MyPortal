using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class NewStudentViewModel
    {
        public NewStudentViewModel()
        {
            Genders = Gender.GetGenderOptions();
        }
        public IEnumerable<YearGroup> YearGroups { get; set; }
        public IEnumerable<RegGroup> RegGroups { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public Student Student { get; set; }
    }
}