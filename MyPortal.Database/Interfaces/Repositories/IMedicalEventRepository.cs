﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMedicalEventRepository : IReadWriteRepository<MedicalEvent>, IUpdateRepository<MedicalEvent>
    {
    }
}