using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity;

public class BehaviourRoleTypeModel : LookupItemModel
{
    public BehaviourRoleTypeModel(BehaviourRoleType model) : base(model)
    {
        DefaultPoints = model.DefaultPoints;
    }

    public int DefaultPoints { get; set; }
}