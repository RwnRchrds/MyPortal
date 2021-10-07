using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class IncidentTypeModel : LookupItemModel
    {
        public IncidentTypeModel(IncidentType model) : base(model)
        {
            DefaultPoints = model.DefaultPoints;
        }
        
        public int DefaultPoints { get; set; }
    }
}