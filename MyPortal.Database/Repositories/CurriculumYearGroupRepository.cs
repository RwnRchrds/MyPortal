using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class CurriculumYearGroupRepository : BaseReadRepository<CurriculumYearGroup>, ICurriculumYearGroupRepository
    {
        public CurriculumYearGroupRepository(DbUser dbUser) : base(dbUser)
        {
        }
    }
}