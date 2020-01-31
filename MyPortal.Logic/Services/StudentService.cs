using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Dtos;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            return (await _repository.GetAll()).Select(_mapper.Map<StudentDto>).ToList();
        }

        public async Task CreateStudent(StudentDto student)
        {
            ValidationHelper.ValidateModel(student);

            _repository.Create(_mapper.Map<Student>(student));

            await _repository.SaveChanges();
        }
    }
}
