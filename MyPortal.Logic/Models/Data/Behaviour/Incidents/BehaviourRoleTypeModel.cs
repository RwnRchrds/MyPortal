using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Incidents;

public class BehaviourRoleTypeModel : LookupItemModel
{
    public BehaviourRoleTypeModel(BehaviourRoleType model) : base(model)
    {
        DefaultPoints = model.DefaultPoints;
    }

    public int DefaultPoints { get; set; }
}