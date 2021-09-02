using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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