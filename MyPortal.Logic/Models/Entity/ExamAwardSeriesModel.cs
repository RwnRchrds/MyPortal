using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamAwardSeriesModel : BaseModel, ILoadable
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
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamAwardSeries.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}