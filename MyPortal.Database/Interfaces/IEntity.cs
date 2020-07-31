using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
