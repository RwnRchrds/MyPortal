CREATE VIEW [dbo].[Sessions_Metadata]
AS
SELECT DISTINCT 
            S.Id AS SessionId,
            S.StartDate, 
            S.EndDate,
            PAP.AttendanceWeekId,
            PAP.PeriodId,
            C.Id AS ClassId,
            C.CurriculumGroupId AS CurriculumGroupId,
            PAP.StartTime,
            PAP.EndTime,
            P.Name AS PeriodName,
            C.Code AS ClassCode,
            CO.Description AS CourseDescription,
            CASE WHEN CA.Id IS NOT NULL THEN CSM.Id ELSE SM.Id END AS TeacherId,
            CASE WHEN CA.Id IS NOT NULL THEN CNA.Name ELSE FNA.Name END AS TeacherName,
            CASE WHEN CA.Id IS NOT NULL
            THEN CR.Id ELSE R.Id END AS RoomId,
            CASE WHEN CA.Id IS NOT NULL THEN CR.Name ELSE R.Name END AS RoomName,
            CASE WHEN CA.Id IS NOT NULL THEN 1 ELSE 0 END AS IsCover
FROM        dbo.Sessions AS S 
            LEFT OUTER JOIN dbo.AttendancePeriods AS P ON P.Id = S.PeriodId
            LEFT OUTER JOIN dbo.AttendancePeriods_PossibleAttendancePeriods AS PAP ON PAP.PeriodId = P.Id
            LEFT OUTER JOIN dbo.Classes AS C ON C.Id = S.ClassId
            LEFT OUTER JOIN dbo.StaffMembers AS SM ON SM.Id = S.TeacherId
            LEFT OUTER JOIN dbo.Rooms AS R ON R.Id = S.RoomId           
            LEFT OUTER JOIN dbo.CoverArrangements AS CA ON CA.SessionId = S.Id AND CA.WeekId = PAP.AttendanceWeekId
            LEFT OUTER JOIN dbo.Rooms AS CR ON CR.Id = CA.RoomId
            LEFT OUTER JOIN dbo.StaffMembers AS CSM ON CSM.Id = CA.TeacherId                        
            LEFT OUTER JOIN dbo.Courses AS CO ON CO.Id = C.CourseId
            CROSS APPLY dbo.GetOverlappingEvents(PAP.StartTime, PAP.EndTime, '84E9DDA4-1BCB-4A2F-8082-FCE51DD04F28') AS DE
            CROSS APPLY dbo.GetName_FullNameAbbreviated(SM.PersonId, 0, 1) AS FNA
            CROSS APPLY dbo.GetName_FullNameAbbreviated(CSM.PersonId, 0, 1) AS CNA
WHERE       DE.Id IS NULL AND S.StartDate <= PAP.EndTime AND S.EndDate >= PAP.StartTime