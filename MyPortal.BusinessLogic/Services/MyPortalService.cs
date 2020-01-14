using System;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Persistence;

namespace MyPortal.BusinessLogic.Services
{
    public abstract class MyPortalService : IDisposable
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly Mapper Mapper;

        protected MyPortalService()
        {
            UnitOfWork = new UnitOfWork();
            Mapper = MappingService.GetMapperBusinessConfiguration();
        }

        protected MyPortalService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task SaveChanges()
        {
            await UnitOfWork.Complete();
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}