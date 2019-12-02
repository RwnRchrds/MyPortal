using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class PastoralHouseRepository : ReadWriteRepository<PastoralHouse>, IPastoralHouseRepository
    {
        public PastoralHouseRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}