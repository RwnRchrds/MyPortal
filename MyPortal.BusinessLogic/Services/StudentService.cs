using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class StudentService : MyPortalService
    {
        public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public StudentService() : base()
        {

        }

        public async Task CreateStudent(StudentDto student)
        {
            ValidationService.ValidateModel(student);

            UnitOfWork.Students.Add(Mapper.Map<Student>(student));

            await UnitOfWork.Complete();
        }

        public async Task DeleteStudent(int studentId)
        {
            var studentInDb = await UnitOfWork.Students.GetById(studentId);

            if (studentInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Student not found");
            }

            UnitOfWork.Results.RemoveRange(studentInDb.Results);
            UnitOfWork.AttendanceMarks.RemoveRange(studentInDb.AttendanceMarks);
            UnitOfWork.Achievements.RemoveRange(studentInDb.Achievements);
            UnitOfWork.Incidents.RemoveRange(studentInDb.Incidents);
            UnitOfWork.Enrolments.RemoveRange(studentInDb.Enrolments);
            UnitOfWork.BasketItems.RemoveRange(studentInDb.FinanceBasketItems);
            UnitOfWork.Sales.RemoveRange(studentInDb.Sales);
            UnitOfWork.MedicalEvent.RemoveRange(studentInDb.MedicalEvents);
            UnitOfWork.SenEvents.RemoveRange(studentInDb.SenEvents);
            UnitOfWork.SenProvisions.RemoveRange(studentInDb.SenProvisions);
            UnitOfWork.ProfileLogNotes.RemoveRange(studentInDb.ProfileLogs);
            UnitOfWork.GiftedTalented.RemoveRange(studentInDb.GiftedTalentedSubjects);

            UnitOfWork.Students.Remove(studentInDb);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            return (await UnitOfWork.Students.GetAll()).Select(Mapper.Map<StudentDto>).ToList();
        }

        public async Task<StudentDto> GetStudentById(int studentId)
        {
            var student = await UnitOfWork.Students.GetById(studentId);

            if (student == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Student not found");
            }

            return Mapper.Map<StudentDto>(student);
        }

        //public async Task<StudentDto> GetStudentByIdWithRelated(int studentId, params Expression<Func<StudentDto, object>>[] includeProperties)
        //{
        //    var student = await UnitOfWork.Students.GetByIdWithRelated(studentId, includeProperties);

        //    if (student == null)
        //    {
        //        throw new ServiceException(ExceptionType.NotFound, "Student not found");
        //    }

        //    return Mapping.Map<StudentDto>(student);
        //}

        public async Task<StudentDto> GetStudentByUserId(string userId)
        {
            var student = await UnitOfWork.Students.GetByUserId(userId);

            if (student == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Student not found");
            }

            return Mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> TryGetStudentByUserId(string userId)
        {
            var student = await UnitOfWork.Students.GetByUserId(userId);

            return Mapper.Map<StudentDto>(student);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByRegGroup(int regGroupId)
        {
            return (await UnitOfWork.Students.GetByRegGroup(regGroupId)).Select(Mapper.Map<StudentDto>).ToList();
        }
        
        public async Task<IEnumerable<StudentDto>> GetStudentsByYearGroup(int yearGroupId)
        {
            return (await UnitOfWork.Students.GetByYearGroup(yearGroupId)).Select(Mapper.Map<StudentDto>).ToList();
        }

        public async Task UpdateStudent(StudentDto student)
        {
            ValidationService.ValidateModel(student);

            var studentInDb = await UnitOfWork.Students.GetById(student.Id);

            studentInDb.HouseId = student.HouseId;
            studentInDb.Upn = student.Upn;
            studentInDb.CandidateNumber = student.CandidateNumber;
            studentInDb.FreeSchoolMeals = student.FreeSchoolMeals;
            studentInDb.GiftedAndTalented = student.GiftedAndTalented;
            studentInDb.PupilPremium = student.PupilPremium;
            studentInDb.RegGroupId = student.RegGroupId;
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.SenStatusId = student.SenStatusId;
            studentInDb.Uci = student.Uci;

            studentInDb.Person.FirstName = student.Person.FirstName;
            studentInDb.Person.LastName = student.Person.LastName;
            studentInDb.Person.Gender = student.Person.Gender;
            studentInDb.Person.Dob = student.Person.Dob;
            studentInDb.Person.MiddleName = student.Person.MiddleName;
            studentInDb.Person.PhotoId = student.Person.PhotoId;
            studentInDb.Person.NhsNumber = student.Person.NhsNumber;
            studentInDb.Person.Deceased = student.Person.Deceased;

            await UnitOfWork.Complete();
        }
    }
}