﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IClassRepository : IReadWriteRepository<Class>, IUpdateRepository<Class>
    {
    }
}