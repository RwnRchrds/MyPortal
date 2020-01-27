using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface IDocumentTypeRepository : IReadRepository<IDocumentRepository, int>
    {
    }
}
