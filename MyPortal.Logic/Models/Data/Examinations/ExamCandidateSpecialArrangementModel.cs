using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamCandidateSpecialArrangementModel : BaseModelWithLoad
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
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamCandidateSpecialArrangements.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}