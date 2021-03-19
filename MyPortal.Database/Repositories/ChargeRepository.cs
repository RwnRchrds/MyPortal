using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;

namespace MyPortal.Database.Repositories
{
    public class ChargeRepository : BaseReadWriteRepository<Charge>, IChargeRepository
    {
        public ChargeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "C")
        {
        }
    }
}
