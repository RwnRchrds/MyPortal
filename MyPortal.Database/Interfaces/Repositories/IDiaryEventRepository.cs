using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IDiaryEventRepository : IReadWriteRepository<DiaryEvent>
    {
        Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime firstDate, DateTime lastDate, bool includePrivateEvents = false);

        Task<IEnumerable<DiaryEvent>> GetByPerson(DateTime firstDate, DateTime lastDate, Guid personId,
            bool includeDeclined = false);

        Task<IEnumerable<DiaryEvent>> GetLessonsByStudent(Guid studentId, DateTime firstDate, DateTime lastDate);

        Task<IEnumerable<DiaryEvent>> GetLessonsByTeacher(Guid staffMemberId, DateTime firstDate,
            DateTime lastDate);
    }
}
