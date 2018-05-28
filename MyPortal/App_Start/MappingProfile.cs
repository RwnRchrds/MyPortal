using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos;
using MyPortal.Dtos.Identity;
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

            Mapper.CreateMap<StaffDto, Staff>();
            Mapper.CreateMap<Staff, StaffDto>();

            Mapper.CreateMap<TrainingCertificateDto, TrainingCertificate>();
            Mapper.CreateMap<TrainingCertificate, TrainingCertificateDto>();

            Mapper.CreateMap<TrainingCourseDto, TrainingCourse>();
            Mapper.CreateMap<TrainingCourse, TrainingCourseDto>();

            Mapper.CreateMap<UserDto, IdentityUser>();
            Mapper.CreateMap<IdentityUser, UserDto>();

            Mapper.CreateMap<RoleDto, IdentityRole>();
            Mapper.CreateMap<IdentityRole, RoleDto>();

            Mapper.CreateMap<RegGroupDto, RegGroup>();
            Mapper.CreateMap<RegGroup, RegGroupDto>();

            Mapper.CreateMap<ResultDto, Result>();
            Mapper.CreateMap<Result, ResultDto>();

            Mapper.CreateMap<SubjectDto, Subject>();
            Mapper.CreateMap<Subject, SubjectDto>();
        }
    }
}