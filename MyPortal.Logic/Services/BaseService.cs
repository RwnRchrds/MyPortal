using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _mapper;

        public BaseService()
        {
            _mapper = MappingService.GetMapperBusinessConfiguration();
        }
    }
}
