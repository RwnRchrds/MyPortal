﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamDateRepository : IReadWriteRepository<ExamDate>, IUpdateRepository<ExamDate>
    {
        
    }
}