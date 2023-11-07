using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Students;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ISenService : IService
    {
        Task<IEnumerable<GiftedTalentedModel>> GetGiftedTalentedSubjects(Guid studentId);
    }
}