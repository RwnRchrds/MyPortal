﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Models.Database;

namespace MyPortal.Interfaces
{
    public interface IFinanceBasketItemRepository : IRepository<FinanceBasketItem>
    {
        Task<IEnumerable<FinanceBasketItem>> GetBasketItemsByStudent(int studentId);

        Task<decimal> GetBasketTotalForStudent(int studentId);
    }
}