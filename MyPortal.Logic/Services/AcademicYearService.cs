using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;

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

            return _businessMapper.Map<AcademicYearModel>(acadYear);
        }
    }
}
