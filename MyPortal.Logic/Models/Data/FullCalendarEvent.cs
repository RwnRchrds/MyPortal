using System;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Data
{
    public class FullCalendarEvent
    {
        public FullCalendarEvent(DiaryEventModel eventModel)
        {
            Id = eventModel.Id.ToString("N");
            AllDay = eventModel.IsAllDay;
            Start = eventModel.StartTime;
            End = eventModel.EndTime;
            Title = eventModel.Subject;
            Display = FullCalendarDisplayModes.Auto;
            Color = eventModel.EventType.ColourCode;
            Editable = false;
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
        public string Color { get; set; }
        public object ExtendedProps { get; set; }
    }

    public static class FullCalendarDisplayModes
    {
        public const string Auto = @"auto";
        public const string Block = @"block";
        public const string ListItem = @"list-item";
        public const string Background = @"background";
        public const string InverseBackground = @"inverse-background";
        public const string None = @"none";
    }
}
