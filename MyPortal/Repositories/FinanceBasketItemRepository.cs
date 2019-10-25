using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Repositories
{
    public class FinanceBasketItemRepository : Repository<FinanceBasketItem>, IFinanceBasketItemRepository
    {
        public FinanceBasketItemRepository(MyPortalDbContext context) : base(context)
        {

        }
    }
}