using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data.Students;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamCandidateModel : BaseModelWithLoad
    {
        public ExamCandidateModel(ExamCandidate model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ExamCandidate model)
        {
            StudentId = model.StudentId;
            Uci = model.Uci;
            CandidateNumber = model.CandidateNumber;
            PreviousCandidateNumber = model.PreviousCandidateNumber;
            PreviousCentreNumber = model.PreviousCentreNumber;
            SpecialConsideration = model.SpecialConsideration;
            Note = model.Note;

            if (model.Student != null)
            {
                Student = new StudentModel(model.Student);
            }
        }
        
        public Guid StudentId { get; set; }

        public string Uci { get; set; }

        public string CandidateNumber { get; set; }

        public string PreviousCandidateNumber { get; set; }

        public string PreviousCentreNumber { get; set; }

        public bool SpecialConsideration { get; set; }

        public string Note { get; set; }
        
        public virtual StudentModel Student { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamCandidates.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
