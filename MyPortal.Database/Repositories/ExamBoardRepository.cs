using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamBoardRepository : BaseReadWriteRepository<ExamBoard>, IExamBoardRepository
    {
        public ExamBoardRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(ExamBoard entity)
        {
            var examBoard = await DbUser.Context.ExamBoards.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (examBoard == null)
            {
                throw new EntityNotFoundException("Exam board not found.");
            }

            examBoard.Abbreviation = entity.Abbreviation;
            examBoard.FullName = entity.FullName;
            examBoard.Code = entity.Code;
            examBoard.Domestic = entity.Domestic;
            examBoard.UseEdi = entity.UseEdi;
            examBoard.Active = entity.Active;
        }
    }
}