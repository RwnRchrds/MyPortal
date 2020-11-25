using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class DiaryEventRepository : BaseReadWriteRepository<DiaryEvent>, IDiaryEventRepository
    {
        public DiaryEventRepository(ApplicationDbContext context) : base(context, "DiaryEvent")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(DiaryEventType), "DiaryEventType");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("DiaryEventTypes as DiaryEventType", "DiaryEventType.Id", "DiaryEvent.EventTypeId");
        }

        protected override async Task<IEnumerable<DiaryEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<DiaryEvent, DiaryEventType, DiaryEvent>(sql.Sql, (diaryEvent, type) =>
            {
                diaryEvent.EventType = type;
                return diaryEvent;
            }, sql.NamedBindings);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByDateRange(DateTime firstDate, DateTime lastDate, bool includePrivateEvents = false)
        {
            var query = GenerateQuery();

            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            if (!includePrivateEvents)
            {
                query.WhereTrue("DiaryEvent.IsPublic");
            }

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<DiaryEvent>> GetByPerson(DateTime firstDate, DateTime lastDate, Guid personId, bool includeDeclined = false)
        {
            var query = GenerateQuery();

            query.LeftJoin("DiaryEventAttendee as A", "A.EventId", "E.Id");
            
            query.WhereDate("DiaryEvent.StartTime", ">=", firstDate.Date);
            query.WhereDate("DiaryEvent.EndTime", "<", lastDate.Date.AddDays(1));

            query.Where("A.PersonId", personId);

            if (!includeDeclined)
            {
                query.Where(q => q.Where("A.ResponseId", "<>", AttendeeResponses.Declined).WhereFalse("A.Required"));
            }

            return await ExecuteQuery(query);
        }


        public async Task<IEnumerable<DiaryEvent>> GetLessonsByStudent(Guid studentId, DateTime firstDate,
            DateTime lastDate)
        {
            var query = new Query("Session as S");

            query.SelectRaw(@"SELECT S.[Id] AS Id
      ,@LessonEventType AS EventTypeId
      ,S.RoomId AS RoomId
      ,C.Name AS Subject
      ,null AS Description
	  ,null AS Location
	  ,CAST(CONCAT(DATEADD(DAY, (P.Weekday - 1), W.Beginning), ' ', (P.StartTime)) AS DATETIME) AS StartTime
	  ,CAST(CONCAT(DATEADD(DAY, (P.Weekday - 1), W.Beginning), ' ', (P.EndTime)) AS DATETIME) AS EndTime
	  ,0 AS IsAllDay
	  ,0 AS IsBlock
	  ,0 AS IsPublic
	  ,0 AS IsStudentVisible");
            
            query.Define("LessonEventType", EventTypes.Lesson);

            query.LeftJoin("AttendancePeriod as P", "P.Id", "S.PeriodId");
            query.LeftJoin("AttendanceWeekPattern as WP", "WP.Id", "P.WeekPatternId");
            query.LeftJoin("AttendanceWeek as W", "W.WeekPatternId", "WP.Id");
            query.LeftJoin("Class as C", "C.Id", "S.ClassId");
            query.LeftJoin("CurriculumGroup as G", "G.Id", "C.GroupId");
            query.LeftJoin("CurriculumGroupMembership as M", "M.GroupId", "G.Id");

            query.WhereFalse("W.IsNonTimetable");
            query.Where("M.StudentId", studentId);
            query.WhereDate("StartTime", ">=", firstDate);
            query.WhereDate("EndTime", "<=", lastDate);

            return await ExecuteQuery(query);
        }

        public async Task<IEnumerable<DiaryEvent>> GetLessonsByTeacher(Guid staffMemberId, DateTime firstDate,
            DateTime lastDate)
        {
            var query = new Query("Session as S");
            
            query.SelectRaw(@"SELECT S.[Id] AS Id
      ,@LessonEventType AS EventTypeId
      ,S.RoomId AS RoomId
      ,C.Name AS Subject
      ,null AS Description
	  ,null AS Location
	  ,CAST(CONCAT(DATEADD(DAY, (P.Weekday - 1), W.Beginning), ' ', (P.StartTime)) AS DATETIME) AS StartTime
	  ,CAST(CONCAT(DATEADD(DAY, (P.Weekday - 1), W.Beginning), ' ', (P.EndTime)) AS DATETIME) AS EndTime
	  ,0 AS IsAllDay
	  ,0 AS IsBlock
	  ,0 AS IsPublic
	  ,0 AS IsStudentVisible");

            query.Define("LessonEventType", EventTypes.Lesson);

            query.LeftJoin("AttendancePeriod as P", "P.Id", "S.PeriodId");
            query.LeftJoin("AttendanceWeekPattern as WP", "WP.Id", "P.WeekPatternId");
            query.LeftJoin("AttendanceWeek as W", "W.WeekPatternId", "WP.Id");
            query.LeftJoin("Class as C", "C.Id", "S.ClassId");
            
            query.WhereFalse("W.IsNonTimetable");
            query.Where("S.TeacherId", staffMemberId);
            query.WhereDate("StartTime", ">=", firstDate);
            query.WhereDate("EndTime", "<=", lastDate);

            return await ExecuteQuery(query);
        }
    }
}
