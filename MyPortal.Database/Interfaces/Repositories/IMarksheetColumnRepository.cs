using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMarksheetColumnRepository : IReadWriteRepository<MarksheetColumn>,
        IUpdateRepository<MarksheetColumn>
    {
        Task<IEnumerable<MarksheetColumn>> GetByMarksheet(Guid marksheetId);
    }
}