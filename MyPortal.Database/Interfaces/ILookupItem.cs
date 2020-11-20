using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface ILookupItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
