﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
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

        public AttendanceMarkService(IAttendanceMarkRepository attendanceMarkRepository,
            IAttendanceCodeRepository attendanceCodeRepository, IStudentRepository studentRepository,
            IAttendanceWeekRepository attendanceWeekRepository, IAttendancePeriodRepository periodRepository)
        {
            _attendanceMarkRepository = attendanceMarkRepository;
            _attendanceCodeRepository = attendanceCodeRepository;
            _studentRepository = studentRepository;
            _attendanceWeekRepository = attendanceWeekRepository;
            _periodRepository = periodRepository;
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
                CodeId = Guid.Empty
            };
        }

        public async Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId)
        {
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
                if (model.CodeId == Guid.Empty)
                {
                    throw new AttendanceCodeException("Cannot insert blank attendance codes.");
                }

                var markInDb = await GetAttendanceMark(model.StudentId, model.WeekId, model.PeriodId);

                if (markInDb.Id != Guid.Empty)
                {
                    markInDb.CodeId = model.CodeId;
                    markInDb.MinutesLate = model.MinutesLate ?? 0;
                    markInDb.Comments = model.Comments;

                    var updatedMark = new AttendanceMark
                    {
                        Id = markInDb.Id,
                        CodeId = markInDb.CodeId,
                        StudentId = markInDb.StudentId,
                        WeekId = markInDb.WeekId,
                        PeriodId = markInDb.PeriodId,
                        MinutesLate = markInDb.MinutesLate,
                        Comments = markInDb.Comments
                    };

                    _attendanceMarkRepository.Update(updatedMark);
                }
                else
                {
                    var mark = new AttendanceMark
                    {
                        StudentId = model.StudentId,
                        WeekId = model.WeekId,
                        PeriodId = model.PeriodId,
                        CodeId = model.CodeId,
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

        public async Task<AttendanceSummary> GetSummaryByStudent(Guid studentId, Guid academicYearId)
        {
            var codes = (await _attendanceCodeRepository.GetAll()).Select(BusinessMapper.Map<AttendanceCodeModel>)
                .ToList();

            var marks =
                (await _attendanceMarkRepository.GetByStudent(studentId, academicYearId)).Select(BusinessMapper
                    .Map<AttendanceMarkModel>).ToList();

            var summary = new AttendanceSummary(codes, marks);

            return summary;
        }
    }
}
