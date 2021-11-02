using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Models.Data
{
    public abstract class BaseModel
    {
        protected BaseModel(IEntity model)
        {
            Id = model.Id;
        }

        protected BaseModel()
        {
            
        }

        public Guid? Id { get; set; }
    }
}
