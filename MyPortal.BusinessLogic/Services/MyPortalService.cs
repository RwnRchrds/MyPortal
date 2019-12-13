using System;
using AutoMapper;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Persistence;

namespace MyPortal.BusinessLogic.Services
{
    public abstract class MyPortalService : IDisposable
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected IMapper Mapper;

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