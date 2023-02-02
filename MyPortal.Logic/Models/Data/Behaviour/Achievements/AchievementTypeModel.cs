using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Achievements
{
    public class AchievementTypeModel : LookupItemModel
    {
        public int DefaultPoints { get; set; }

        public AchievementTypeModel(AchievementType model) : base(model)
        {
            DefaultPoints = model.DefaultPoints;
        }
    }
}
