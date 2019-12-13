using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class StudyTopicRepository : ReadWriteRepository<StudyTopic>, IStudyTopicRepository
    {
        public StudyTopicRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<StudyTopic>> GetBySubject(int subjectId)
        {
            return await Context.StudyTopics.Where(x => x.SubjectId == subjectId).OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}