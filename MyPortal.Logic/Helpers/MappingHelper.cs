using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Details;
using MyPortal.Logic.Models.Lite;
using MyPortal.Logic.Models.Student;

namespace MyPortal.Logic.Helpers
{
    public class MappingHelper
    {
        public static IMapper GetBusinessConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<House, HouseDetails>().ReverseMap();
                cfg.CreateMap<Person, PersonDetails>().ReverseMap();
                cfg.CreateMap<RegGroup, RegGroupDetails>().ReverseMap();
                cfg.CreateMap<SenStatus, SenStatusDetails>().ReverseMap();
                cfg.CreateMap<StaffMember, StaffMemberDetails>().ReverseMap();
                cfg.CreateMap<Student, StudentDetails>().ReverseMap();
                cfg.CreateMap<YearGroup, YearGroupDetails>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static IMapper GetDataGridConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                
            });

            return new Mapper(config);
        }
    }
}
