using System;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Persistence;

namespace MyPortal.Services
{
    public class MyPortalService : IService
    {
        protected readonly IUnitOfWork UnitOfWork;

        public MyPortalService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            //UnitOfWork.Dispose();
        }
    }
}