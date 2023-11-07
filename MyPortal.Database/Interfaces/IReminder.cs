using System;

namespace MyPortal.Database.Interfaces;

public interface IReminder
{
    Guid UserId { get; set; }

    DateTime RemindTime { get; set; }
}