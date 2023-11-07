using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface ITrainingCertificateStatusRepository : IReadRepository<TrainingCertificateStatus>,
        IUpdateRepository<TrainingCertificateStatus>
    {
    }
}