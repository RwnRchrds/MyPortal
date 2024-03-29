﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAchievementOutcomeRepository : IReadWriteRepository<AchievementOutcome>,
        IUpdateRepository<AchievementOutcome>
    {
    }
}