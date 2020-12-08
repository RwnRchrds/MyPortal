using System;

namespace MyPortal.Logic.Models.Requests.Behaviour.Achievements
{
    public class CreateAchievementModel
    {
        public Guid AcademicYearId { get; set; }
        public Guid AchievementTypeId { get; set; }
        public Guid StudentId { get; set; }
        public Guid LocationId { get; set; }
        public Guid? OutcomeId { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }
    }
}