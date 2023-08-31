using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class VatRateRepository : BaseReadWriteRepository<VatRate>, IVatRateRepository
    {
        public VatRateRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(VatRate entity)
        {
            var vatRate = await DbUser.Context.VatRates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (vatRate == null)
            {
                throw new EntityNotFoundException("VAT rate not found.");
            }

            vatRate.Description = entity.Description;
            vatRate.Active = entity.Active;
            vatRate.Value = entity.Value;
        }
    }
}