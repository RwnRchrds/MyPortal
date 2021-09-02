using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class EnrolmentStatusRepository : BaseReadRepository<EnrolmentStatus>, IEnrolmentStatusRepository
    {
        public EnrolmentStatusRepository(DbTransaction transaction) : base(transaction)
        {
        }
    }
}