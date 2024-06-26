﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ISubjectRepository : IReadWriteRepository<Subject>, IUpdateRepository<Subject>
    {
    }
}