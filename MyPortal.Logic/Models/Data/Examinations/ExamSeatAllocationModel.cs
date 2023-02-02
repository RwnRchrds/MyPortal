using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Examinations
{
    public class ExamSeatAllocationModel : BaseModelWithLoad
    {
        public ExamSeatAllocationModel(ExamSeatAllocation model) : base(model)
        {
            LoadFromModel(model);
        }
        
        private void LoadFromModel(ExamSeatAllocation model)
        {
            SittingId = model.SittingId;
            SeatRow = model.SeatRow;
            SeatColumn = model.SeatColumn;
            CandidateId = model.CandidateId;
            Active = model.Active;
            Attended = model.Attended;

            if (model.Sitting != null)
            {
                Sitting = new ExamComponentSittingModel(model.Sitting);
            }

            if (model.Candidate != null)
            {
                Candidate = new ExamCandidateModel(model.Candidate);
            }
        }
        
        public Guid SittingId { get; set; }
        
        public int SeatRow { get; set; }
        
        public int SeatColumn { get; set; }

        public Guid CandidateId { get; set; }
        
        public bool Active { get; set; }
        
        public bool Attended { get; set; }

        public virtual ExamComponentSittingModel Sitting { get; set; }
        public virtual ExamCandidateModel Candidate { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ExamSeatAllocations.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}