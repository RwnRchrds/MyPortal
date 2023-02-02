using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data.Students;


namespace MyPortal.Logic.Models.Summary
{
    public class StudentAchievementSummaryModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }

        private StudentAchievementSummaryModel(StudentAchievementModel model)
        {
            if (model.Id.HasValue)
            {
                Id = model.Id.Value;   
            }
            TypeName = model.Achievement.Type.Description;
            Location = model.Achievement.Location.Description;
            RecordedBy = model.Achievement.CreatedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            CreatedDate = model.Achievement.CreatedDate;
            Comments = model.Achievement.Comments;
            Points = model.Points;
        }
        
        public static async Task<StudentAchievementSummaryModel> GetSummary(IUnitOfWork unitOfWork, StudentAchievementModel model)
        {
            await model.Student.Load(unitOfWork);
            await model.Achievement.Load(unitOfWork);

            return new StudentAchievementSummaryModel(model);
        }
    }
}