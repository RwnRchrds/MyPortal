using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Response.Students;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<StudentModel> GetById(Guid studentId)
        {
            var student = await UnitOfWork.Students.GetById(studentId);
            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentStatsModel> GetStatsById(Guid studentId, Guid academicYearId)
        {
            var stats = new StudentStatsModel();

            var achievements = await UnitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);
            var incidents = await UnitOfWork.Incidents.GetPointsByStudent(studentId, academicYearId);
            var attendanceMarks = await UnitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId);
            var exclusions = await UnitOfWork.Exclusions.GetCountByStudent(studentId);
            var attendanceCodes = await UnitOfWork.AttendanceCodes.GetAll();

            var attendanceSummary =
                new AttendanceSummary(attendanceCodes.Select(BusinessMapper.Map<AttendanceCodeModel>).ToList(),
                    attendanceMarks.Select(BusinessMapper.Map<AttendanceMarkModel>).ToList());

            stats.StudentId = studentId;
            stats.AchievementPoints = achievements;
            stats.BehaviourPoints = incidents;
            stats.PercentageAttendance = attendanceSummary.GetPresentAndAea(true);
            stats.Exclusions = exclusions;

            return stats;
        }

        public async Task<StudentModel> GetByUserId(Guid userId, bool throwNotFound = true)
        {
            var student = await UnitOfWork.Students.GetByUserId(userId);

            if (student == null && throwNotFound)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            var student = await UnitOfWork.Students.GetByPersonId(personId);

            if (student == null && throwIfNotFound)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public SelectList GetStudentStatusOptions(StudentStatus defaultStatus = StudentStatus.OnRoll)
        {
            var searchTypes = new Dictionary<string, int>();

            searchTypes.Add("Any", (int)StudentStatus.Any);
            searchTypes.Add("On Roll", (int)StudentStatus.OnRoll);
            searchTypes.Add("Leavers", (int)StudentStatus.Leavers);
            searchTypes.Add("Future", (int)StudentStatus.Future);

            return new SelectList(searchTypes, "Value", "Key", (int)defaultStatus);
        }

        public async Task<IEnumerable<StudentModel>> Get(StudentSearchOptions searchOptions)
        {
            var students = await UnitOfWork.Students.GetAll(searchOptions);

            return students.Select(BusinessMapper.Map<StudentModel>).ToList();
        }

        public async Task Create(StudentModel student)
        {
            UnitOfWork.Students.Create(BusinessMapper.Map<Student>(student));

            await UnitOfWork.SaveChanges();
        }

        public async Task Update(StudentModel student)
        {
            var studentInDb = await UnitOfWork.Students.GetByIdForEditing(student.Id);

            await UnitOfWork.SaveChanges();
        }
    }
}