using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces
{
    public interface IReadOnlyEntity : IEntity
    {
        // Read only entities are created during database setup and the tables should never be modified from within the application
    }
}
