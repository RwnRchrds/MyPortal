using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamEnrolmentModel : BaseModel, ILoadable
    {
        public ExamEnrolmentModel(ExamEnrolment model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamEnrolment model)
        {
            AwardId = model.AwardId;
            CandidateId = model.CandidateId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            RegistrationNumber = model.RegistrationNumber;

            if (model.Award != null)
            {
                Award = new ExamAwardModel(model.Award);
            }

            if (model.Candidate != null)
            {
                Candidate = new ExamCandidateModel(model.Candidate);
            }
        }
        
        public Guid AwardId { get; set; }
        public Guid CandidateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string RegistrationNumber { get; set; }

        public virtual ExamAwardModel Award { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamEnrolments.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}