using System.Data;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class BehaviourStatusRepository : BaseReadRepository<BehaviourStatus>
    {
        public BehaviourStatusRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }
    }
}