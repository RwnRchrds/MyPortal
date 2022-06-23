using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Scaffolding;
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
using MyPortal.Logic.Models.Reporting;
using MyPortal.Logic.Models.Requests.Student;
using MyPortal.Logic.Models.Response.Students;
using MyPortal.Logic.Models.Summary;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<StudentModel> GetStudentById(Guid studentId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetById(studentId);
                if (student == null)
                {
                    throw new NotFoundException("Student not found.");
                }

                return new StudentModel(student);
            }
        }

        public async Task<StudentStatsResponseModel> GetStatsById(Guid studentId, Guid academicYearId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var stats = new StudentStatsResponseModel();

                var achievements = await unitOfWork.StudentAchievements.GetPointsByStudent(studentId, academicYearId);
                var incidents = await unitOfWork.StudentIncidents.GetPointsByStudent(studentId, academicYearId);
                var attendanceMarks = await unitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId);
                var exclusions = await unitOfWork.Exclusions.GetCountByStudent(studentId);
                var attendanceCodes = await unitOfWork.AttendanceCodes.GetAll();

                var attendanceSummary =
                    new AttendanceSummary(attendanceCodes.Select(c => new AttendanceCodeModel(c)).ToList(),
                        attendanceMarks.Select(m => new AttendanceMarkModel(m)).ToList());

                stats.StudentId = studentId;
                stats.AchievementPoints = achievements;
                stats.BehaviourPoints = incidents;
                stats.PercentageAttendance = attendanceSummary.GetPresentAndAea(true);
                stats.Exclusions = exclusions;

                return stats;
            }
        }

        public async Task<StudentModel> GetStudentByUserId(Guid userId, bool throwNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetByUserId(userId);

                if (student == null && throwNotFound)
                {
                    throw new NotFoundException("Student not found.");
                }

                return new StudentModel(student);
            }
        }

        public async Task<StudentModel> GetStudentByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var student = await unitOfWork.Students.GetByPersonId(personId);

                if (student == null && throwIfNotFound)
                {
                    throw new NotFoundException("Student not found.");
                }

                return new StudentModel(student);
            }
        }

        public Dictionary<string, int> GetStudentStatusOptions()
        {
            var searchTypes = new Dictionary<string, int>();

            searchTypes.Add("Any", (int)StudentStatus.Any);
            searchTypes.Add("On Roll", (int)StudentStatus.OnRoll);
            searchTypes.Add("Leavers", (int)StudentStatus.Leavers);
            searchTypes.Add("Future", (int)StudentStatus.Future);

            return searchTypes;
        }

        public async Task<IEnumerable<StudentModel>> GetStudents(StudentSearchOptions searchOptions)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var students = await unitOfWork.Students.GetAll(searchOptions);

                return students.Select(s => new StudentModel(s)).ToList();
            }
        }

        public async Task<IEnumerable<StudentSummaryModel>> SearchStudents(StudentSearchOptions searchOptions)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var students = await unitOfWork.Students.SearchAll(searchOptions);

                return students.Select(s => new StudentSummaryModel(s)).ToList();
            }
        }

        public async Task CreateStudent(params CreateStudentRequestModel[] students)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var admissionNumbers = (await unitOfWork.Students.GetAdmissionNumbers()).ToArray();

                var nextAdmissionNumber = admissionNumbers.Any() ? admissionNumbers.Max() + 1 : 1;

                foreach (var request in students)
                {
                    var createDate = DateTime.Now;
                    var groupIds = new List<Guid>() {request.YearGroupId, request.RegGroupId};

                    if (request.HouseId.HasValue)
                    {
                        groupIds.Add(request.HouseId.Value);
                    }
                    
                    var student = new Student
                    {
                        AdmissionNumber = nextAdmissionNumber,
                        DateStarting = request.DateStarting,
                        SenStatusId = request.SenStatusId,
                        SenTypeId = request.SenTypeId,
                        EnrolmentStatusId = request.EnrolmentStatusId,
                        BoarderStatusId = request.BoarderStatusId,
                        PupilPremium = request.PupilPremium,
                        Person = PersonHelper.CreatePerson(request)
                    };

                    foreach (var groupId in groupIds)
                    {
                        student.StudentGroupMemberships.Add(new StudentGroupMembership
                        {
                            StudentGroupId = groupId,
                            StartDate = createDate
                        });
                    }
                    
                    unitOfWork.Students.Create(student);

                    nextAdmissionNumber++;
                }

                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateUpn()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var school = await unitOfWork.Schools.GetLocal();

                if (school == null)
                {
                    throw new NotFoundException("Local school not found.");
                }

                var academicYear = await unitOfWork.AcademicYears.GetCurrent();

                if (academicYear == null)
                {
                    throw new NotFoundException("No academic year is currently in session.");
                }

                var academicTerms =
                    (await unitOfWork.AcademicTerms.GetByAcademicYear(academicYear.Id)).OrderBy(t => t.StartDate)
                    .ToList();

                var firstTerm = academicTerms.FirstOrDefault();

                if (firstTerm == null)
                {
                    throw new NotFoundException("No academic terms were found in current academic year.");
                }

                var allocationYear =
                    int.Parse(firstTerm.StartDate.Year.ToString().Substring(3, 2));

                var upnSerials = (await unitOfWork.Students.GetUpns(school.LocalAuthority.LeaCode,
                    school.EstablishmentNumber,
                    allocationYear)).Select(u => int.Parse(u.Substring(10, 3))).ToList();

                var nextSerial = upnSerials.Any() ? upnSerials.Max() + 1 : 1;

                var baseUpn =
                    $"{school.LocalAuthority.LeaCode}{school.EstablishmentNumber}{allocationYear}{nextSerial:000}";

                var checkDigit = ValidationHelper.GetUpnCheckDigit(baseUpn);

                return $"{checkDigit}{baseUpn}";
            }
        }

        public async Task UpdateStudent(params UpdateStudentRequestModel[] models)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                foreach (var model in models)
                {
                    var student = await unitOfWork.Students.GetById(model.Id);

                    student.DateStarting = model.DateStarting;
                    student.DateLeaving = model.DateLeaving;
                    student.SenStatusId = model.SenStatusId;
                    student.SenTypeId = model.SenTypeId;
                    student.EnrolmentStatusId = model.EnrolmentStatusId;
                    student.BoarderStatusId = model.BoarderStatusId;
                    student.PupilPremium = model.PupilPremium;
                    student.Upn = model.Upn;
                    
                    PersonHelper.UpdatePerson(student.Person, model);

                    await unitOfWork.People.Update(student.Person);
                    await unitOfWork.Students.Update(student);
                }

                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}