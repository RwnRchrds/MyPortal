using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Models.Data;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentSearchViewModel
    {
        public Lookup SearchTypes { get; set; }
        public Dictionary<string, string> GenderOptions { get; set; }
        public Lookup RegGroups { get; set; }
        public Lookup YearGroups { get; set; }
        public Lookup Houses { get; set; }  
    }
}