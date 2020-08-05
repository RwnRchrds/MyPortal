using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseService, IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public AcademicYearService(ApplicationDbContext context)
        {
            _academicYearRepository = new AcademicYearRepository(context);
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

        public async Task Create(params AcademicYearModel[] academicYearModels)
        {
            throw new NotImplementedException();
        }

        public async Task Update(params AcademicYearModel[] academicYearModels)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(params Guid[] academicYearIds)
        {
            throw new NotImplementedException();
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
