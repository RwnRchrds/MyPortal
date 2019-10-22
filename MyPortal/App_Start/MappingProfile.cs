using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Dtos.Identity;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using Syncfusion.EJ2.Grids;

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

            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();

            CreateMap<ApplicationRole, ApplicationRoleDto>();
            CreateMap<ApplicationRoleDto, ApplicationRole>();

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

            CreateMap<CurriculumSession, CurriculumSessionDto>();
            CreateMap<CurriculumSessionDto, CurriculumSession>();

            CreateMap<AttendanceWeek, AttendanceWeekDto>();
            CreateMap<AttendanceWeekDto, AttendanceWeek>();

            CreateMap<AttendanceMark, AttendanceMarkDto>();
            CreateMap<AttendanceMarkDto, AttendanceMark>();

            CreateMap<AttendanceMark, AttendanceMarkLite>();
            CreateMap<AttendanceMarkLite, AttendanceMark>();

            CreateMap<FinanceProductType, FinanceProductTypeDto>();
            CreateMap<FinanceProductTypeDto, FinanceProductType>();

            CreateMap<CurriculumStudyTopic, CurriculumStudyTopicDto>();
            CreateMap<CurriculumLessonPlan, CurriculumLessonPlanDto>();

            CreateMap<BehaviourAchievement, BehaviourAchievementDto>();

            CreateMap<BehaviourIncident, BehaviourIncidentDto>();

            CreateMap<Student, GridStudentDto>()
                .ForMember(dest => dest.DisplayName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src).ResponseObject))
                .ForMember(dest => dest.HouseName,
                    opts => opts.MapFrom(src => src.House.Name))
                .ForMember(dest => dest.RegGroupName,
                    opts => opts.MapFrom(src => src.PastoralRegGroup.Name))
                .ForMember(dest => dest.YearGroupName,
                    opts => opts.MapFrom(src => src.PastoralYearGroup.Name));

            CreateMap<ProfileLog, GridProfileLogDto>()
                .ForMember(dest => dest.Id,
                    opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Author).ResponseObject))
                .ForMember(dest => dest.Date,
                    opts => opts.MapFrom(src => src.Date))
                .ForMember(dest => dest.LogTypeName,
                    opts => opts.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Message,
                    opts => opts.MapFrom(src => src.Message));

            CreateMap<Document, GridDocumentDto>()
                .ForMember(dest => dest.DocumentId,
                    opts => opts.MapFrom(src => src.Id));

            CreateMap<PersonDocument, GridPersonDocumentDto>()
                .ForMember(dest => dest.Approved,
                    opts => opts.MapFrom(src => src.Document.Approved))
                .ForMember(dest => dest.Date,
                    opts => opts.MapFrom(src => src.Document.Date))
                .ForMember(dest => dest.Description,
                    opts => opts.MapFrom(src => src.Document.Description))
                .ForMember(dest => dest.Url,
                    opts => opts.MapFrom(src => src.Document.Url))
                .ForMember(dest => dest.DocumentId,
                    opts => opts.MapFrom(src => src.Document.Id));

            CreateMap<BehaviourAchievement, GridBehaviourAchievementDto>()
                .ForMember(dest => dest.Location,
                    opts => opts.MapFrom(src => src.Location.Description))
                .ForMember(dest => dest.TypeName,
                    opts => opts.MapFrom(src => src.Type.Description))
                .ForMember(dest => dest.RecordedBy,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.RecordedBy).ResponseObject));

            CreateMap<BehaviourIncident, GridBehaviourIncidentDto>()
                .ForMember(dest => dest.Location,
                    opts => opts.MapFrom(src => src.Location.Description))
                .ForMember(dest => dest.TypeName,
                    opts => opts.MapFrom(src => src.Type.Description))
                .ForMember(dest => dest.RecordedBy,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.RecordedBy).ResponseObject));

            CreateMap<PersonnelObservation, GridPersonnelObservationDto>()
                .ForMember(dest => dest.ObserveeName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Observee).ResponseObject))
                .ForMember(dest => dest.ObserverName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Observer).ResponseObject))
                .ForMember(dest => dest.Outcome,
                    opts => opts.MapFrom(src => src.Outcome.ToString()));

            CreateMap<PersonnelTrainingCertificate, GridPersonnelTrainingCertificateDto>()
                .ForMember(dest => dest.Status,
                    opts => opts.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.CourseCode,
                    opts => opts.MapFrom(src => src.TrainingCourse.Code))
                .ForMember(dest => dest.CourseDescription,
                    opts => opts.MapFrom(src => src.TrainingCourse.Description));

            CreateMap<FinanceProduct, GridFinanceProductDto>()
                .ForMember(dest => dest.TypeDescription,
                    opts => opts.MapFrom(src => src.Type.Description));

            CreateMap<FinanceSale, GridFinanceSaleDto>()
                .ForMember(dest => dest.ProductDescription,
                    opts => opts.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.StudentName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src.Student).ResponseObject));

            CreateMap<AssessmentResultSet, GridAssessmentResultSetDto>();

            CreateMap<CurriculumSession, GridCurriculumSessionDto>()
                .ForMember(dest => dest.ClassName,
                    opts => opts.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.PeriodName,
                    opts => opts.MapFrom(src => src.Period.Name))
                .ForMember(dest => dest.Teacher,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Class.Teacher).ResponseObject))
                .ForMember(dest => dest.Time,
                    opts => opts.MapFrom(src => AttendanceProcesses.GetPeriodTime(src.Period)));

            CreateMap<CurriculumClass, GridCurriculumClassDto>()
                .ForMember(dest => dest.Subject,
                    opts => opts.MapFrom(src => CurriculumProcesses.GetSubjectNameForClass(src)))
                .ForMember(dest => dest.Teacher,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Teacher).ResponseObject));

            CreateMap<CurriculumEnrolment, GridCurriculumEnrolmentDto>()
                .ForMember(dest => dest.StudentName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src.Student).ResponseObject))
                .ForMember(dest => dest.ClassName,
                    opts => opts.MapFrom(src => src.Class.Name));

            CreateMap<CurriculumLessonPlan, GridCurriculumLessonPlanDto>()
                .ForMember(dest => dest.StudyTopic,
                    opts => opts.MapFrom(src => src.StudyTopic.Name))
                .ForMember(dest => dest.Author,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Author).ResponseObject));

            CreateMap<CurriculumStudyTopic, GridCurriculumStudyTopicDto>()
                .ForMember(dest => dest.SubjectName,
                    opts => opts.MapFrom(src => src.CurriculumSubject.Name))
                .ForMember(dest => dest.YearGroup,
                    opts => opts.MapFrom(src => src.PastoralYearGroup.Name));

            CreateMap<CurriculumSubject, GridCurriculumSubjectDto>()
                .ForMember(dest => dest.LeaderName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffDisplayName(src.Leader).ResponseObject));

            CreateMap<StaffMember, GridStaffMemberDto>()
                .ForMember(dest => dest.DisplayName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStaffFullName(src).ResponseObject));

            CreateMap<PersonnelTrainingCourse, GridPersonnelTrainingCourseDto>();

            CreateMap<FinanceBasketItem, GridFinanceBasketItemDto>()
                .ForMember(dest => dest.ProductDescription,
                    opts => opts.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.StudentName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src.Student)));
            CreateMap<ProfileComment, GridProfileCommentDto>()
                .ForMember(dest => dest.CommentBankName,
                    opts => opts.MapFrom(src => src.CommentBank.Name));

            CreateMap<ProfileCommentBank, GridProfileCommentBankDto>();

            CreateMap<StudentAttendanceMarkCollection, StudentAttendanceMarkSingular>()
                .ForMember(dest => dest.Mark,
                    opts => opts.MapFrom(src => src.Marks.ElementAt(0)));

            CreateMap<StudentAttendanceMarkSingular, StudentAttendanceMarkCollection>()
                .ForMember(dest => dest.Marks,
                    opts => opts.MapFrom(src => new List<AttendanceMarkLite> {src.Mark}));

            CreateMap<ApplicationUser, GridApplicationUserDto>();

            CreateMap<AssessmentResult, GridAssessmentResultDto>()
                .ForMember(dest => dest.ResultSet,
                    opts => opts.MapFrom(src => src.ResultSet.Name))
                .ForMember(dest => dest.StudentName,
                    opts => opts.MapFrom(src => PeopleProcesses.GetStudentDisplayName(src.Student)))
                .ForMember(dest => dest.Aspect,
                    opts => opts.MapFrom(src => src.Aspect.Description));

            CreateMap<MedicalPersonCondition, GridMedicalPersonConditionDto>()
                .ForMember(dest => dest.Condition,
                    opts => opts.MapFrom(src => src.Condition.Description));

            CreateMap<MedicalPersonDietaryRequirement, GridMedicalPersonDietaryRequirementDto>()
                .ForMember(dest => dest.Description,
                    opts => opts.MapFrom(src => src.DietaryRequirement.Description));
        }
    }
}    