using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;
using MyPortal.Logic.Models.Data.School;

namespace MyPortal.Logic.Services;

public class ReminderService : BaseUserService, IReminderService
{
    public ReminderService(ISessionUser user) : base(user)
    {
    }


    public async Task<IEnumerable<ReminderDataModel>> GetActiveRemindersByUser(Guid userId)
    {
        var reminders = new List<ReminderDataModel>();
        
        await using var unitOfWork = await User.GetConnection();

        var taskReminders = await unitOfWork.TaskReminders.GetRemindersByUser(userId);

        foreach (var taskReminder in taskReminders)
        {
            if (taskReminder.RemindTime <= DateTime.Now)
            {
                reminders.Add(new ReminderDataModel
                {
                    Title = taskReminder.Task.Title,
                    Description = taskReminder.Task.Description,
                    ReminderType = ReminderTypes.Task,
                    DueDate = taskReminder.Task.DueDate,
                    EntityId = taskReminder.TaskId
                });
            }
        }

        return reminders.ToArray();
    }
}