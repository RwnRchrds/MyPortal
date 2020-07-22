using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceMarkModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }

        [Required]
        [StringLength(1)]
        public string Mark { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public int MinutesLate { get; set; }

        public virtual AttendancePeriodModel AttendancePeriod { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual AttendanceWeekModel Week { get; set; }

        public AttendanceMarkListModel ToListModel()
        {
            return new AttendanceMarkListModel
            {
                Id = Id,
                StudentId = StudentId,
                WeekId = WeekId,
                PeriodId = PeriodId,
                Mark = Mark,
                MinutesLate = MinutesLate,
                Comments = Comments
            };
        }
    }
}
