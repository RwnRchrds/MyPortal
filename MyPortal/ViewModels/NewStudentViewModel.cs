using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class NewStudentViewModel 
    {
        public Student Student { get; set; }
        public IDictionary<string, string> Genders { get; set; }
    }
}