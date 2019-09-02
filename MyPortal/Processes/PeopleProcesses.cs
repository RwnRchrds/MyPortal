using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PeopleProcesses
    {
        public static ProcessResponse<StaffMember> GetStaffFromUserId(string userId, MyPortalDbContext context)
        {
            var staff = context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (staff == null)
            {
                return new ProcessResponse<StaffMember>(ResponseType.NotFound, "Staff record not found", null);
            }

            return new ProcessResponse<StaffMember>(ResponseType.Ok, null, staff);
        }

        public static ProcessResponse<Student> GetStudentFromUserId(string userId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(x => x.Person.UserId == userId);

            if (student == null)
            {
                return new ProcessResponse<Student>(ResponseType.NotFound, "Student not found", null);
            }

            return new ProcessResponse<Student>(ResponseType.Ok, null, student);
        }

        public static ProcessResponse<string> GetStudentDisplayName(Student student)
        {
            var displayName = $"{student.Person.LastName}, {student.Person.FirstName}";
            return new ProcessResponse<string>(ResponseType.Ok, null, displayName);
        }

        public static ProcessResponse<string> GetStaffDisplayName(StaffMember staffMember)
        {
           var displayName = $"{staffMember.Person.Title} {staffMember.Person.FirstName.Substring(0,1)} {staffMember.Person.LastName}";
           return new ProcessResponse<string>(ResponseType.Ok, null, displayName);
        }

        public static ProcessResponse<string> GetStaffDisplayNameFromUserId(string userId)
        {
            var context = new MyPortalDbContext();

            var staffMember = GetStaffFromUserId(userId, context);

            if (staffMember.ResponseType == ResponseType.BadRequest)
            {
                return new ProcessResponse<string>(ResponseType.BadRequest, staffMember.ResponseMessage, null);
            }

            if (staffMember.ResponseType == ResponseType.NotFound)
            {
                return new ProcessResponse<string>(ResponseType.NotFound, staffMember.ResponseMessage, null);
            }

            return GetStaffDisplayName(staffMember.ResponseObject);
        }

        public static ProcessResponse<string> GetStaffFullName(StaffMember staffMember)
        {
            var fullName = $"{staffMember.Person.LastName}, {staffMember.Person.FirstName}";
            
            return new ProcessResponse<string>(ResponseType.Ok, null, fullName);
        }

        public static ProcessResponse<StaffMember> HandleAuthorFromUserId(string userId, int authorId, MyPortalDbContext context)
        {
            var author = new StaffMember();

            if (authorId == 0)
            {
                
                author = GetStaffFromUserId(userId, context).ResponseObject;
                if (author == null)
                {
                    return new ProcessResponse<StaffMember>(ResponseType.NotFound, "Staff member not found", null);
                }
            }

            if (authorId != 0) author = context.StaffMembers.SingleOrDefault(x => x.Id == authorId);

            if (author == null)
            {
                return new ProcessResponse<StaffMember>(ResponseType.NotFound, "Staff member not found", null);
            }

            return new ProcessResponse<StaffMember>(ResponseType.Ok, null, author);
        }

        public static ProcessResponse<object> CreateStaffMember(StaffMember staffMember, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(staffMember))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.Persons.Add(staffMember.Person);
            context.StaffMembers.Add(staffMember);

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Staff member created", null);
        }

        public static ProcessResponse<object> DeleteStaffMember(int staffMemberId, string userId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.Single(x => x.Id == staffMemberId);

            if (staffInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            if (staffInDb.Person.UserId == userId)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot delete yourself", null);
            }

            staffInDb.Deleted = true;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Staff member deleted", null);
        }

        public static ProcessResponse<object> UpdateStaffMember(StaffMember staffMember, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.SingleOrDefault(x => x.Id == staffMember.Id);

            if (staffInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            if (context.StaffMembers.Any(x => x.Code == staffMember.Code) && staffInDb.Code != staffMember.Code)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Staff code already exists", null);
            }

            staffInDb.Person.FirstName = staffMember.Person.FirstName;
            staffInDb.Person.LastName = staffMember.Person.LastName;
            staffInDb.Person.Title = staffMember.Person.Title;
            staffInDb.Code = staffMember.Code;
            staffInDb.JobTitle = staffMember.JobTitle;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Staff member updated", null);
        }
        
        public static ProcessResponse<IEnumerable<StaffMember>> GetAllStaffMembers_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<StaffMember>>(ResponseType.Ok, null, context.StaffMembers
                .Where(x => !x.Deleted)
                .OrderBy(x => x.Person.LastName)
                .ToList());
        }

        public static ProcessResponse<IEnumerable<StaffMemberDto>> GetAllStaffMembers(MyPortalDbContext context)
                 {
                     return new ProcessResponse<IEnumerable<StaffMemberDto>>(ResponseType.Ok, null,
                         GetAllStaffMembers_Model(context).ResponseObject
                             .Select(Mapper.Map<StaffMember, StaffMemberDto>));
                 }
        
        public static ProcessResponse<IEnumerable<GridStaffMemberDto>> GetAllStaffMembers_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridStaffMemberDto>>(ResponseType.Ok, null,
                GetAllStaffMembers_Model(context).ResponseObject
                    .Select(Mapper.Map<StaffMember, GridStaffMemberDto>));
        }

        public static ProcessResponse<StaffMemberDto> GetStaffMemberById(int staffMemberId, MyPortalDbContext context)
        {
            var staff = context.StaffMembers.SingleOrDefault(s => s.Id == staffMemberId);

            if (staff == null)
            {
                return new ProcessResponse<StaffMemberDto>(ResponseType.NotFound, "Staff member not found", null);
            }

            return new ProcessResponse<StaffMemberDto>(ResponseType.Ok, null,
                Mapper.Map<StaffMember, StaffMemberDto>(staff));
        }

        public static ProcessResponse<bool> StaffMemberHasDocuments(int staffMemberId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.SingleOrDefault(x => x.Id == staffMemberId);

            if (staffInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Staff member not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, staffInDb.Documents.Any());
        }

        public static ProcessResponse<bool> StaffMemberHasWrittenLogs(int staffMemberId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.SingleOrDefault(x => x.Id == staffMemberId);

            if (staffInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Staff member not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null,
                context.ProfileLogs.Any(x => x.AuthorId == staffMemberId));
        }

        public static ProcessResponse<object> CreateStudent(Student student, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(student))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.Persons.Add(student.Person);
            context.Students.Add(student);

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Student created", null);
        }

        public static ProcessResponse<object> DeleteStudent(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(s => s.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            studentInDb.Deleted = true;
            //context.Students.Remove(studentInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Student deleted", null);
        }

        public static ProcessResponse<StudentDto> GetStudentById(int studentId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<StudentDto>(ResponseType.NotFound, "Student not found", null);
            }

            return new ProcessResponse<StudentDto>(ResponseType.Ok, null, Mapper.Map<Student, StudentDto>(student));
        }

        public static ProcessResponse<Student> GetStudentById_Model(int studentId, MyPortalDbContext context)
        {
            var student = context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                return new ProcessResponse<Student>(ResponseType.NotFound, "Student not found", null);
            }

            return new ProcessResponse<Student>(ResponseType.Ok, null, student);
        }

        public static ProcessResponse<IEnumerable<StudentDto>> GetAllStudents(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<StudentDto>>(ResponseType.Ok, null, context.Students
                .OrderBy(x => x.Person.LastName)
                .ToList()
                .Select(Mapper.Map<Student, StudentDto>));
        }

        public static ProcessResponse<IEnumerable<GridStudentDto>> GetAllStudents_DataGrid(MyPortalDbContext context)
        {
            var students = context.Students.OrderBy(x => x.Person.LastName).ToList().Select(Mapper.Map<Student, GridStudentDto>);

            return new ProcessResponse<IEnumerable<GridStudentDto>>(ResponseType.Ok, null, students);
            ;
        }

        public static ProcessResponse<IEnumerable<StudentDto>> GetStudentsByRegGroup(int regGroupId, MyPortalDbContext context)
        {
            var students = context.Students.Where(x => x.RegGroupId == regGroupId).OrderBy(x => x.Person.LastName)
                .ToList().Select(Mapper.Map<Student, StudentDto>);

            return new ProcessResponse<IEnumerable<StudentDto>>(ResponseType.Ok, null, students);
        }

        public static ProcessResponse<IEnumerable<StudentDto>> GetStudentsByYearGroup(int yearGroupId,
            MyPortalDbContext context)
        {
            var students = context.Students.Where(x => x.YearGroupId == yearGroupId).OrderBy(x => x.Person.LastName)
                .ToList().Select(Mapper.Map<Student, StudentDto>);

            return new ProcessResponse<IEnumerable<StudentDto>>(ResponseType.Ok, null, students);
        }

        public static ProcessResponse<bool> StudentHasBasketItems(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, studentInDb.FinanceBasketItems.Any());
        }

        public static ProcessResponse<bool> StudentHasDocuments(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null,
                context.PersonDocuments.Any(x => x.PersonId == studentInDb.PersonId));
        }

        public static ProcessResponse<bool> StudentHasLogs(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, studentInDb.ProfileLogs.Any());
        }

        public static ProcessResponse<bool> StudentHasResults(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, studentInDb.AssessmentResults.Any());
        }

        public static ProcessResponse<bool> StudentHasSales(int studentId, MyPortalDbContext context)
        {
            var studentInDb = context.Students.SingleOrDefault(x => x.Id == studentId);

            if (studentInDb == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, studentInDb.FinanceSales.Any());
        }

        public static ProcessResponse<object> UpdateStudent(Student student, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(student))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var studentInDb = context.Students.SingleOrDefault(s => s.Id == student.Id);

            if (studentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            Mapper.Map(student, studentInDb);
            studentInDb.Person.FirstName = student.Person.FirstName;
            studentInDb.Person.LastName = student.Person.LastName;
            studentInDb.Person.Gender = student.Person.Gender;
            studentInDb.RegGroupId = student.RegGroupId;
            studentInDb.YearGroupId = student.YearGroupId;
            studentInDb.AccountBalance = student.AccountBalance;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Student updated", null);
        }

        public static ProcessResponse<Person> GetPersonByUserId(string userId, MyPortalDbContext context)
        {
            var person = context.Persons.SingleOrDefault(x => x.UserId == userId);

            if (person == null)
            {
                return new ProcessResponse<Person>(ResponseType.NotFound, "Person not found", null);
            }
            
            return new ProcessResponse<Person>(ResponseType.Ok, null, person);
        }
    }
}