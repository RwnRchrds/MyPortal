using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StudentExtendedDetailsViewModel
    {
        public Student Student { get; set; }
        public IDictionary<int, string> YearGroups { get; set; }
        public IDictionary<int, string> RegGroups { get; set; }
        public IDictionary<int, string> Houses { get; set; }
        public IDictionary<int, string> ResultSets { get; set; }
        public IDictionary<int, string> Subjects { get; set; }
        public IDictionary<string, string> Genders { get; set; }
    }
}