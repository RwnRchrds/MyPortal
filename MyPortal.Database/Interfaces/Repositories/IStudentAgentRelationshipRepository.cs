using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentAgentRelationshipRepository : IReadWriteRepository<StudentAgentRelationship>,
        IUpdateRepository<StudentAgentRelationship>
    {
    }
}