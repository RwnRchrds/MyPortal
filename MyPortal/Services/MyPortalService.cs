using System;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Persistence;

namespace MyPortal.Services
{
    public abstract class MyPortalService : IService
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected MyPortalService()
        {
            UnitOfWork = new UnitOfWork();
        }

        protected MyPortalService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}