using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class SchoolTypeRepository : BaseReadRepository<SchoolType>, ISchoolTypeRepository
    {
        public SchoolTypeRepository(DbTransaction transaction) : base(transaction)
        {
        }
    }
}