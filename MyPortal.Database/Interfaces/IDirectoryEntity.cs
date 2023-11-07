using System;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces
{
    public interface IDirectoryEntity : IEntity
    {
        public Guid DirectoryId { get; set; }
        public Directory Directory { get; set; }
    }
}