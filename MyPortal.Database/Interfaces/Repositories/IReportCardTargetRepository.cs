﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReportCardTargetRepository : IReadWriteRepository<ReportCardTarget>, IUpdateRepository<ReportCardTarget>
    {
        
    }
}