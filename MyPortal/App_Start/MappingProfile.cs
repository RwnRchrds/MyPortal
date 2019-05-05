using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos;
using MyPortal.Dtos.Identity;
using MyPortal.Models.Database;

namespace MyPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PeopleStudent, CoreStudentDto>();
            CreateMap<CoreStudentDto, PeopleStudent>();

            CreateMap<ProfileLogDto, ProfileLog>();
            CreateMap<ProfileLog, ProfileLogDto>();

            CreateMap<PastoralYearGroupDto, PastoralYearGroup>();
            CreateMap<PastoralYearGroup, PastoralYearGroupDto>();

            CreateMap<PastoralRegGroupDto, PastoralRegGroup>();
            CreateMap<PastoralRegGroup, PastoralRegGroupDto>();

            CreateMap<CoreStaffMemberDto, PeopleStaffMember>();
            CreateMap<PeopleStaffMember, CoreStaffMemberDto>();

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

            CreateMap<CoreDocumentDto, DocsDocument>();
            CreateMap<DocsDocument, CoreDocumentDto>();

            CreateMap<CoreStudentDocumentDto, DocsStudentDocument>();
            CreateMap<DocsStudentDocument, CoreStudentDocumentDto>();

            CreateMap<CoreStaffDocumentDto, DocsStaffDocument>();
            CreateMap<DocsStaffDocument, CoreStaffDocumentDto>();

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
        }
    }
}