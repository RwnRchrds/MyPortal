﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamElementComponentRepository : IReadWriteRepository<ExamElementComponent>, IUpdateRepository<ExamElementComponent>
    {
        
    }
}