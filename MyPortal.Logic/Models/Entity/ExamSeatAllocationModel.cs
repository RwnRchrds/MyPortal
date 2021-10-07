using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ExamSeatAllocationModel : BaseModel, ILoadable
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
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ExamSeatAllocations.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}