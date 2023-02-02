using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Students;

using MyPortal.Logic.Models.Requests.Student.LogNotes;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ILogNoteService
    {
        Task<LogNoteModel> GetLogNoteById(Guid logNoteId);
        Task<IEnumerable<LogNoteModel>> GetLogNotesByStudent(Guid studentId, Guid academicYearId, bool includePrivate);
        Task<IEnumerable<LogNoteTypeModel>> GetLogNoteTypes();
        Task CreateLogNote(LogNoteRequestModel logNote);
        Task UpdateLogNote(Guid logNoteId, LogNoteRequestModel logNote);
        Task DeleteLogNote(Guid logNoteIds);
    }
}
