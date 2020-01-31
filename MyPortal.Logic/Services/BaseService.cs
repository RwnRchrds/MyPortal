using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper _businessMapper;
        protected readonly IMapper _dataGridMapper;

        public BaseService()
        {
            _businessMapper = MappingHelper.GetMapperBusinessConfiguration();
            _dataGridMapper = MappingHelper.GetMapperDataGridConfiguration();
        }
    }
}
