using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentContactRelationshipRepository : IReadWriteRepository<StudentContactRelationship>, IUpdateRepository<StudentContactRelationship>
    {
    }
}
