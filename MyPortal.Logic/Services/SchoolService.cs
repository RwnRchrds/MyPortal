using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Dictionaries;
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
