using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces;

public interface ITaskEntity
{
    Guid TaskId { get; set; }
    Task Task { get; set; }
}