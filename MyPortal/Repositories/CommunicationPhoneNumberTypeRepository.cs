using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationPhoneNumberTypeRepository : ReadRepository<CommunicationPhoneNumberType>, ICommunicationPhoneNumberTypeRepository
    {
        public CommunicationPhoneNumberTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}