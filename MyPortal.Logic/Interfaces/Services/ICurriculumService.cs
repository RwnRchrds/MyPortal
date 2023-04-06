using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Requests.Curriculum;

namespace MyPortal.Logic.Interfaces.Services
{
    public interface ICurriculumService : IService
    {
        Task<IEnumerable<CurriculumBandModel>> GetCurriculumBands();
        Task<IEnumerable<CurriculumBandModel>> GetCurriculumBandsByYearGroup(Guid yearGroupId);
        Task<CurriculumBandModel> GetCurriculumBandById(Guid bandId);
        Task CreateCurriculumBand(CurriculumBandRequestModel model);
        Task UpdateCurriculumBand(Guid bandId, CurriculumBandRequestModel model);
        Task DeleteCurriculumBand(Guid bandId);
    }
}