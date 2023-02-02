using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamAwardSeriesModel : BaseModelWithLoad
    {
        public ExamAwardSeriesModel(ExamAwardSeries model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamAwardSeries model)
        {
            AwardId = model.AwardId;
            SeriesId = model.SeriesId;

            if (model.Award != null)
            {
                Award = new ExamAwardModel(model.Award);
            }

            if (model.Series != null)
            {
                Series = new ExamSeriesModel(model.Series);
            }
        }
        
        public Guid AwardId { get; set; }
        public Guid SeriesId { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamSeriesModel Series { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamAwardSeries.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}