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
using MyPortal.Logic.Models.Requests.Person.Tasks;
using MyPortal.Logic.Models.Requests.Student.LogNotes;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentOverviewViewModel
    {
        public StudentModel Student { get; set; }
        public IEnumerable<LogNoteListModel> LogNotes { get; set; }
        public IEnumerable<TaskListModel> Tasks { get; set; }
        public IEnumerable<SelectListItem> LogNoteTypes { get; set; }
        public IEnumerable<SelectListItem> TaskTypes { get; set; }
        public double? Attendance { get; set; }
        public int AchievementPoints { get; set; }
        public int BehaviourPoints { get; set; }
        public CreateLogNoteModel LogNote { get; set; }
        public CreateTaskModel Task { get; set; }
    }   
}
