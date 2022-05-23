﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Person.Tasks;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ITaskService
    {
        Task Create(params CreateTaskRequestModel[] tasks);

        Task Update(params UpdateTaskRequestModel[] tasks);

        Task Delete(params Guid[] taskIds);

        Task<bool> IsTaskOwner(Guid taskId, Guid userId);

        Task<IEnumerable<TaskTypeModel>> GetTypes(bool personalOnly, bool activeOnly = true);

        Task<TaskModel> GetById(Guid taskId);

        Task<IEnumerable<TaskModel>> GetByPerson(Guid personId, TaskSearchOptions searchOptions = null);
        Task SetCompleted(Guid taskId, bool completed);
    }
}
