using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class RelationshipTypeRepository : ReadOnlyRepository<RelationshipType>, IRelationshipTypeRepository
    {
        public RelationshipTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}