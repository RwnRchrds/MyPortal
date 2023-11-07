using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Behaviour.ReportCards
{
    public class ReportCardTargetModel : BaseModelWithLoad
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

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.ReportCardTargets.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}