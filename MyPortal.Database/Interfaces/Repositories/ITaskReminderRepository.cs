using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories;

public interface ITaskReminderRepository : IReadWriteRepository<TaskReminder>, IUpdateRepository<TaskReminder>
{
    Task<IEnumerable<TaskReminder>> GetRemindersByTask(Guid taskId);
    Task<IEnumerable<TaskReminder>> GetRemindersByUser(Guid userId);
}