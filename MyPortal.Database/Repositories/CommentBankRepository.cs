using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class CommentBankRepository : BaseReadWriteRepository<CommentBank>, ICommentBankRepository
    {
        public CommentBankRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        public async Task Update(CommentBank entity)
        {
            var commentBank = await Context.CommentBanks.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (commentBank == null)
            {
                throw new EntityNotFoundException("Comment bank not found.");
            }

            commentBank.Name = entity.Name;
            commentBank.Active = entity.Active;
        }
    }
}