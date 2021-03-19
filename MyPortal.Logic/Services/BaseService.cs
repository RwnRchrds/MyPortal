using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;

 namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper BusinessMapper;

        public BaseService()
        {
            BusinessMapper = MappingHelper.GetConfig();
        }
    }
}
