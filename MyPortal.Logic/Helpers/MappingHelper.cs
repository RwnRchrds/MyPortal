using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Summary;

namespace MyPortal.Logic.Helpers
{
    public class MappingHelper
    {
        public static IMapper GetBusinessConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AcademicYear, AcademicYearModel>().ReverseMap();
                cfg.CreateMap<ApplicationRole, RoleModel>().ReverseMap();
                cfg.CreateMap<ApplicationUser, UserModel>().ReverseMap();
                cfg.CreateMap<House, HouseModel>().ReverseMap();
                cfg.CreateMap<Directory, DirectoryModel>().ReverseMap();
                cfg.CreateMap<Document, DocumentModel>().ReverseMap();
                cfg.CreateMap<DocumentType, DocumentTypeModel>().ReverseMap();
                cfg.CreateMap<Person, PersonModel>().ReverseMap();
                cfg.CreateMap<LogNote, LogNoteModel>().ReverseMap();
                cfg.CreateMap<LogNoteType, LogNoteTypeModel>().ReverseMap();
                cfg.CreateMap<RegGroup, RegGroupModel>().ReverseMap();
                cfg.CreateMap<SenStatus, SenStatusModel>().ReverseMap();
                cfg.CreateMap<StaffMember, StaffMemberModel>().ReverseMap();
                cfg.CreateMap<Student, StudentModel>().ReverseMap();
                cfg.CreateMap<YearGroup, YearGroupModel>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static IMapper GetDataGridConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoleModel, ApplicationRoleListModel>();
                cfg.CreateMap<StudentModel, StudentListModel>()
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
                cfg.CreateMap<LogNoteModel, LogNoteListModel>()
                    .ForMember(dest => dest.AuthorName,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName(true)))
                    .ForMember(dest => dest.LogTypeName,
                        opts => opts.MapFrom(src => src.LogNoteType.Name))
                    .ForMember(dest => dest.LogTypeColourCode,
                        opts => opts.MapFrom(src => src.LogNoteType.ColourCode))
                    .ForMember(dest => dest.LogTypeIcon,
                        opts => opts.MapFrom(src => src.LogNoteType.GetIcon()));
            });

            return new Mapper(config);
        }
    }
}
