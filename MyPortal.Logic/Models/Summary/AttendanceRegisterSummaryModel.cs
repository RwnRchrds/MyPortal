using System;
using MyPortal.Database.Models.QueryResults.Curriculum;

namespace MyPortal.Logic.Models.Summary;

public class AttendanceRegisterSummaryModel
{
    internal AttendanceRegisterSummaryModel(SessionPeriodDetailModel periodDetailModel)
    {
        StudentGroupId = periodDetailModel.StudentGroupId;
        AttendanceWeekId = periodDetailModel.AttendanceWeekId;
        PeriodId = periodDetailModel.PeriodId;
        PeriodName = periodDetailModel.PeriodName;
        ClassCode = periodDetailModel.ClassCode;
        TeacherName = periodDetailModel.TeacherName;
        RoomName = periodDetailModel.RoomName;
        StartTime = periodDetailModel.StartTime;
        EndTime = periodDetailModel.EndTime;
    }

    public Guid StudentGroupId { get; set; }
    public Guid AttendanceWeekId { get; set; }
    public Guid PeriodId { get; set; }
    public string PeriodName { get; set; }
    public string ClassCode { get; set; }
    public string TeacherName { get; set; }
    public string RoomName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}