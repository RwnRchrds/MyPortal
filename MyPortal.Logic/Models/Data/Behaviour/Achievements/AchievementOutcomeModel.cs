using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;

namespace MyPortal.Logic.Models.Data.Behaviour.Achievements
{
    public class AchievementOutcomeModel : LookupItemModel
    {
        public AchievementOutcomeModel(AchievementOutcome model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(AchievementOutcome model)
        {
            Description = model.Description;
            Active = model.Active;
        }

        public bool System { get; set; }
    }
}