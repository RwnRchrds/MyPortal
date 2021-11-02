using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateSpecialArrangementModel : BaseModel, ILoadable
    {
        public ExamCandidateSpecialArrangementModel(ExamCandidateSpecialArrangement model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamCandidateSpecialArrangement model)
        {
            CandidateId = model.CandidateId;
            SpecialArrangementId = model.SpecialArrangementId;

            if (model.Candidate != null)
            {
                Candidate = new ExamCandidateModel(model.Candidate);
            }

            if (model.SpecialArrangement != null)
            {
                SpecialArrangement = new ExamSpecialArrangementModel(model.SpecialArrangement);
            }
        }

        public Guid CandidateId { get; set; }
        
        public Guid SpecialArrangementId { get; set; }

        public virtual ExamCandidateModel Candidate { get; set; }
        public virtual ExamSpecialArrangementModel SpecialArrangement { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamCandidateSpecialArrangements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}