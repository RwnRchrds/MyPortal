using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.School
{
    public class RoomModel : BaseModelWithLoad
    {
        public RoomModel(Room model) : base(model)
        {
           LoadFromModel(model);
        }

        private void LoadFromModel(Room model)
        {
            BuildingFloorId = model.BuildingFloorId;
            Code = model.Code;
            Name = model.Name;
            MaxGroupSize = model.MaxGroupSize;
            TelephoneNo = model.TelephoneNo;
            ExcludeFromCover = model.ExcludeFromCover;

            if (model.BuildingFloor != null)
            {
                BuildingFloor = new BuildingFloorModel(model.BuildingFloor);
            }
        }
        
        public Guid? BuildingFloorId { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        public int MaxGroupSize { get; set; }

        public string TelephoneNo { get; set; }

        public bool ExcludeFromCover { get; set; }

        public virtual BuildingFloorModel BuildingFloor { get; set; }
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Rooms.GetById(Id.Value);
            
                LoadFromModel(model);   
            }
        }
    }
}