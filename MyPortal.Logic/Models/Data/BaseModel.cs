using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Models.Data
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
