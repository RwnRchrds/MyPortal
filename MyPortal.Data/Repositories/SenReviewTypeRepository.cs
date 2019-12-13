using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class SenReviewTypeRepository : ReadRepository<SenReviewType>, ISenReviewTypeRepository
    {
        public SenReviewTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}