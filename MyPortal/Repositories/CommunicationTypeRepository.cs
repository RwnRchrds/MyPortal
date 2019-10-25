using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationTypeRepository : ReadOnlyRepository<CommunicationType>, ICommunicationTypeRepository
    {
        public CommunicationTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}