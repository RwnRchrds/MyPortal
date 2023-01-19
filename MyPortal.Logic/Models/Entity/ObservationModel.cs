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
    public class ObservationModel : BaseModelWithLoad
    {
        public ObservationModel(Observation model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Observation model)
        {
            Date = model.Date;
            ObserveeId = model.ObserveeId;
            ObserverId = model.ObserverId;
            OutcomeId = model.OutcomeId;

            if (model.Observee != null)
            {
                Observee = new StaffMemberModel(model.Observee);
            }

            if (model.Observer != null)
            {
                Observer = new StaffMemberModel(model.Observer);
            }

            if (model.Outcome != null)
            {
                Outcome = new ObservationOutcomeModel(model.Outcome);
            }
        }

        public DateTime Date { get; set; }
        public Guid ObserveeId { get; set; }
        public Guid ObserverId { get; set; }
        public Guid OutcomeId { get; set; }

        public virtual StaffMemberModel Observee { get; set; }
        public virtual StaffMemberModel Observer { get; set; }
        public virtual ObservationOutcomeModel Outcome { get; set; }
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Observations.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}
