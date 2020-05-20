﻿using System;
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

        public ServiceException NotFound(string message = null)
        {
            return new ServiceException(ExceptionType.NotFound, string.IsNullOrWhiteSpace(message) ? $"{_objectName} not found." : message);
        }

        public ServiceException BadRequest(string message = null)
        {
            return new ServiceException(ExceptionType.BadRequest, string.IsNullOrWhiteSpace(message) ? "An error occurred." : message);
        }

        public ServiceException BadRequest(Exception ex)
        {
            var exceptionMessage = ExceptionHelper.GetRootExceptionMessage(ex);

            var exception = new ServiceException(ExceptionType.BadRequest, string.IsNullOrWhiteSpace(exceptionMessage) ? "An error occurred." : exceptionMessage);

            return exception;
        }

        public ServiceException Forbidden(string message = null)
        {
            return new ServiceException(ExceptionType.Forbidden, string.IsNullOrWhiteSpace(message) ? "Access denied." : message);
        }
    }
}
