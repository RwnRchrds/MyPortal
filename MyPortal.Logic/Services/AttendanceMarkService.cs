using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
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

        public async Task<AttendanceMarkModel> GetAttendanceMark(Guid studentId, Guid attendanceWeekId, Guid periodId, bool returnNoMark = false)
        {
            var attendanceMark = await UnitOfWork.AttendanceMarks.GetMark(studentId, attendanceWeekId, periodId);

            if (returnNoMark && attendanceMark == null)
            {
                return NoMark(studentId, attendanceWeekId, periodId);
            }

            return BusinessMapper.Map<AttendanceMarkModel>(attendanceMark);
        }

        public async Task<IEnumerable<AttendanceRegisterStudentModel>> GetAttendanceMarksBySession(Guid attendanceWeekId, Guid sessionId)
        {
            throw new NotImplementedException();
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

                if (markInDb != null)
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

                    UnitOfWork.AttendanceMarks.Update(updatedMark);
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

                    UnitOfWork.AttendanceMarks.Create(mark);
                }
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Delete(params Guid[] attendanceMarkIds)
        {
            foreach (var attendanceMarkId in attendanceMarkIds)
            {
                await UnitOfWork.AttendanceMarks.Delete(attendanceMarkId);
            }

            await UnitOfWork.SaveChanges();
        }

        public async Task Save(params AttendanceRegisterStudentModel[] markCollections)
        {
            var attendanceMarks = new List<AttendanceMarkListModel>();

            foreach (var collection in markCollections)
            {
                attendanceMarks.AddRange(collection.Marks);
            }

            await Save(attendanceMarks.ToArray());
        }

        public async Task<AttendanceSummary> GetAttendanceSummaryByStudent(Guid studentId, Guid academicYearId)
        {
            var codes = (await UnitOfWork.AttendanceCodes.GetAll()).Select(BusinessMapper.Map<AttendanceCodeModel>)
                .ToList();

            var marks =
                (await UnitOfWork.AttendanceMarks.GetByStudent(studentId, academicYearId)).Select(BusinessMapper
                    .Map<AttendanceMarkModel>).ToList();

            var summary = new AttendanceSummary(codes, marks);

            return summary;
        }

        public AttendanceMarkService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
