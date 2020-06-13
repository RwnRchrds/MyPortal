using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class GovernanceTypeRepository : BaseReadRepository<GovernanceType>, IGovernanceTypeRepository
    {
        public GovernanceTypeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
        }
    }
}