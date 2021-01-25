using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Interfaces.Repositories;
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
        private IStudentRepository _studentRepository;
        private IAchievementRepository _achievementRepository;
        private IIncidentRepository _incidentRepository;
        private IAttendanceMarkRepository _attendanceMarkRepository;
        private IAttendanceCodeRepository _attendanceCodeRepository;
        private IExclusionRepository _exclusionRepository;

        public StudentService(IStudentRepository studentRepository, IAchievementRepository achievementRepository,
            IIncidentRepository incidentRepository, IAttendanceMarkRepository attendanceMarkRepository,
            IExclusionRepository exclusionRepository, IAttendanceCodeRepository attendanceCodeRepository)
        {
            _studentRepository = studentRepository;
            _achievementRepository = achievementRepository;
            _incidentRepository = incidentRepository;
            _attendanceMarkRepository = attendanceMarkRepository;
            _exclusionRepository = exclusionRepository;
            _attendanceCodeRepository = attendanceCodeRepository;
        }

        public async Task<StudentModel> GetById(Guid studentId)
        {
            var student = await _studentRepository.GetById(studentId);
            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentStatsModel> GetStatsById(Guid studentId, Guid academicYearId)
        {
            var stats = new StudentStatsModel();

            var achievements = await _achievementRepository.GetPointsByStudent(studentId, academicYearId);
            var incidents = await _incidentRepository.GetPointsByStudent(studentId, academicYearId);
            var attendanceMarks = await _attendanceMarkRepository.GetByStudent(studentId, academicYearId);
            var exclusions = await _exclusionRepository.GetCountByStudent(studentId);
            var attendanceCodes = await _attendanceCodeRepository.GetAll();

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
            var student = await _studentRepository.GetByUserId(userId);

            if (student == null && throwNotFound)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            var student = await _studentRepository.GetByPersonId(personId);

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
            var students = await _studentRepository.GetAll(searchOptions);

            return students.Select(BusinessMapper.Map<StudentModel>).ToList();
        }

        public async Task Create(StudentModel student)
        {
            _studentRepository.Create(BusinessMapper.Map<Student>(student));

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
            _achievementRepository.Dispose();
            _incidentRepository.Dispose();
            _attendanceMarkRepository.Dispose();
            _exclusionRepository.Dispose();
            _attendanceCodeRepository.Dispose();
        }
    }
}