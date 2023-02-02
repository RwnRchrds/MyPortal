using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Incidents
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