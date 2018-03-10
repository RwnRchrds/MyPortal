using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Student, StudentDto>();
            Mapper.CreateMap<StudentDto, Student>();

            Mapper.CreateMap<LogDto, Log>();
            Mapper.CreateMap<Log, LogDto>();

            Mapper.CreateMap<YearGroupDto, YearGroup>();
            Mapper.CreateMap<YearGroup, YearGroupDto>();

            Mapper.CreateMap<RegGroupDto, RegGroup>();
            Mapper.CreateMap<RegGroup, RegGroupDto>();
        }
    }
}