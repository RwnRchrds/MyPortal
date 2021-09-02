using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMarksheetTemplateRepository : IReadWriteRepository<MarksheetTemplate>, IUpdateRepository<MarksheetTemplate>
    {
        
    }
}