CREATE VIEW SessionMetadata AS
SELECT
    S.Id as SessionId,
    API.AttendanceWeekId as AttendanceWeekId,
    API.PeriodId as PeriodId,
    CG.StudentGroupId as StudentGroupId,
    API.ActualStartTime as StartTime,
    API.ActualEndTime as EndTime,
    API.Name as PeriodName,
    C.Code as ClassCode,
    COALESCE(CA.TeacherId, S.TeacherId) as TeacherId,
    COALESCE(CNA.Name, FNA.Name) as TeacherName,
    COALESCE(CR.Id, R.Id) as RoomId,
    COALESCE(CR.Name, R.Name) as RoomName,
    CASE WHEN CA.Id IS NULL THEN 0 ELSE 1 END as IsCover
FROM dbo.Sessions S
         LEFT JOIN dbo.AttendancePeriods AP ON S.PeriodId = AP.Id
         LEFT JOIN dbo.AttendancePeriodInstances API ON AP.Id = API.PeriodId
         LEFT JOIN dbo.Classes C ON S.ClassId = C.Id
         LEFT JOIN dbo.CurriculumGroups CG ON C.CurriculumGroupId = CG.Id
         LEFT JOIN dbo.StaffMembers SM ON S.TeacherId = SM.Id
         LEFT JOIN Rooms R ON S.RoomId = R.Id
         LEFT JOIN dbo.CoverArrangements CA ON S.Id = CA.SessionId AND API.AttendanceWeekId = CA.WeekId
         LEFT JOIN Rooms CR ON CA.RoomId = CR.Id
         LEFT JOIN dbo.StaffMembers CSM ON CA.TeacherId = CSM.Id
         LEFT JOIN dbo.Courses CO ON C.CourseId = CO.Id
    OUTER APPLY GetName(SM.PersonId, 2, 0, 1) FNA
OUTER APPLY GetName(CSM.PersonId, 2, 0, 1) CNA
WHERE S.StartDate <= API.ActualEndTime AND S.EndDate >= API.ActualStartTime;