using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DataGrid;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Attendance;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class AttendanceMarkService : BaseService, IAttendanceMarkService
    {
        private readonly IAttendanceMarkRepository _attendanceMarkRepository;
        private readonly IAttendanceCodeRepository _attendanceCodeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAttendanceWeekRepository _attendanceWeekRepository;
        private readonly IAttendancePeriodRepository _periodRepository;

        public AttendanceMarkService(ApplicationDbContext context)
        {
            var connection = context.Database.GetDbConnection();

            _attendanceMarkRepository = new AttendanceMarkRepository(context);
            _attendanceCodeRepository = new AttendanceCodeRepository(connection);
            _studentRepository = new StudentRepository(context);
            _attendanceWeekRepository = new AttendanceWeekRepository(context);
            _periodRepository = new AttendancePeriodRepository(context);
        }

        public override void Dispose()
        {
            _attendanceMarkRepository.Dispose();
            _attendanceCodeRepository.Dispose();
            _studentRepository.Dispose();
            _attendanceWeekRepository.Dispose();
            _periodRepository.Dispose();
        }

        private AttendanceMarkModel NoMark(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            return new AttendanceMarkModel
            {
                Id = Guid.Empty,
                StudentId = studentId,
                WeekId = attendanceWeekId,
                PeriodId = periodId,
                MinutesLate = 0,
                Mark = "-"
            };
        }

        public async Task<AttendanceMarkModel> Get(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
            var student = await _studentRepository.GetById(studentId);
            var attendanceWeek = await _attendanceWeekRepository.GetById(attendanceWeekId);
            var period = await _periodRepository.GetById(periodId);

            var attendanceMark = await _attendanceMarkRepository.Get(studentId, attendanceWeekId, periodId);

            if (attendanceMark == null)
            {
                return NoMark(studentId, attendanceWeekId, periodId);
            }

            return BusinessMapper.Map<AttendanceMarkModel>(attendanceMark);
        }

        public async Task Save(params AttendanceMarkListModel[] marks)
        {
            foreach (var model in marks)
            {
                var markInDb = await Get(model.StudentId, model.WeekId, model.PeriodId);

                if (markInDb.Id != Guid.Empty)
                {
                    markInDb.Mark = model.Mark;
                    markInDb.MinutesLate = model.MinutesLate ?? 0;
                    markInDb.Comments = model.Comments;

                    var updatedMark = new AttendanceMark
                    {
                        Id = markInDb.Id,
                        Mark = markInDb.Mark,
                        StudentId = markInDb.StudentId,
                        WeekId = markInDb.WeekId,
                        PeriodId = markInDb.PeriodId,
                        MinutesLate = markInDb.MinutesLate,
                        Comments = markInDb.Comments
                    };

                    await _attendanceMarkRepository.Update(updatedMark);
                }
                else
                {
                    var mark = new AttendanceMark
                    {
                        StudentId = model.StudentId,
                        WeekId = model.WeekId,
                        PeriodId = model.PeriodId,
                        Mark = model.Mark,
                        MinutesLate = model.MinutesLate ?? 0,
                        Comments = model.Comments
                    };

                    _attendanceMarkRepository.Create(mark);
                }
            }

            await _attendanceMarkRepository.SaveChanges();
        }

        public async Task Delete(params Guid[] attendanceMarkIds)
        {
            foreach (var attendanceMarkId in attendanceMarkIds)
            {
                await _attendanceMarkRepository.Delete(attendanceMarkId);
            }

            await _attendanceMarkRepository.SaveChanges();
        }

        public async Task Save(params StudentRegisterMarkCollection[] markCollections)
        {
            var attendanceMarks = new List<AttendanceMarkListModel>();

            foreach (var collection in markCollections)
            {
                attendanceMarks.AddRange(collection.Marks);
            }

            await Save(attendanceMarks.ToArray());
        }

        public async Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId, bool asPercentage)
        {
            var codes = (await _attendanceCodeRepository.GetAll()).Select(BusinessMapper.Map<AttendanceCodeModel>)
                .ToList();

            var marks =
                (await _attendanceMarkRepository.GetByStudent(studentId, academicYearId)).Select(BusinessMapper
                    .Map<AttendanceMarkModel>).ToList();

            var summary = new AttendanceSummary(codes, marks);

            if (asPercentage)
            {
                summary.ConvertToPercentage();
            }

            return summary;
        }
    }
}
