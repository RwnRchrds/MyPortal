using System.Collections.Generic;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.ViewModels
{
    public class SubjectsViewModel
    {
        public CurriculumSubject Subject { get; set; }
        public IEnumerable<CoreStaffMember> Staff { get; set; }
    }
}