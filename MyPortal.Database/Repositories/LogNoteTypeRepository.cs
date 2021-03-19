using System.Data.Common;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class LogNoteTypeRepository : BaseReadRepository<LogNoteType>, ILogNoteTypeRepository
    {
        public LogNoteTypeRepository(DbTransaction transaction) : base(transaction)
        {
        }
    }
}