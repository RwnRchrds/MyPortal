using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class SchoolTypeRepository : BaseReadRepository<SchoolType>, ISchoolTypeRepository
    {
        public SchoolTypeRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}