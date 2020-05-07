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
using MyPortal.Logic.Models.Summary;

namespace MyPortalCore.Areas.Staff.ViewModels.Student
{
    public class StudentOverviewViewModel
    {
        private readonly IStudentService _service;
        private readonly IPersonService _personService;
        private readonly ILogNoteService _logNoteService;
        private readonly Guid _studentId;
        private readonly Guid _academicYearId;
        private readonly IMapper _mapper;

        public StudentOverviewViewModel(IStudentService service, IPersonService personService, ILogNoteService logNoteService, Guid studentId, Guid academicYearId)
        {
            _service = service;
            _personService = personService;
            _logNoteService = logNoteService;
            _studentId = studentId;
            _academicYearId = academicYearId;
            _mapper = MappingHelper.GetDataGridConfig();
        }

        public async Task LoadData()
        {
            Student = await _service.GetById(_studentId);

            LogNotes =
                (await _logNoteService.GetByStudent(_studentId, (Guid) _academicYearId)).Select(
                    _mapper.Map<LogNoteSummary>).OrderByDescending(x => x.Date);

            LogNoteTypes =
                (await _logNoteService.GetTypes()).Select(x => new SelectListItem(x.Key, x.Value.ToString()));

            LogNote = new LogNoteModel();
        }

        public StudentModel Student { get; set; }
        public IEnumerable<LogNoteSummary> LogNotes { get; set; }
        public IEnumerable<SelectListItem> LogNoteTypes { get; set; }
        public decimal Attendance { get; set; }
        public int AchievementPoints { get; set; }
        public int BehaviourPoints { get; set; }
        public LogNoteModel LogNote { get; set; }  
    }   
}
