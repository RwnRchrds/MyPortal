using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PersonDocumentRepository : Repository<PersonDocument>, IPersonDocumentRepository
    {
        public PersonDocumentRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}