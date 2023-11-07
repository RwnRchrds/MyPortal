using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.People;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services;

public class ReminderService : BaseService, IReminderService
{
    public ReminderService(ISessionUser user) : base(user)
    {
    }


    public async Task<ReminderDataModel> GetTaskReminderById(Guid reminderId)
    {
        await using var unitOfWork = await User.GetConnection();

        TaskReminder reminder = await unitOfWork.TaskReminders.GetById(reminderId);

        if (reminder == null)
        {
            throw new NotFoundException("Task reminder not found.");
        }

        return new ReminderDataModel
        {
            Id = reminder.Id,
            Title = reminder.Task.Title,
            Description = reminder.Task.Description,
            ReminderType = ReminderTypes.Task,
            DueDate = reminder.Task.DueDate,
            EntityId = reminder.TaskId
        };
    }

    public async Task<IEnumerable<ReminderDataModel>> GetActiveRemindersByUser(Guid userId)
    {
        var reminders = new List<ReminderDataModel>();

        await using var unitOfWork = await User.GetConnection();

        IEnumerable<TaskReminder> taskReminders = await unitOfWork.TaskReminders.GetRemindersByUser(userId);

        foreach (var taskReminder in taskReminders)
        {
            if (taskReminder.RemindTime <= DateTime.Now)
            {
                reminders.Add(new ReminderDataModel
                {
                    Id = taskReminder.Id,
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

    public async Task DismissTaskReminder(Guid reminderId)
    {
        await using var unitOfWork = await User.GetConnection();

        TaskReminder reminder = await unitOfWork.TaskReminders.GetById(reminderId);

        if (reminder == null)
        {
            throw new NotFoundException("Task reminder not found.");
        }

        await unitOfWork.TaskReminders.Delete(reminderId);
    }
}