﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IAchievementTypeRepository : IReadWriteRepository<AchievementType>,
        IUpdateRepository<AchievementType>
    {
        Task<IEnumerable<AchievementType>> GetTypesWithRecordedAchievementsByYear(Guid academicYearId);
    }
}