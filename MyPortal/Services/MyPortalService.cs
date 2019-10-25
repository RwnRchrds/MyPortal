using System;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Persistence;

namespace MyPortal.Services
{
    public class MyPortalService : IService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public MyPortalService(MyPortalDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}