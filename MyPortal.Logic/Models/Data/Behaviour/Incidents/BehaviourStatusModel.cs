using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Incidents
{
    public class BehaviourStatusModel : LookupItemModel
    {
        public BehaviourStatusModel(BehaviourStatus model) : base(model)
        {
            Resolved = model.Resolved;
        }

        public bool Resolved { get; set; }
    }
}