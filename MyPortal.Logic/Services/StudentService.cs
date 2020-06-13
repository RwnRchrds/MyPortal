using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Database.Search;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Requests.Student;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private IStudentRepository _studentRepository;

        private StudentSearch GenerateSearchObject(StudentSearchModel searchModel)
        {
            return new StudentSearch
            {
                RegGroupId = searchModel.RegGroupId,
                YearGroupId = searchModel.YearGroupId,
                HouseId = searchModel.HouseId,
                SenStatusId = searchModel.SenStatusId,

                FirstName = searchModel.FirstName,
                LastName = searchModel.LastName,
                Gender = searchModel.Gender,
                Dob = searchModel.Dob
            };
        }

        public StudentService(IStudentRepository studentRepository) : base("Student")
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentModel> GetById(Guid studentId)
        {
            var student = await _studentRepository.GetById(studentId);
            if (student == null)
            {
                throw NotFound();
            }

            return _businessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentModel> GetByUserId(Guid userId)
        {
            var student = await _studentRepository.GetByUserId(userId);

            if (student == null)
            {
                throw NotFound();
            }

            return _businessMapper.Map<StudentModel>(student);
        }

        public Lookup GetSearchFilters()
        {
            var searchTypes = new Dictionary<string, Guid>();
            
            searchTypes.Add("All", Guid.Empty);
            searchTypes.Add("On Roll", SearchFilters.Students.OnRoll);
            searchTypes.Add("Leavers", SearchFilters.Students.Leavers);
            searchTypes.Add("Future", SearchFilters.Students.Future);

            return new Lookup(searchTypes);
        }

        public async Task<IEnumerable<StudentModel>> Get(StudentSearchModel searchModel)
        {
            var searchObject = GenerateSearchObject(searchModel);

            IEnumerable<Student> students;
            
            if (searchModel.SearchType == SearchFilters.Students.OnRoll)
            {
                students = await _studentRepository.GetOnRoll(searchObject);
            }
            else if (searchModel.SearchType == SearchFilters.Students.Leavers)
            {
                students = await _studentRepository.GetLeavers(searchObject);
            }
            else if (searchModel.SearchType == SearchFilters.Students.Future)
            {
                students = await _studentRepository.GetFuture(searchObject);
            }
            else
            {
                students = await _studentRepository.GetAll(searchObject);
            }

            return students.Select(_businessMapper.Map<StudentModel>).ToList();
        }

        public async Task Create(StudentModel student)
        {
            _studentRepository.Create(_businessMapper.Map<Student>(student));

            await _studentRepository.SaveChanges();
        }

        public async Task Update(StudentModel student)
        {
            var studentInDb = await _studentRepository.GetByIdWithTracking(student.Id);

            await _studentRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _studentRepository.Dispose();
        }
    }
}