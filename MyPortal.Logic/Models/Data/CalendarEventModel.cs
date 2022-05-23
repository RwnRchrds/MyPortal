using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Data
{
    // Calendar event data requestModel for use with FullCalendar
    public class CalendarEventModel
    {
        public CalendarEventModel(DiaryEventModel eventModel)
        {
            if (eventModel.Id.HasValue)
            {
                Id = eventModel.Id.Value.ToString("N");
            }
            AllDay = eventModel.IsAllDay;
            Start = eventModel.StartTime;
            End = eventModel.EndTime;
            Title = eventModel.Subject;
            Display = CalendarDisplayModes.Auto;
            Color = eventModel.EventType.ColourCode;
        }

        public CalendarEventModel(SessionMetadata sessionMetadata, string colour)
        {
            AllDay = false;
            Start = sessionMetadata.StartTime;
            End = sessionMetadata.EndTime;
            Title = $"{sessionMetadata.ClassCode}";
            Display = CalendarDisplayModes.Auto;
            Color = colour;
            if (sessionMetadata.RoomId.HasValue && !string.IsNullOrWhiteSpace(sessionMetadata.RoomName))
            {
                ExtendedProps = new
                {
                    Room = sessionMetadata.RoomName,
                    Teacher = sessionMetadata.TeacherName
                };
            }
        }

        public string Id { get; set; }
        public string GroupId { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        #region Recurring Events
        
        public int[] DaysOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime StartRecur { get; set; }
        public DateTime EndRecur { get; set; }
        
        #endregion
        
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Interactive { get; set; }
        public string[] ClassNames { get; set; }
        public string[] ResourceIds { get; set; }
        public bool Editable { get; set; }
        public bool Overlap { get; set; }
        public string Display { get; set; }
        public string Color { get; set; }
        public object ExtendedProps { get; set; }
    }

    public static class CalendarDisplayModes
    {
        public const string Auto = @"auto";
        public const string Block = @"block";
        public const string ListItem = @"list-item";
        public const string Background = @"background";
        public const string InverseBackground = @"inverse-background";
        public const string None = @"none";
    }
}
