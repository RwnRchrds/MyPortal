using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Dtos;
using MyPortal.Logic.Models.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class SchoolService : BaseService, ISchoolService
    {
        private readonly ISchoolRepository _repository;

        public SchoolService(ISchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetLocalSchoolName()
        {
            return await _repository.GetLocalSchoolName();
        }

        public async Task<SchoolDto> GetLocalSchool()
        {
            var school = await _repository.GetLocal();

            return _businessMapper.Map<SchoolDto>(school);
        }

        public async Task CreateSchool(SchoolDto school)
        {
            ValidationHelper.ValidateModel(school);

            _repository.Create(_businessMapper.Map<School>(school));

            await _repository.SaveChanges();
        }

        public async Task UpdateSchool(SchoolDto school)
        {
            await _repository.Update(_businessMapper.Map<School>(school));

            await _repository.SaveChanges();
        }

        public async Task DeleteSchool(Guid id)
        {
            var schoolInDb = await _repository.GetById(id);

            if (schoolInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "School not found.");
            }

            if (schoolInDb.Local)
            {
                throw new ServiceException(ExceptionType.BadRequest, "Cannot delete local school.");
            }

            _repository.Delete(schoolInDb);

            await _repository.SaveChanges();
        }
    }
}
