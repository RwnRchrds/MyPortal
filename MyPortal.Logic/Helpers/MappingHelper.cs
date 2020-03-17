using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Admin;
using MyPortal.Logic.Models.DataGrid;
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
                cfg.CreateMap<AcademicYear, AcademicYearDetails>().ReverseMap();
                cfg.CreateMap<ApplicationRole, RoleDetails>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserDetails>().ReverseMap();
                cfg.CreateMap<House, HouseDetails>().ReverseMap();
                cfg.CreateMap<Person, PersonDetails>().ReverseMap();
                cfg.CreateMap<ProfileLogNote, ProfileLogNoteDetails>().ReverseMap();
                cfg.CreateMap<ProfileLogNoteType, ProfileLogNoteTypeDetails>().ReverseMap();
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
                cfg.CreateMap<RoleDetails, DataGridApplicationRole>();
                cfg.CreateMap<StudentDetails, DataGridStudent>()
                    .ForMember(dest => dest.DisplayName,
                        opts => opts.MapFrom(src => src.Person.GetDisplayName(false)))
                    .ForMember(dest => dest.RegGroupName,
                        opts => opts.MapFrom(src => src.RegGroup.Name))
                    .ForMember(dest => dest.YearGroupName,
                        opts => opts.MapFrom(src => src.YearGroup.Name))
                    .ForMember(dest => dest.HouseName,
                        opts => opts.MapFrom(src => src.House.Name))
                    .ForMember(dest => dest.Gender,
                        opts => opts.MapFrom(src => src.Person.Gender));
                cfg.CreateMap<ProfileLogNoteDetails, DataGridProfileLogNote>()
                    .ForMember(dest => dest.AuthorName,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName(true)))
                    .ForMember(dest => dest.LogTypeName,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.Name))
                    .ForMember(dest => dest.LogTypeColourCode,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.ColourCode))
                    .ForMember(dest => dest.LogTypeIcon,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.GetIcon()));
            });

            return new Mapper(config);
        }
    }
}
