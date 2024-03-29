﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IYearGroupRepository : IBaseStudentGroupRepository<YearGroup>, IUpdateRepository<YearGroup>
    {
    }
}