using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IRoleRepository : IReadWriteRepository<Role>, IUpdateRepository<Role>
    {

    }
}
