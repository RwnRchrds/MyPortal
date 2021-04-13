using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class TrainingCertificateStatusRepository : BaseReadWriteRepository<TrainingCertificateStatus>, ITrainingCertificateStatusRepository
    {
        public TrainingCertificateStatusRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        public async Task Update(TrainingCertificateStatus entity)
        {
            var status = await Context.TrainingCertificateStatus.FirstOrDefaultAsync(x => x.Id == entity.Id);

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