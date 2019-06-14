using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos;
using MyPortal.Dtos.Identity;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Dtos.SpecialDtos;
using MyPortal.Models.Database;
using MyPortal.Processes;

namespace MyPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            CreateMap<ProfileLogDto, ProfileLog>();
            CreateMap<ProfileLog, ProfileLogDto>();

            CreateMap<PastoralYearGroupDto, PastoralYearGroup>();
            CreateMap<PastoralYearGroup, PastoralYearGroupDto>();

            CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
            CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

            CreateMap<StaffMemberDto, StaffMember>();
            CreateMap<StaffMember, StaffMemberDto>();

            CreateMap<PersonnelTrainingCertificateDto, PersonnelTrainingCertificate>();
            CreateMap<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>();

            CreateMap<PersonnelTrainingCourseDto, PersonnelTrainingCourse>();
            CreateMap<PersonnelTrainingCourse, PersonnelTrainingCourseDto>();

            CreateMap<UserDto, IdentityUser>();
            CreateMap<IdentityUser, UserDto>();

            CreateMap<RoleDto, IdentityRole>();
            CreateMap<IdentityRole, RoleDto>();

            CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
            CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

            CreateMap<AssessmentResultDto, AssessmentResult>();
            CreateMap<AssessmentResult, AssessmentResultDto>();

            CreateMap<AssessmentResultSet, AssessmentResultSetDto>();
            CreateMap<AssessmentResultSetDto, AssessmentResultSet>();

            CreateMap<CurriculumSubjectDto, CurriculumSubject>();
            CreateMap<CurriculumSubject, CurriculumSubjectDto>();

            CreateMap<ProfileLogTypeDto, ProfileLogType>();
            CreateMap<ProfileLogType, ProfileLogTypeDto>();

            CreateMap<FinanceProductDto, FinanceProduct>();
            CreateMap<FinanceProduct, FinanceProductDto>();

            CreateMap<FinanceSaleDto, FinanceSale>();
            CreateMap<FinanceSale, FinanceSaleDto>();

            CreateMap<FinanceBasketItemDto, FinanceBasketItem>();
            CreateMap<FinanceBasketItem, FinanceBasketItemDto>();

            CreateMap<PersonnelTrainingStatusDto, PersonnelTrainingStatus>();
            CreateMap<PersonnelTrainingStatus, PersonnelTrainingStatusDto>();

            CreateMap<DocumentDto, Document>();
            CreateMap<Document, DocumentDto>();

            CreateMap<AssessmentGradeSetDto, AssessmentGradeSet>();
            CreateMap<AssessmentGradeSet, AssessmentGradeSetDto>();

            CreateMap<AssessmentGradeDto, AssessmentGrade>();
            CreateMap<AssessmentGrade, AssessmentGradeDto>();

            CreateMap<ProfileCommentBankDto, ProfileCommentBank>();
            CreateMap<ProfileCommentBank, ProfileCommentBankDto>();

            CreateMap<ProfileCommentDto, ProfileComment>();
            CreateMap<ProfileComment, ProfileCommentDto>();

            CreateMap<CurriculumStudyTopic, CurriculumStudyTopicDto>();
            CreateMap<CurriculumStudyTopicDto, CurriculumStudyTopic>();

            CreateMap<CurriculumClass, CurriculumClassDto>();
            CreateMap<CurriculumClassDto, CurriculumClass>();

            CreateMap<CurriculumClassPeriod, CurriculumClassPeriodDto>();
            CreateMap<CurriculumClassPeriodDto, CurriculumClassPeriod>();

            CreateMap<AttendanceWeek, AttendanceWeekDto>();
            CreateMap<AttendanceWeekDto, AttendanceWeek>();

            CreateMap<AttendanceRegisterMark, AttendanceRegisterMarkDto>();
            CreateMap<AttendanceRegisterMarkDto, AttendanceRegisterMark>();

            CreateMap<AttendanceRegisterMark, AttendanceRegisterMarkLite>();
            CreateMap<AttendanceRegisterMarkLite, AttendanceRegisterMark>();

            CreateMap<FinanceProductType, FinanceProductTypeDto>();
            CreateMap<FinanceProductTypeDto, FinanceProductType>();

            CreateMap<Student, StudentSearchDto>()
                .ForMember(dest => dest.DisplayName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src)))
                .ForMember(dest => dest.HouseName,
                    opts => opts.MapFrom(src => src.House.Name))
                .ForMember(dest => dest.RegGroupName,
                    opts => opts.MapFrom(src => src.PastoralRegGroup.Name))
                .ForMember(dest => dest.YearGroupName,
                    opts => opts.MapFrom(src => src.PastoralYearGroup.Name));
        }
    }
}