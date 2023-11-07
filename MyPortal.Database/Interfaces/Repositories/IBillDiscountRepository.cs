using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBillDiscountRepository : IReadWriteRepository<BillDiscount>, IUpdateRepository<BillDiscount>
    {
    }
}