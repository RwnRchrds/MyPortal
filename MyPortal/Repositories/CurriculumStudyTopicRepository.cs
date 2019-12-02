using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CurriculumStudyTopicRepository : ReadWriteRepository<CurriculumStudyTopic>, ICurriculumStudyTopicRepository
    {
        public CurriculumStudyTopicRepository(MyPortalDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<CurriculumStudyTopic>> GetBySubject(int subjectId)
        {
            return await Context.CurriculumStudyTopics.Where(x => x.SubjectId == subjectId).OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}