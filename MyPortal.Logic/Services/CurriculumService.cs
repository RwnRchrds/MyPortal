using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Data.Curriculum;
using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CurriculumService : BaseUserService, ICurriculumService
    {
        public CurriculumService(ISessionUser user) : base(user)
        {
        }

        public async Task<IEnumerable<CurriculumBandModel>> GetCurriculumBands()
        {
            await using var unitOfWork = await User.GetConnection();

            var bands = await unitOfWork.CurriculumBands.GetAll();

            return bands.Select(b => new CurriculumBandModel(b)).ToArray();
        }

        public async Task<IEnumerable<CurriculumBandModel>> GetCurriculumBandsByYearGroup(Guid yearGroupId)
        {
            await using var unitOfWork = await User.GetConnection();

            var bands = await unitOfWork.CurriculumBands.GetCurriculumBandsByYearGroup(yearGroupId);

            return bands.Select(b => new CurriculumBandModel(b)).ToArray();
        }

        public async Task<CurriculumBandModel> GetCurriculumBandById(Guid bandId)
        {
            await using var unitOfWork = await User.GetConnection();

            var band = await unitOfWork.CurriculumBands.GetById(bandId);

            return new CurriculumBandModel(band);
        }

        public async Task CreateCurriculumBand(CurriculumBandRequestModel model)
        {
            Validate(model);
            
            await using var unitOfWork = await User.GetConnection();

            var band = new CurriculumBand
            {
                Id = Guid.NewGuid(),
                AcademicYearId = model.AcademicYearId,
                CurriculumYearGroupId = model.CurriculumYearGroupId,
                StudentGroup = StudentGroupHelper.CreateStudentGroupFromModel(model)
            };
            
            unitOfWork.CurriculumBands.Create(band);
            
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCurriculumBand(Guid bandId, CurriculumBandRequestModel model)
        {
            Validate(model);

            await using var unitOfWork = await User.GetConnection();

            var bandInDb = await unitOfWork.CurriculumBands.GetById(bandId);

            if (bandInDb == null)
            {
                throw new NotFoundException("Curriculum band not found.");
            }

            bandInDb.AcademicYearId = model.AcademicYearId;
            bandInDb.CurriculumYearGroupId = model.CurriculumYearGroupId;
            
            StudentGroupHelper.UpdateStudentGroupFromModel(bandInDb.StudentGroup, model);

            await unitOfWork.StudentGroups.Update(bandInDb.StudentGroup);
            await unitOfWork.CurriculumBands.Update(bandInDb);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCurriculumBand(Guid bandId)
        {
            await using var unitOfWork = await User.GetConnection();

            await unitOfWork.CurriculumBands.Delete(bandId);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CurriculumBlockModel>> GetCurriculumBlocks()
        {
            await using var unitOfWork = await User.GetConnection();

            var blocks = await unitOfWork.CurriculumBlocks.GetAll();

            return blocks.Select(b => new CurriculumBlockModel(b));
        }
    }
}