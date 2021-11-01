using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardTargetModel : BaseModel, ILoadable
    {
        public ReportCardTargetModel(ReportCardTarget model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ReportCardTarget model)
        {
            ReportCardId = model.ReportCardId;
            TargetId = model.TargetId;

            if (model.ReportCard != null)
            {
                ReportCard = new ReportCardModel(model.ReportCard);
            }

            if (model.Target != null)
            {
                Target = new BehaviourTargetModel(model.Target);
            }
        }
        
        public Guid ReportCardId { get; set; }
        
        public Guid TargetId { get; set; }

        public virtual ReportCardModel ReportCard { get; set; }
        public virtual BehaviourTargetModel Target { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            var model = await unitOfWork.ReportCardTargets.GetById(Id);
            
            LoadFromModel(model);
        }
    }
}