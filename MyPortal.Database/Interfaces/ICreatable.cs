using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces
{
    public interface ICreatable
    {
        DateTime CreatedDate { get; set; }
        Guid CreatedById { get; set; }
        User CreatedBy { get; set; }
    }
}