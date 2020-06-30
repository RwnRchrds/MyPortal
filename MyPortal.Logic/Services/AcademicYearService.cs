using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseService, IAcademicYearService
    {
        private readonly IAcademicYearRepository _academicYearRepository;

        public AcademicYearService(IAcademicYearRepository academicYearRepository) : base("Academic year")
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<AcademicYearModel> GetCurrent()
        {
            var acadYear = await _academicYearRepository.GetCurrent();

            if (acadYear == null)
            {
                throw NotFound("Current academic year not defined.");
            }

            return BusinessMapper.Map<AcademicYearModel>(acadYear);
        }

        public override void Dispose()
        {
            _academicYearRepository.Dispose();
        }
    }
}
