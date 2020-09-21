using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IDirectoryEntity : IEntity
    {
        public Guid DirectoryId { get; set; }
        public Directory Directory { get; set; }
    }
}
