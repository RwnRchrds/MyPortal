using System;
using MyPortal.Database.Models.QueryResults.Attendance;

namespace MyPortal.Logic.Models.Summary;

public class AttendanceRegisterSummaryModel
{
    internal AttendanceRegisterSummaryModel(SessionMetadata metadata)
    {
        StudentGroupId = metadata.StudentGroupId;
        AttendanceWeekId = metadata.AttendanceWeekId;
        PeriodId = metadata.PeriodId;
        PeriodName = metadata.PeriodName;
        ClassCode = metadata.ClassCode;
        TeacherName = metadata.TeacherName;
        RoomName = metadata.RoomName;
        StartTime = metadata.StartTime;
        EndTime = metadata.EndTime;
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