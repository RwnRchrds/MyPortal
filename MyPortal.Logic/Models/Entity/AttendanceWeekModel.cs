using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceWeekModel : BaseModelWithLoad
    {
        public AttendanceWeekModel(AttendanceWeek model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AttendanceWeek model)
        {
            WeekPatternId = model.WeekPatternId;
            AcademicTermId = model.AcademicTermId;
            Beginning = model.Beginning;
            IsNonTimetable = model.IsNonTimetable;

            if (model.WeekPattern != null)
            {
                WeekPattern = new AttendanceWeekPatternModel(model.WeekPattern);
            }

            if (model.AcademicTerm != null)
            {
                AcademicTerm = new AcademicTermModel(model.AcademicTerm);
            }
        }
        
        public Guid WeekPatternId { get; set; }

        public Guid AcademicTermId { get; set; }

        public DateTime Beginning { get; set; }

        public bool IsNonTimetable { get; set; }

        public AttendanceWeekPatternModel WeekPattern { get; set; }
        public AcademicTermModel AcademicTerm { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var week = await unitOfWork.AttendanceWeeks.GetById(Id.Value);

                if (week != null)
                {
                    LoadFromModel(week);
                }
            }
        }
    }
}
