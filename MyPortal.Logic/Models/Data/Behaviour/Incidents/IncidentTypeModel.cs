using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Incidents
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