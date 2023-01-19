using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class AttendanceMarkModel : BaseModelWithLoad
    {
        public AttendanceMarkModel(AttendanceMark model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AttendanceMark model)
        {
            StudentId = model.StudentId;
            WeekId = model.WeekId;
            PeriodId = model.PeriodId;
            CodeId = model.CodeId;
            Comments = model.Comments;
            MinutesLate = model.MinutesLate;

            if (model.AttendancePeriod != null)
            {
                AttendancePeriod = new AttendancePeriodModel(model.AttendancePeriod);
            }

            if (model.AttendanceCode != null)
            {
                AttendanceCode = new AttendanceCodeModel(model.AttendanceCode);
            }

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }

            if (model.Week != null)
            {
                Week = new AttendanceWeekModel(model.Week);
            }
        }

        public Guid StudentId { get; set; }

        public Guid WeekId { get; set; }

        public Guid PeriodId { get; set; }

        public Guid CodeId { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public int MinutesLate { get; set; }

        public AttendancePeriodModel AttendancePeriod { get; set; }

        public AttendanceCodeModel AttendanceCode { get; set; }

        public StudentModel Student { get; set; }

        public AttendanceWeekModel Week { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var attendanceMark = await unitOfWork.AttendanceMarks.GetById(Id.Value);

                if (attendanceMark != null)
                {
                    LoadFromModel(attendanceMark);
                }
            }
        }
    }
}
