using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Attendance;
using MyPortal.Logic.Models.Data.Settings;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.ReportCards
{
    public class ReportCardEntryModel : BaseModelWithLoad
    {
        public ReportCardEntryModel(ReportCardEntry model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ReportCardEntry model)
        {
            ReportCardId = model.ReportCardId;
            CreatedById = model.CreatedById;
            WeekId = model.WeekId;
            PeriodId = model.PeriodId;
            Comments = model.Comments;

            if (model.ReportCard != null)
            {
                ReportCard = new ReportCardModel(model.ReportCard);
            }

            if (model.CreatedBy != null)
            {
                CreatedBy = new UserModel(model.CreatedBy);
            }

            if (model.AttendanceWeek != null)
            {
                AttendanceWeek = new AttendanceWeekModel(model.AttendanceWeek);
            }

            if (model.Period != null)
            {
                Period = new AttendancePeriodModel(model.Period);
            }
        }
        
        public Guid ReportCardId { get; set; }
        public Guid CreatedById { get; set; }
        public Guid WeekId { get; set; }
        public Guid PeriodId { get; set; }

        [StringLength(256)]
        public string Comments { get; set; }

        public virtual ReportCardModel ReportCard { get; set; }
        public virtual UserModel CreatedBy { get; set; }
        public virtual AttendanceWeekModel AttendanceWeek { get; set; }
        public virtual AttendancePeriodModel Period { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ReportCardEntries.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
