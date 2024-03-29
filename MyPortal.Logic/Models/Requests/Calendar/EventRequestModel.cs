﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyPortal.Logic.Models.Requests.Calendar
{
    public class EventRequestModel
    {
        public Guid EventTypeId { get; set; }

        public Guid? RoomId { get; set; }

        [Required] [StringLength(256)] public string Subject { get; set; }

        [StringLength(256)] public string Description { get; set; }

        [StringLength(256)] public string Location { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsAllDay { get; set; }

        public bool IsPublic { get; set; }
    }
}