using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.People;

namespace MyPortal.Logic.Interfaces.Services;

public interface IReminderService : IService
{
    Task<ReminderDataModel> GetTaskReminderById(Guid reminderId);
    Task<IEnumerable<ReminderDataModel>> GetActiveRemindersByUser(Guid userId);
    Task DismissTaskReminder(Guid reminderId);
}