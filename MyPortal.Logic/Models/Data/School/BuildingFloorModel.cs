using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.School
{
    public class BuildingFloorModel : LookupItemModelWithLoad
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
        
        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var floor = await unitOfWork.BuildingFloors.GetById(Id.Value);

                if (floor != null)
                {
                    LoadFromModel(floor);
                }
            }
        }
    }
}