﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IContactRepository : IReadWriteRepository<Contact>, IUpdateRepository<Contact>
    {
    }
}