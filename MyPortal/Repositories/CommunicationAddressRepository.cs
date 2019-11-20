using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class CommunicationAddressRepository : Repository<CommunicationAddress>, ICommunicationAddressRepository
    {
        public CommunicationAddressRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}