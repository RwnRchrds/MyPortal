﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MyPortal.Logic.Helpers;
 using MyPortal.Logic.Interfaces;
 using MyPortal.Logic.Models.Exceptions;

 namespace MyPortal.Logic.Services
{
    public abstract class BaseService : IService
    {
        protected readonly IMapper _businessMapper;
        protected readonly string _objectName;

        public BaseService(string objectName)
        {
            _businessMapper = MappingHelper.GetBusinessConfig();
            _objectName = objectName;
        }

        public abstract void Dispose();

        public void NotFound(string message = null)
        {
            throw new ServiceException(ExceptionType.NotFound, message ?? $"{_objectName} not found.");
        }

        public void BadRequest(string message = null)
        {
            throw new ServiceException(ExceptionType.BadRequest, message ?? "An error occurred.");
        }

        public void Forbidden(string message = null)
        {
            throw new ServiceException(ExceptionType.Forbidden, message ?? "Access denied.");   
        }
    }
}
