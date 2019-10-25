using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationEmailAddressRepository : Repository<CommunicationEmailAddress>, ICommunicationEmailAddressRepository
    {
        public CommunicationEmailAddressRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}