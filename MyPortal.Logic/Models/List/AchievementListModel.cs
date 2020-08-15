using System;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.List
{
    public class AchievementListModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }

        public AchievementListModel(AchievementModel model)
        {
            Id = model.Id;
            TypeName = model.Type.Description;
            Location = model.Location.Description;
            RecordedBy = model.RecordedBy.GetDisplayName(true);
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Points = model.Points;
        }
    }
}