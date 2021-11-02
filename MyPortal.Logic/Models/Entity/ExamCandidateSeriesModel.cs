using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateSeriesModel : BaseModel, ILoadable
    {
        public ExamCandidateSeriesModel(ExamCandidateSeries model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamCandidateSeries model)
        {
            SeriesId = model.SeriesId;
            CandidateId = model.CandidateId;
            Flag = model.Flag;

            if (model.Series != null)
            {
                Series = new ExamSeriesModel(model.Series);
            }

            if (model.Candidate != null)
            {
                Candidate = new ExamCandidateModel(model.Candidate);
            }
        }
        
        public Guid SeriesId { get; set; }
        
        public Guid CandidateId { get; set; }
        
        public string Flag { get; set; }
        
        public virtual ExamSeriesModel Series { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamCandidateSeries.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}