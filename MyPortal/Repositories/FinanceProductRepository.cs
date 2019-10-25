using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class FinanceProductRepository : Repository<FinanceProduct>, IFinanceProductRepository
    {
        public FinanceProductRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}