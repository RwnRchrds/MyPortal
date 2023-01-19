using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class RoomClosureModel : BaseModelWithLoad
    {
        public RoomClosureModel(RoomClosure model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(RoomClosure model)
        {
            RoomId = model.RoomId;
            ReasonId = model.ReasonId;
            StartDate = model.StartDate;
            EndDate = model.EndDate;
            Notes = model.Notes;

            if (model.Room != null)
            {
                Room = new RoomModel(model.Room);
            }

            if (model.Reason != null)
            {
                Reason = new RoomClosureReasonModel(model.Reason);
            }
        }
        
        
        public Guid RoomId { get; set; }
        
        public Guid ReasonId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        [StringLength(256)]
        public string Notes { get; set; }

        public virtual RoomModel Room { get; set; }
        public virtual RoomClosureReasonModel Reason { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.RoomClosures.GetById(Id.Value);
                LoadFromModel(model);
            }
        }
    }
}