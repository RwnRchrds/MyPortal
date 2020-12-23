using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CurriculumYearGroupRepository : BaseReadRepository<CurriculumYearGroup>, ICurriculumYearGroupRepository
    {
        public CurriculumYearGroupRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}