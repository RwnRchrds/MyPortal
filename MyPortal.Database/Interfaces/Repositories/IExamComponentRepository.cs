﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamComponentRepository : IReadWriteRepository<ExamComponent>, IUpdateRepository<ExamComponent>
    {
    }
}