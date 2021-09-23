using System.Data.Common;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class LanguageRepository : BaseReadRepository<Language>
    {
        public LanguageRepository(DbTransaction transaction) : base(transaction)
        {
        }
    }
}