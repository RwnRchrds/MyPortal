using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationPhoneNumberRepository : Repository<CommunicationPhoneNumber>, ICommunicationPhoneNumberRepository
    {
        public CommunicationPhoneNumberRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}