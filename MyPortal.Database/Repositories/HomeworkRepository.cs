using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class HomeworkRepository : BaseReadWriteRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
        }
    }
}