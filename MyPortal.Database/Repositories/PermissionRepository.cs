using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class PermissionRepository : BaseReadRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DbTransaction transaction) : base(transaction)
        {
           
        }
    }
}
