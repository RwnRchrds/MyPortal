using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database;
using MyPortal.Database.Enums;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Attendance;
using MyPortal.Logic.Models.Response.Students;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<StudentModel> GetById(Guid studentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetById(studentId);
                if (student == null)
                {
                    throw new NotFoundException("Student not found.");
                }

                return BusinessMapper.Map<StudentModel>(student);
            }
        }

        public async Task<StudentStatsModel> GetStatsById(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var stats = new StudentStatsModel();

                var achievements = await unitOfWork.Achievements.GetPointsByStudent(studentId, academicYearId);
                var incidents = await unitOfWork.Incidents.GetPointsByStudent(studentId, academicYearId);
                var attendanceMarks = await unitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId);
                var exclusions = await unitOfWork.Exclusions.GetCountByStudent(studentId);
                var attendanceCodes = await unitOfWork.AttendanceCodes.GetAll();

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
        }

        public async Task<StudentModel> GetByUserId(Guid userId, bool throwNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetByUserId(userId);

                if (student == null && throwNotFound)
                {
                    throw new NotFoundException("Student not found.");
                }

                return BusinessMapper.Map<StudentModel>(student);
            }
        }

        public async Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetByPersonId(personId);

                if (student == null && throwIfNotFound)
                {
                    throw new NotFoundException("Student not found.");
                }

                return BusinessMapper.Map<StudentModel>(student);
            }
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
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var students = await unitOfWork.Students.GetAll(searchOptions);

                return students.Select(BusinessMapper.Map<StudentModel>).ToList();
            }
        }

        public async Task Create(StudentModel student)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                unitOfWork.Students.Create(BusinessMapper.Map<Student>(student));

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task Update(StudentModel student)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var studentInDb = await unitOfWork.Students.GetByIdForEditing(student.Id);

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}