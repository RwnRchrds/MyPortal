using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ResultRepository : BaseReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        public async Task Update(Result entity)
        {
            var result = await Context.Results.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (result == null)
            {
                throw new EntityNotFoundException("Result not found.");
            }

            result.GradeId = entity.GradeId;
            result.Mark = entity.Mark;
            result.Comments = entity.Comments;
            result.ColourCode = entity.ColourCode;
        }
    }
}