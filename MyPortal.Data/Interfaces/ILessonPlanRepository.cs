using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface ILessonPlanRepository : IReadWriteRepository<LessonPlan>
    {
        Task<IEnumerable<LessonPlan>> GetByStudyTopic(int studyTopicId);
    }
}
