﻿using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISystemSettingRepository
    {
        Task<SystemSetting> Get(string name);
        Task Update(string name, string value);
    }
}