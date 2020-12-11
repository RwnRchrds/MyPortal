using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseService, IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public AcademicYearService(IAcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<AcademicYearModel> GetCurrent()
        {
            var acadYear = await _academicYearRepository.GetCurrent();

            if (acadYear == null)
            {
                throw new NotFoundException("Current academic year not defined.");
            }

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public async Task<AcademicYearModel> GetById(Guid academicYearId)
        {
            throw new NotImplementedException();
        }

        public async Task<AcademicYearModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Create(AcademicYearModel academicYearModel)
        {
            var academicYear = BusinessMapper.Map<AcademicYear>(academicYearModel);

            _academicYearRepository.Create(academicYear);

            await _academicYearRepository.SaveChanges();
        }

        public async Task Update(params AcademicYearModel[] academicYearModels)
        {
            foreach (var academicYearModel in academicYearModels)
            {
                var academicYearInDb = await _academicYearRepository.GetById(academicYearModel.Id);

                academicYearInDb.Locked = academicYearModel.Locked;
            }
        }

        public async Task Delete(params Guid[] academicYearIds)
        {
            foreach (var academicYearId in academicYearIds)
            {
                var academicYearInDb = await _academicYearRepository.GetById(academicYearId);
            }
        }

        public async Task<bool> IsLocked(Guid academicYearId)
        {
            return await _academicYearRepository.IsLocked(academicYearId);
        }

        public override void Dispose()
        {
            _academicYearRepository.Dispose();
        }
    }
}
