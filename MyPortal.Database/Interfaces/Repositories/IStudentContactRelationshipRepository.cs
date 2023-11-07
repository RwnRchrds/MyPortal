using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentContactRelationshipRepository : IReadWriteRepository<StudentContactRelationship>,
        IUpdateRepository<StudentContactRelationship>
    {
        Task<IEnumerable<StudentContactRelationship>> GetRelationshipsByContact(Guid contactId);
        Task<IEnumerable<StudentContactRelationship>> GetRelationshipsByStudent(Guid studentId);
    }
}