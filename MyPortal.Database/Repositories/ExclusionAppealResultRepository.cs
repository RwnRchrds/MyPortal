using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExclusionAppealResultRepository : BaseReadRepository<ExclusionAppealResult>, IExclusionAppealResultRepository
    {
        public ExclusionAppealResultRepository(DbTransaction transaction) : base(transaction)
        {
        }
    }
}