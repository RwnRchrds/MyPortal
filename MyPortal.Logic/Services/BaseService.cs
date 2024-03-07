using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Audit;
using MyPortal.Logic.Models.Data.Settings;

namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly ISessionUser User;

        protected BaseService(ISessionUser user)
        {
            User = user;
        }

        protected UnauthorisedException Unauthenticated()
        {
            return new UnauthorisedException("The user is not authenticated.");
        }

        protected void Validate<T>(T model)
        {
            ValidationHelper.ValidateModel(model);
        }
    }
}