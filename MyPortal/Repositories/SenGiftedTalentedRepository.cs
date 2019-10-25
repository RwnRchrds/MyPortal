using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class SenGiftedTalentedRepository : Repository<SenGiftedTalented>, ISenGiftedTalentedRepository
    {
        public SenGiftedTalentedRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}