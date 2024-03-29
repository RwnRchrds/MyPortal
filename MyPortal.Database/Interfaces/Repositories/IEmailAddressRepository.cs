﻿using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IEmailAddressRepository : IReadWriteRepository<EmailAddress>, IUpdateRepository<EmailAddress>
    {
    }
}