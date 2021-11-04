using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BuildingModel : LookupItemModel
    {
        public BuildingModel(Building model) : base(model)
        {
        }
    }
}