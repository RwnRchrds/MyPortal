﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITrainingCourseRepository : IReadWriteRepository<TrainingCourse>, IUpdateRepository<TrainingCourse>
    {
    }
}