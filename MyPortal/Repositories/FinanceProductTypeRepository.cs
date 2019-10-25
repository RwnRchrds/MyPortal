using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class FinanceProductTypeRepository : Repository<FinanceProductType>, IFinanceProductTypeRepository
    {
        public FinanceProductTypeRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}