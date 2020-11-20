using System;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Models.Data
{
    public class BaseModel : IEntity
    {
        public Guid Id { get; set; }
    }
}
