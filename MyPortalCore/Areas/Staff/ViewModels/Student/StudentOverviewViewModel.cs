using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.ListModels;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentOverviewViewModel
    {
        public StudentModel Student { get; set; }
        public IEnumerable<LogNoteListModel> LogNotes { get; set; }
        public IEnumerable<SelectListItem> LogNoteTypes { get; set; }
        public double? Attendance { get; set; }
        public int AchievementPoints { get; set; }
        public int BehaviourPoints { get; set; }
        public LogNoteModel LogNote { get; set; }  
    }   
}
