﻿using System;
using AutoMapper;
 using MyPortal.Database.Interfaces;
 using MyPortal.Logic.Exceptions;
 using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
 using MyPortal.Logic.Interfaces.Services;

 namespace MyPortal.Logic.Services
{
    public abstract class BaseService : IService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper BusinessMapper;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            BusinessMapper = MappingHelper.GetConfig();
        }

        public virtual void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
