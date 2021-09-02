using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class BehaviourOutcomeModel : LookupItemModel
    {
        public BehaviourOutcomeModel(BehaviourOutcome model) : base(model)
        {
            System = model.System;
        }
        
        public bool System { get; set; }
    }
}