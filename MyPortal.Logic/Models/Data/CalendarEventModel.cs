using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Query.Attendance;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Data
{
    // Calendar event data model for use with FullCalendar
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
            BackgroundColor = eventModel.EventType.ColourCode;
            Editable = false;
        }

        public CalendarEventModel(SessionMetadata sessionMetadata, string colour)
        {
            AllDay = false;
            Start = sessionMetadata.StartTime;
            End = sessionMetadata.EndTime;
            Title = $"{sessionMetadata.ClassCode}";
            Display = CalendarDisplayModes.Auto;
            BackgroundColor = colour;
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
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string[] ClassNames { get; set; }
        public bool Editable { get; set; }
        public string Display { get; set; }
        public string BackgroundColor { get; set; }
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
