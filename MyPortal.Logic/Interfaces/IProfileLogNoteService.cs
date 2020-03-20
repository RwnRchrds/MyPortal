using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Interfaces
{
    public interface IProfileLogNoteService
    {
        Task<IEnumerable<ProfileLogNoteDetails>> GetByStudent(Guid studentId);
        Task<Lookup> GetTypes();
        Task Create(params ProfileLogNoteDetails[] logNoteObjects);
        Task Update(params ProfileLogNoteDetails[] logNoteObjects);
        Task Delete(params Guid[] logNoteIds);
    }
}
