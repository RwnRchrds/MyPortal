﻿using System;
using AutoMapper;
 using MyPortal.Logic.Exceptions;
 using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
 using MyPortal.Logic.Interfaces.Services;

 namespace MyPortal.Logic.Services
{
    public abstract class BaseService : IService
    {
        protected readonly IMapper BusinessMapper;

        public BaseService()
        {
            BusinessMapper = MappingHelper.GetConfig();
        }

        public abstract void Dispose();
    }
}
