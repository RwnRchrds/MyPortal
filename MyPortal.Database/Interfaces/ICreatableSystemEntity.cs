using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces;

public interface ICreatableSystemEntity : ISystemEntity
{
    DateTime CreatedDate { get; set; }
    Guid? CreatedById { get; set; }
    User CreatedBy { get; set; }
}