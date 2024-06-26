﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IIncidentTypeRepository : IReadWriteRepository<IncidentType>, IUpdateRepository<IncidentType>
    {
    }
}