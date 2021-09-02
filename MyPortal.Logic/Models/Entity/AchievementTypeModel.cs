using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
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
