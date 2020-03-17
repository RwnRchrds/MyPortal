using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Interfaces
{
    public interface IProfileLogNoteService
    {
        Task<IEnumerable<ProfileLogNoteDetails>> GetByStudent(Guid studentId);
    }
}
