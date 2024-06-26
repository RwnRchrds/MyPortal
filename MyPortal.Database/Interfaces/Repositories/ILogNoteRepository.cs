﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ILogNoteRepository : IReadWriteRepository<LogNote>, IUpdateRepository<LogNote>
    {
        Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId, bool includePrivate);
    }
}