using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Models.Data.Behaviour.Achievements;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Students;

public class StudentAchievementModel : BaseModelWithLoad
{
    public StudentAchievementModel(StudentAchievement model) : base(model)
    {
        LoadFromModel(model);
    }

    private void LoadFromModel(StudentAchievement model)
    {
        StudentId = model.StudentId;
        AchievementId = model.AchievementId;
        OutcomeId = model.OutcomeId;
        Points = model.Points;

        if (model.Student != null)
        {
            Student = new StudentModel(model.Student);
        }

        if (model.Achievement != null)
        {
            Achievement = new AchievementModel(model.Achievement);
        }

        if (model.Outcome != null)
        {
            Outcome = new AchievementOutcomeModel(model.Outcome);
        }
    }
    
    public Guid StudentId { get; set; }
    
    public Guid AchievementId { get; set; }
    
    public Guid? OutcomeId { get; set; }
    
    public int Points { get; set; }

    [EagerLoad]
    public virtual StudentModel Student { get; set; }
    
    [EagerLoad]
    public virtual AchievementModel Achievement { get; set; }
    public virtual AchievementOutcomeModel Outcome { get; set; }
    
    protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var model = await unitOfWork.StudentAchievements.GetById(Id.Value);

            if (model == null)
            {
                throw new NotFoundException("Student achievement not found.");
            }
            
            LoadFromModel(model);
        }
    }
}