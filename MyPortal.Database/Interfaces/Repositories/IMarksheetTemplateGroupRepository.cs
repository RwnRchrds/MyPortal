using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IMarksheetTemplateGroupRepository : IReadWriteRepository<MarksheetTemplateGroup>, IUpdateRepository<MarksheetTemplateGroup>
    {
        
    }
}