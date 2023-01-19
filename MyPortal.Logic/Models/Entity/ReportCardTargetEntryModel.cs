using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class ReportCardTargetEntryModel : BaseModelWithLoad
    {
        public ReportCardTargetEntryModel(ReportCardTargetEntry model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(ReportCardTargetEntry model)
        {
            EntryId = model.EntryId;
            TargetId = model.TargetId;
            TargetCompleted = model.TargetCompleted;

            if (model.Entry != null)
            {
                Entry = new ReportCardEntryModel(model.Entry);
            }

            if (model.Target != null)
            {
                Target = new ReportCardTargetModel(model.Target);
            }
        }
        
        public Guid EntryId { get; set; }
        
        public Guid TargetId { get; set; }
        
        public bool TargetCompleted { get; set; }

        public virtual ReportCardEntryModel Entry { get; set; }
        public virtual ReportCardTargetModel Target { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var targetEntry = await unitOfWork.ReportCardTargetEntries.GetById(Id.Value);

                if (targetEntry != null)
                {
                    LoadFromModel(targetEntry);
                }
            }
        }
    }
}