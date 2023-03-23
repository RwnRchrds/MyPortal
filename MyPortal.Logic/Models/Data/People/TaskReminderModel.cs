using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.People;

public class TaskReminderModel : BaseModelWithLoad
{
    public TaskReminderModel(TaskReminder model) : base(model)
    {
        LoadFromModel(model);
    }

    private void LoadFromModel(TaskReminder model)
    {
        TaskId = model.TaskId;
        UserId = model.UserId;
        RemindTime = model.RemindTime;
        Dismissed = model.Dismissed;

        if (model.Task != null)
        {
            Task = new TaskModel(model.Task);
        }
    }
    
    public Guid TaskId { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime RemindTime { get; set; }
    
    public bool Dismissed { get; set; }
    
    public virtual TaskModel Task { get; set; }
    
    protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
    {
        if (Id.HasValue)
        {
            var model = await unitOfWork.TaskReminders.GetById(Id.Value);
                
            LoadFromModel(model);
        }
    }
}