using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;

namespace MyPortal.Logic.Models.Data
{
    public abstract class BaseModel
    {
        public BaseModel(IEntity model)
        {
            Id = model.Id;
        }

        /*public BaseModel()
        {
            
        }*/
        
        public Guid Id { get; set; }
    }
}
