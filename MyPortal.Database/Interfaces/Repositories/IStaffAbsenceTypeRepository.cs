﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStaffAbsenceTypeRepository : IReadWriteRepository<StaffAbsenceType>,
        IUpdateRepository<StaffAbsenceType>
    {
    }
}