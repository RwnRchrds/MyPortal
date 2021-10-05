using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamCandidateModel : BaseModel, ILoadable
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
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamCandidates.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}
