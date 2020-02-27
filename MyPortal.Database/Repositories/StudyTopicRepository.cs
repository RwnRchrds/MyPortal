using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudyTopicRepository : BaseReadWriteRepository<StudyTopic>, IStudyTopicRepository
    {
        public StudyTopicRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override async Task<IEnumerable<StudyTopic>> ExecuteQuery(string sql, object param = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(StudyTopic entity)
        {
            throw new System.NotImplementedException();
        }
    }
}