using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class ContactRepository : ReadWriteRepository<Contact>, IContactRepository
    {
        public ContactRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}