using System;
using MyPortal.Database.Models.QueryResults.Attendance;

namespace MyPortal.Logic.Models.Summary;

public class AttendanceRegisterSummaryModel
{
    internal AttendanceRegisterSummaryModel(SessionDetailModel detailModel)
    {
        StudentGroupId = detailModel.StudentGroupId;
        AttendanceWeekId = detailModel.AttendanceWeekId;
        PeriodId = detailModel.PeriodId;
        PeriodName = detailModel.PeriodName;
        ClassCode = detailModel.ClassCode;
        TeacherName = detailModel.TeacherName;
        RoomName = detailModel.RoomName;
        StartTime = detailModel.StartTime;
        EndTime = detailModel.EndTime;
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