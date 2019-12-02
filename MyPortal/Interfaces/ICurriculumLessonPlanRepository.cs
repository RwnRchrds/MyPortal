using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface ICurriculumLessonPlanRepository : IReadWriteRepository<CurriculumLessonPlan>
    {
        Task<IEnumerable<CurriculumLessonPlan>> GetByStudyTopic(int studyTopicId);
    }
}
