using MyPortal.Database.Enums;
using MyPortal.Database.Helpers;
using SqlKata;

namespace MyPortal.Database.Constants;

public class CommonTableExpressions
{
    internal static Query WithPossibleAttendancePeriods(Query query, string alias)
    {
        return query.With(alias, GetPossibleAttendancePeriodsCte());
    }

    internal static Query WithSessionsMetadata(Query query, string papCteAlias, string alias, bool includeRegPeriods)
    {
        var cteQuery = GetSessionMetadataCte(papCteAlias);

        if (includeRegPeriods)
        {
            cteQuery.Union(GetRegSessionMetadataCte(papCteAlias));
        }
        
        return query.With(alias, cteQuery);
    }

    private static Query GetPossibleAttendancePeriodsCte()
    {
        var query = new Query("AttendancePeriods as AP");
            
        query.SelectRaw(
            "CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning), 112) + ' ' + CONVERT(CHAR(8), AP.StartTime, 108)) AS ActualStartTime");

        query.SelectRaw(
            "CONVERT(DATETIME, CONVERT(CHAR(8), DATEADD(DAY, CASE WHEN AP.Weekday = 0 THEN 6 ELSE AP.Weekday - 1 END, AW.Beginning), 112) + ' ' + CONVERT(CHAR(8), AP.EndTime, 108)) AS ActualEndTime");

        query.Select("AP.Id as PeriodId", "AW.Id as AttendanceWeekId", "AP.WeekPatternId as WeekPatternId",
            "AP.Weekday as Weekday", "AP.Name as Name", "AP.StartTime as StartTime", "AP.EndTime as EndTime",
            "AP.AmReg as AmReg", "AP.PmReg as PmReg");

        query.LeftJoin("AttendanceWeekPatterns as AWP", "AWP.Id", "AP.WeekPatternId");

        query.LeftJoin("AttendanceWeeks AS AW", "AW.WeekPatternId", "AWP.Id");

        query.Where("AW.IsNonTimeTable", false);

        return query;
    }

    private static Query GetRegSessionMetadataCte(string papCteAlias)
    {
        var cteQuery = new Query("RegGroups as RG").Distinct();

        cteQuery.SelectRaw("NULL as SessionId");

        cteQuery.Select("PAP.AttendanceWeekId as AttendanceWeekId", "PAP.PeriodId as PeriodId", "SG.Id as StudentGroupId", "PAP.ActualStartTime as StartTime",
            "PAP.ActualEndTime as EndTime", "PAP.Name as PeriodName", "SG.Code as ClassCode", "SM.Id as TeacherId", "FNA.Name as TeacherName",
            "R.Id as RoomId", "R.Name as RoomName");
        
        cteQuery.SelectRaw("0 as IsCover");

        cteQuery.CrossJoin("AttendancePeriods as AP");
        cteQuery.Join($"{papCteAlias} as PAP", "PAP.PeriodId", "AP.Id");
        cteQuery.LeftJoin("StudentGroups as SG", "RG.StudentGroupId", "SG.Id");
        cteQuery.LeftJoin("StudentGroupSupervisors as SGS", "SGS.Id", "SG.MainSupervisorId");
        cteQuery.LeftJoin("StaffMembers as SM", "SM.Id", "SGS.SupervisorId");
        cteQuery.LeftJoin("Rooms as R", "R.Id", "RG.RoomId");
        cteQuery.ApplyOverlappingEvents("DE", "PAP.ActualStartTime", "PAP.ActualEndTime", EventTypes.SchoolHoliday);
        cteQuery.ApplyName("FNA", "SM.PersonId", NameFormat.FullNameAbbreviated);
        cteQuery.Where(x => x.Where($"AP.AmReg", true).OrWhere("AP.PmReg", true));

        return cteQuery.WhereNull("DE.Id");
    }

    private static Query GetSessionMetadataCte(string papCteAlias)
    {
        var cteQuery = new Query("Sessions as S").Distinct();

        cteQuery.Select("S.Id as SessionId", "PAP.AttendanceWeekId as AttendanceWeekId", "PAP.PeriodId as PeriodId", "CG.StudentGroupId as StudentGroupId",
            "PAP.ActualStartTime as StartTime", "PAP.ActualEndTime as EndTime", "PAP.Name as PeriodName",
            "C.Code as ClassCode");

        cteQuery.SelectRaw("COALESCE(CA.TeacherId, S.TeacherId) as TeacherId");
        cteQuery.SelectRaw("COALESCE(CNA.Name, FNA.Name) as TeacherName");
        cteQuery.SelectRaw("COALESCE(CR.Id, R.Id) as RoomId");
        cteQuery.SelectRaw("COALESCE(CR.Name, R.Name) as RoomName");
        cteQuery.SelectRaw("CASE WHEN CA.Id IS NULL THEN 0 ELSE 1 END as IsCover");

        cteQuery.LeftJoin("AttendancePeriods as AP", "AP.Id", "S.PeriodId");
        cteQuery.LeftJoin($"{papCteAlias} as PAP", "PAP.PeriodId", "AP.Id");
        cteQuery.LeftJoin("Classes as C", "C.Id", "S.ClassId");
        cteQuery.LeftJoin("CurriculumGroups as CG", "CG.Id", "C.CurriculumGroupId");
        cteQuery.LeftJoin("StaffMembers as SM", "SM.Id", "S.TeacherId");
        cteQuery.LeftJoin("Rooms as R", "R.Id", "S.RoomId");
        cteQuery.LeftJoin("CoverArrangements as CA",
            ca => ca.On("CA.SessionId", "S.Id").On("CA.WeekId", "PAP.AttendanceWeekId"));
        cteQuery.LeftJoin("Rooms as CR", "CR.Id", "CA.RoomId");
        cteQuery.LeftJoin("StaffMembers as CSM", "CSM.Id", "CA.TeacherId");
        cteQuery.LeftJoin("Courses as CO", "CO.Id", "C.CourseId");
        cteQuery.ApplyOverlappingEvents("DE", "PAP.ActualStartTime", "PAP.ActualEndTime", EventTypes.SchoolHoliday);
        cteQuery.ApplyName("FNA", "SM.PersonId", NameFormat.FullNameAbbreviated);
        cteQuery.ApplyName("CNA", "CSM.PersonId", NameFormat.FullNameAbbreviated);

        return cteQuery.WhereNull("DE.Id").WhereRaw("S.StartDate <= PAP.ActualEndTime")
            .WhereRaw("S.EndDate >= PAP.ActualStartTime");
    }
}