using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{ 
    public interface IPeriodService : IService
    {
        Task<PeriodModel> GetById(Guid periodId);
    }
}
