using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Services
{
    public class AcademicYearService : BaseService, IAcademicYearService
    {
        private readonly IAcademicYearRepository _repository;

        public AcademicYearService(IAcademicYearRepository repository)
        {
            _repository = repository;
        }

        public async Task<AcademicYearDetails> GetCurrent()
        {
            var acadYear = await _repository.GetCurrent();

            return _businessMapper.Map<AcademicYearDetails>(acadYear);
        }
    }
}
