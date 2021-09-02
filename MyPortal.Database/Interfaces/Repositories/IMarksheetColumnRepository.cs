using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMarksheetColumnRepository : IReadWriteRepository<MarksheetColumn>, IUpdateRepository<MarksheetColumn>
    {
        
    }
}