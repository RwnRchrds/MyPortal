using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ResultSetRepository : BaseReadWriteRepository<ResultSet>, IResultSetRepository
    {
        public ResultSetRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(ResultSet entity)
        {
            var resultSet = await DbUser.Context.ResultSets.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (resultSet == null)
            {
                throw new EntityNotFoundException("Result set not found.");
            }

            resultSet.Name = entity.Name;
            resultSet.Active = entity.Active;
            resultSet.Description = entity.Description;
        }
    }
}