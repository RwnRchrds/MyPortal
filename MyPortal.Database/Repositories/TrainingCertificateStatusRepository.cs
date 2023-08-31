using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class TrainingCertificateStatusRepository : BaseReadWriteRepository<TrainingCertificateStatus>, ITrainingCertificateStatusRepository
    {
        public TrainingCertificateStatusRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(TrainingCertificateStatus entity)
        {
            var status = await DbUser.Context.TrainingCertificateStatus.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (status == null)
            {
                throw new EntityNotFoundException("Certificate status not found.");
            }

            status.Description = entity.Description;
            status.Active = entity.Active;
            status.ColourCode = entity.ColourCode;
        }
    }
}