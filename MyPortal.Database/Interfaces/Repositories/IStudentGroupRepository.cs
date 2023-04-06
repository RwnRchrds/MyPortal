using System;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentGroupRepository : IReadWriteRepository<StudentGroup>, IUpdateRepository<StudentGroup>
    {
    }
}