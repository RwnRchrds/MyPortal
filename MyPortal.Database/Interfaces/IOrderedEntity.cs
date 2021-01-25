using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface IOrderedEntity
    {
        public int Order { get; set; }
    }
}
