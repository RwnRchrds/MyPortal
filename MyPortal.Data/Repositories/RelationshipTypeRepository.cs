using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class RelationshipTypeRepository : ReadRepository<RelationshipType>, IRelationshipTypeRepository
    {
        public RelationshipTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}