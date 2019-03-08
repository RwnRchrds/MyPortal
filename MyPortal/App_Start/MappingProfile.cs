using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos;
using MyPortal.Dtos.Identity;
using MyPortal.Models;

namespace MyPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            CreateMap<LogDto, Log>();
            CreateMap<Log, LogDto>();

            CreateMap<YearGroupDto, YearGroup>();
            CreateMap<YearGroup, YearGroupDto>();

            CreateMap<RegGroupDto, RegGroup>();
            CreateMap<RegGroup, RegGroupDto>();

            CreateMap<StaffDto, Staff>();
            CreateMap<Staff, StaffDto>();

            CreateMap<TrainingCertificateDto, TrainingCertificate>();
            CreateMap<TrainingCertificate, TrainingCertificateDto>();

            CreateMap<TrainingCourseDto, TrainingCourse>();
            CreateMap<TrainingCourse, TrainingCourseDto>();

            CreateMap<UserDto, IdentityUser>();
            CreateMap<IdentityUser, UserDto>();

            CreateMap<RoleDto, IdentityRole>();
            CreateMap<IdentityRole, RoleDto>();

            CreateMap<RegGroupDto, RegGroup>();
            CreateMap<RegGroup, RegGroupDto>();

            CreateMap<ResultDto, Result>();
            CreateMap<Result, ResultDto>();

            CreateMap<ResultSet, ResultSetDto>();
            CreateMap<ResultSetDto, ResultSet>();

            CreateMap<SubjectDto, Subject>();
            CreateMap<Subject, SubjectDto>();

            CreateMap<LogTypeDto, LogType>();
            CreateMap<LogType, LogTypeDto>();

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>();

            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<BasketItem, BasketItemDto>();

            CreateMap<TrainingStatusDto, TrainingStatus>();
            CreateMap<TrainingStatus, TrainingStatusDto>();

            CreateMap<DocumentDto, Document>();
            CreateMap<Document, DocumentDto>();

            CreateMap<StudentDocumentDto, StudentDocument>();
            CreateMap<StudentDocument, StudentDocumentDto>();

            CreateMap<StaffDocumentDto, StaffDocument>();
            CreateMap<StaffDocument, StaffDocumentDto>();

            CreateMap<GradeSetDto, GradeSet>();
            CreateMap<GradeSet, GradeSetDto>();

            CreateMap<GradeDto, Grade>();
            CreateMap<Grade, GradeDto>();

            CreateMap<CommentBankDto, CommentBank>();
            CreateMap<CommentBank, CommentBankDto>();

            CreateMap<CommentDto, Comment>();
            CreateMap<Comment, CommentDto>();

            CreateMap<StudyTopic, StudyTopicDto>();
            CreateMap<StudyTopicDto, StudyTopic>();
        }
    }
}