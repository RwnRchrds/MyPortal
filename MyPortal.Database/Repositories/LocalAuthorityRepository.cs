using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class LocalAuthorityRepository : BaseReadRepository<LocalAuthority>, ILocalAuthorityRepository
    {
        public LocalAuthorityRepository(DbTransaction transaction) : base(transaction, "LA")
        {
        }

        public async Task<LocalAuthority> GetCurrent()
        {
            var query = GenerateQuery();

            query.LeftJoin("Schools as School", "School.LocalAuthorityId", "LA.Id");

            query.Where("School.Local", true);

            return (await ExecuteQuery(query)).First();
        }
    }
}