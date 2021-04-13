using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IUpdateRepository<TEntity> where TEntity : class, IEntity
    {
        Task Update(TEntity entity);
    }
}
