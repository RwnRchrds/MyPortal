using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;

        public BaseService()
        {
            _mapper = MappingHelper.GetMapperBusinessConfiguration();
        }
    }
}
