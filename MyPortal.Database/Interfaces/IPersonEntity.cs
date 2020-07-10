using System;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IPersonEntity : IEntity
    {
        public Guid PersonId { get; set; }

        public Person Person { get; set; }
    }
}