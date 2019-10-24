using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Exceptions;

namespace MyPortal.Processes
{
    public static class StudentProcesses
    {
        public static async Task CreateStudent(Student student, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(student))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            context.Persons.Add(student.Person);
            context.Students.Add(student);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteStudent(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(s => s.Id == studentId);

            if (studentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            studentInDb.Deleted = true;
            //context.Students.Remove(studentInDb);
            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<Student>> GetAllStudentsModel(MyPortalDbContext context)
        {
            var result = await context.Students.Include(x => x.Person).Include(x => x.PastoralYearGroup)
                .Include(x => x.PastoralRegGroup).Include(x => x.House).OrderBy(x => x.Person.LastName).ToListAsync();

            return result;
        }

        public static async Task<IEnumerable<StudentDto>> GetAllStudents(MyPortalDbContext context)
        {
            var students = await GetAllStudentsModel(context);

            return students.Select(Mapper.Map<Student, StudentDto>);
        }

        public static async Task<IEnumerable<GridStudentDto>> GetAllStudentsDataGrid(MyPortalDbContext context)
        {
            var students = await GetAllStudentsModel(context);

            return students.Select(Mapper.Map<Student, GridStudentDto>);
        }

        public static async Task<StudentDto> GetStudentById(int studentId, MyPortalDbContext context)
        {
            var student = await GetStudentByIdModel(studentId, context);

            return Mapper.Map<Student, StudentDto>(student);
        }

        public static async Task<Student> GetStudentByIdModel(int studentId, MyPortalDbContext context)
        {
            var student = await context.Students.SingleOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            return student;
        }

        public static string GetDisplayName(this Student student)
        {
            return $"{student.Person.LastName}, {student.Person.FirstName}";
        }

        public static async Task<string> GetStudentDisplayNameFromUserId(string userId)
        {
            var context = new MyPortalDbContext();

            var student = await GetStudentFromUserId(userId, context);

            return student.GetDisplayName();
        }

        public static async Task<Student> GetStudentFromUserId(string userId, MyPortalDbContext context)
        {
            var student = await context.Students.SingleOrDefaultAsync(x => x.Person.UserId == userId);

            if (student == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

            return student;
        }
        public static async Task<IEnumerable<StudentDto>> GetStudentsByRegGroup(int regGroupId, MyPortalDbContext context)
        {
            var students = await context.Students.Where(x => x.RegGroupId == regGroupId).OrderBy(x => x.Person.LastName)
                .ToListAsync();

            return students.Select(Mapper.Map<Student, StudentDto>);
        }

        public static async Task<IEnumerable<StudentDto>> GetStudentsByYearGroup(int yearGroupId,
            MyPortalDbContext context)
        {
            var students = await context.Students.Where(x => x.YearGroupId == yearGroupId).OrderBy(x => x.Person.LastName)
                .ToListAsync();

            return students.Select(Mapper.Map<Student, StudentDto>);
        }

        public static async Task<bool> StudentHasBasketItems(int studentId, MyPortalDbContext context)
        {
            return await context.FinanceBasketItems.AnyAsync(x => x.StudentId == studentId);
        }

        public static async Task<bool> StudentHasLogs(int studentId, MyPortalDbContext context)
        {
            return await context.ProfileLogs.AnyAsync(x => x.StudentId == studentId);
        }

        public static async Task<bool> StudentHasResults(int studentId, MyPortalDbContext context)
        {
            return await context.AssessmentResults.AnyAsync(x => x.StudentId == studentId);
        }

        public static async Task<bool> StudentHasSales(int studentId, MyPortalDbContext context)
        {
            return await context.FinanceSales.AnyAsync(x => x.StudentId == studentId);
        }

        public static async Task UpdateStudent(Student student, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(student))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            var studentInDb = await context.Students.SingleOrDefaultAsync(s => s.Id == student.Id);

            if (studentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Student not found");
            }

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

            await context.SaveChangesAsync();
        }
    }
}