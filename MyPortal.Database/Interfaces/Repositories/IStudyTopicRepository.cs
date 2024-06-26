﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudyTopicRepository : IReadWriteRepository<StudyTopic>, IUpdateRepository<StudyTopic>
    {
    }
}