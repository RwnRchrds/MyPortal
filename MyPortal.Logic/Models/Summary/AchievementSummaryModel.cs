using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Summary
{
    public class AchievementSummaryModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }

        public AchievementSummaryModel(AchievementModel model)
        {
            if (model.Id.HasValue)
            {
                Id = model.Id.Value;   
            }
            TypeName = model.Type.Description;
            Location = model.Location.Description;
            RecordedBy = model.CreatedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Points = model.Points;
        }
    }
}