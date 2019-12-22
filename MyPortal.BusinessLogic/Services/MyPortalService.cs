using System;
using AutoMapper;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Persistence;

namespace MyPortal.BusinessLogic.Services
{
    public abstract class MyPortalService : IDisposable
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly MappingService Mapping;

        protected MyPortalService()
        {
            UnitOfWork = new UnitOfWork();
            Mapping = new MappingService(MapperType.BusinessObjects);
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