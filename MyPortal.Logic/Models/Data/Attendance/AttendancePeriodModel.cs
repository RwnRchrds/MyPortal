using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Attendance
{
    public class AttendancePeriodModel : BaseModelWithLoad
    {
        public AttendancePeriodModel(AttendancePeriod model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AttendancePeriod model)
        {
            WeekPatternId = model.WeekPatternId;
            Weekday = model.Weekday;
            Name = model.Name;
            StartTime = model.StartTime;
            EndTime = model.EndTime;
            AmReg = model.AmReg;
            PmReg = model.PmReg;

            if (model.WeekPattern != null)
            {
                WeekPattern = new AttendanceWeekPatternModel(model.WeekPattern);
            }
        }

        public Guid WeekPatternId { get; set; }

        public DayOfWeek Weekday { get; set; }

        [Required] [StringLength(128)] public string Name { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool AmReg { get; set; }

        public bool PmReg { get; set; }

        public AttendanceWeekPatternModel WeekPattern { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var period = await unitOfWork.AttendancePeriods.GetById(Id.Value);

                if (period != null)
                {
                    LoadFromModel(period);
                }
            }
        }
    }
}