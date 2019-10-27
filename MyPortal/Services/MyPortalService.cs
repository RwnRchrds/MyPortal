using System;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Persistence;

namespace MyPortal.Services
{
    public class MyPortalService : IService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public MyPortalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}