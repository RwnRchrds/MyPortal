using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.Data.Interfaces
{
    public interface IStudyTopicRepository : IReadWriteRepository<StudyTopic>
    {
        Task<IEnumerable<StudyTopic>> GetBySubject(int subjectId);
    }
}
