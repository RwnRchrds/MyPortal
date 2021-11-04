using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class BuildingFloorModel : LookupItemModel, ILoadable
    {
        public BuildingFloorModel(BuildingFloor model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(BuildingFloor model)
        {
            BuildingId = model.BuildingId;


            if (model.Building != null)
            {
                Building = new BuildingModel(model.Building);
            }
        }

        public Guid BuildingId { get; set; }

        public BuildingModel Building { get; set; }
        
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.BuildingFloors.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}