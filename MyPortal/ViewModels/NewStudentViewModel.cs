using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class NewStudentViewModel
    {
        public NewStudentViewModel()
        {
            Genders = Gender.GetGenderOptions();
        }
        public IEnumerable<PastoralYearGroup> YearGroups { get; set; }
        public IEnumerable<PastoralRegGroup> RegGroups { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public PeopleStudent Student { get; set; }
    }
}