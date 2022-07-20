﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Student.LogNotes;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ILogNoteService
    {
        Task<LogNoteModel> GetLogNoteById(Guid logNoteId);
        Task<IEnumerable<LogNoteModel>> GetLogNotesByStudent(Guid studentId, Guid academicYearId, bool includeRestricted);
        Task<IEnumerable<LogNoteTypeModel>> GetLogNoteTypes();
        Task CreateLogNote(params CreateLogNoteRequestModel[] logNoteObjects);
        Task UpdateLogNote(params UpdateLogNoteRequestModel[] logNoteObjects);
        Task DeleteLogNote(params Guid[] logNoteIds);
    }
}
