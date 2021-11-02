using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardEntryModel : BaseModel, ILoadable
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
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ReportCardEntries.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
