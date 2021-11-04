using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces
{
    public interface IStudentGroupEntity : IEntity
    {
        Guid StudentGroupId { get; set; }
        StudentGroup StudentGroup { get; set; }
    }
}