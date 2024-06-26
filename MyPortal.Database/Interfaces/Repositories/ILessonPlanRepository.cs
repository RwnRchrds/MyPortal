﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILessonPlanRepository : IReadWriteRepository<LessonPlan>, IUpdateRepository<LessonPlan>
    {
    }
}