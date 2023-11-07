using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IStudentGroupSupervisorRepository : IReadWriteRepository<StudentGroupSupervisor>,
        IUpdateRepository<StudentGroupSupervisor>
    {
    }
}