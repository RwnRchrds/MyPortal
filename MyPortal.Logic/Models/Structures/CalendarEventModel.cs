using System;
using MyPortal.Database.Models.QueryResults.Attendance;
using MyPortal.Database.Models.QueryResults.Curriculum;
using MyPortal.Logic.Models.Data.Calendar;
using MyPortal.Logic.Models.Data.Curriculum;

namespace MyPortal.Logic.Models.Structures
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
            AllDay = eventModel.AllDay;
            Start = eventModel.StartTime;
            End = eventModel.EndTime;
            Title = eventModel.Subject;
            Display = CalendarDisplayModes.Auto;
            Color = eventModel.EventType.ColourCode;
        }

        public CalendarEventModel(SessionDataModel sessionPeriodDetailModel, string colour)
        {
            AllDay = false;
            Start = sessionPeriodDetailModel.StartTime;
            End = sessionPeriodDetailModel.EndTime;
            Title = $"{sessionPeriodDetailModel.ClassCode}";
            Display = CalendarDisplayModes.Auto;
            Color = colour;
            TextColor = "#FFFFFF";
            if (sessionPeriodDetailModel.RoomId.HasValue && !string.IsNullOrWhiteSpace(sessionPeriodDetailModel.RoomName))
            {
                ExtendedProps = new
                {
                    Room = sessionPeriodDetailModel.RoomName,
                    Teacher = sessionPeriodDetailModel.TeacherName
                };
            }
        }

        public string Id { get; set; }
        
        public string GroupId { get; set; }
        public bool AllDay { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        
        public string Url { get; set; }

        public string[] ClassNames { get; set; }
        
        public string[] ResourceIds { get; set; }
        
        public bool Editable { get; set; }
        
        public bool Overlap { get; set; }
        public string Display { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
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
