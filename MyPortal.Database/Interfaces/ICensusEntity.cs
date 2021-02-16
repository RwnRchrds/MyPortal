using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Interfaces
{
    public interface ICensusEntity : IEntity
    {
        public string Code { get; set; }
    }
}
