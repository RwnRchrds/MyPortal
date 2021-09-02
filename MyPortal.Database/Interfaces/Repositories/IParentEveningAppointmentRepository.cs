using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IParentEveningAppointmentRepository : IReadWriteRepository<ParentEveningAppointment>, IUpdateRepository<ParentEveningAppointment>
    {
        
    }
}