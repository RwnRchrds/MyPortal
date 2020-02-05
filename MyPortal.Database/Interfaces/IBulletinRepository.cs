using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IBulletinRepository : IReadWriteRepository<Bulletin>
    {
    }
}
