﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMedicalConditionRepository : IReadWriteRepository<MedicalCondition>,
        IUpdateRepository<MedicalCondition>
    {
    }
}