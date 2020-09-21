using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface ISystemEntity : IEntity
    {
        public bool System { get; set; }
    }
}
