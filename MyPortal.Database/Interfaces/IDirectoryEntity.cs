using System;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IDirectoryEntity : IEntity
    {
        Guid DirectoryId { get; set; }

        Directory Directory { get; set; }
    }
}
