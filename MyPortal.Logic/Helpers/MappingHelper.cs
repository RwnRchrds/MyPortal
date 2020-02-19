using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Dtos;
using MyPortal.Logic.Models.Dtos.DataGrid;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Helpers
{
    public class MappingHelper
    {
        public static Mapper GetMapperBusinessConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
            });

            return new Mapper(config);
        }

        public static Mapper GetMapperDataGridConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
            });

            return new Mapper(config);
        }
    }
}
