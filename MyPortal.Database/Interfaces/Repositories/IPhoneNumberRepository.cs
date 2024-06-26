﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IPhoneNumberRepository : IReadWriteRepository<PhoneNumber>, IUpdateRepository<PhoneNumber>
    {
    }
}