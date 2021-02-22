using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces.Repositories
{
    internal interface IWriteRepository
    {
        Task SaveChanges();
    }
}
