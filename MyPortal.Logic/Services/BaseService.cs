﻿﻿using System;
using AutoMapper;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Exceptions;

 namespace MyPortal.Logic.Services
{
    public abstract class BaseService : IService
    {
        protected readonly IMapper BusinessMapper;
        protected readonly string ObjectName;
        protected Guid UserId;

        public BaseService(string objectName)
        {
            BusinessMapper = MappingHelper.GetBusinessConfig();
            ObjectName = objectName;
        }

        public abstract void Dispose();

        protected ServiceException NotFound(string message = null)
        {
            return new ServiceException(ExceptionType.NotFound, string.IsNullOrWhiteSpace(message) ? $"{ObjectName} not found." : message);
        }

        protected ServiceException BadRequest(string message = null)
        {
            return new ServiceException(ExceptionType.BadRequest, string.IsNullOrWhiteSpace(message) ? "An error occurred." : message);
        }

        protected ServiceException BadRequest(Exception ex)
        {
            var exceptionMessage = ExceptionHelper.GetRootExceptionMessage(ex);

            var exception = new ServiceException(ExceptionType.BadRequest, string.IsNullOrWhiteSpace(exceptionMessage) ? "An error occurred." : exceptionMessage);

            return exception;
        }

        protected ServiceException Forbidden(string message = null)
        {
            return new ServiceException(ExceptionType.Forbidden, string.IsNullOrWhiteSpace(message) ? "Access denied." : message);
        }
    }
}
