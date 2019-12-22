using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Dtos.DataGrid;
using MyPortal.BusinessLogic.Dtos.Identity;
using MyPortal.BusinessLogic.Dtos.Lite;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public enum MapperType
    {
        BusinessObjects,
        DataGridObjects
    }
    public class MappingService
    {
        private readonly MapperConfiguration _configuration;

        public MappingService(MapperType mapperType)
        {
            if (mapperType == MapperType.BusinessObjects)
            {
                _configuration = GetBusinessConfiguration();
            }
            else
            {
                _configuration = GetDataGridConfiguration();
            }
        }

        public MappingService(MapperConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal MapperConfiguration GetBusinessConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AcademicYear, AcademicYearDto>().ReverseMap();
                cfg.CreateMap<Achievement, AchievementDto>().ReverseMap();
                cfg.CreateMap<AchievementType, AchievementTypeDto>().ReverseMap();
                cfg.CreateMap<Address, AddressDto>().ReverseMap();
                cfg.CreateMap<AddressPerson, AddressPersonDto>().ReverseMap();
                cfg.CreateMap<Aspect, AspectDto>().ReverseMap();
                cfg.CreateMap<AspectType, AspectTypeDto>().ReverseMap();
                cfg.CreateMap<AttendanceCode, AttendanceCodeDto>().ReverseMap();
                cfg.CreateMap<AttendanceCodeMeaning, AttendanceCodeMeaningDto>().ReverseMap();
                cfg.CreateMap<AttendanceMark, AttendanceMarkDto>().ReverseMap();
                cfg.CreateMap<AttendanceWeek, AttendanceWeekDto>().ReverseMap();
                cfg.CreateMap<BasketItem, BasketItemDto>().ReverseMap();
                cfg.CreateMap<Bulletin, BulletinDto>().ReverseMap();
                cfg.CreateMap<Class, ClassDto>().ReverseMap();
                cfg.CreateMap<Comment, CommentDto>().ReverseMap();
                cfg.CreateMap<CommentBank, CommentBankDto>().ReverseMap();
                cfg.CreateMap<CommunicationLog, CommunicationLogDto>().ReverseMap();
                cfg.CreateMap<CommunicationType, CommunicationTypeDto>().ReverseMap();
                cfg.CreateMap<Condition, ConditionDto>().ReverseMap();
                cfg.CreateMap<Contact, ContactDto>().ReverseMap();
                cfg.CreateMap<DietaryRequirement, DietaryRequirementDto>().ReverseMap();
                cfg.CreateMap<Document, DocumentDto>().ReverseMap();
                cfg.CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
                cfg.CreateMap<EmailAddress, EmailAddressType>().ReverseMap();
                cfg.CreateMap<Enrolment, EnrolmentDto>().ReverseMap();
                cfg.CreateMap<GiftedTalented, GiftedTalentedDto>().ReverseMap();
                cfg.CreateMap<GovernanceType, GovernanceTypeDto>().ReverseMap();
                cfg.CreateMap<Grade, GradeDto>().ReverseMap();
                cfg.CreateMap<GradeSet, GradeSetDto>().ReverseMap();
                cfg.CreateMap<House, HouseDto>().ReverseMap();
                cfg.CreateMap<Incident, IncidentDto>().ReverseMap();
                cfg.CreateMap<IntakeType, IntakeTypeDto>().ReverseMap();
                cfg.CreateMap<LessonPlan, LessonPlanDto>().ReverseMap();
                cfg.CreateMap<LessonPlanTemplate, LessonPlanTemplateDto>().ReverseMap();
                cfg.CreateMap<LocalAuthority, LocalAuthorityDto>().ReverseMap();
                cfg.CreateMap<Location, LocationDto>().ReverseMap();
                cfg.CreateMap<MedicalEvent, MedicalEventDto>().ReverseMap();
                cfg.CreateMap<Observation, ObservationDto>().ReverseMap();
                cfg.CreateMap<ObservationOutcome, ObservationOutcomeDto>().ReverseMap();
                cfg.CreateMap<Period, PeriodDto>().ReverseMap();
                cfg.CreateMap<Person, PersonDto>().ReverseMap();
                cfg.CreateMap<PersonAttachment, PersonAttachmentDto>().ReverseMap();
                cfg.CreateMap<PersonCondition, PersonConditionDto>().ReverseMap();
                cfg.CreateMap<PersonDietaryRequirement, PersonDietaryRequirementDto>().ReverseMap();
                cfg.CreateMap<Phase, PhaseDto>().ReverseMap();
                cfg.CreateMap<PhoneNumber, PhoneNumberDto>().ReverseMap();
                cfg.CreateMap<PhoneNumberType, PhoneNumberTypeDto>().ReverseMap();
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
                cfg.CreateMap<ProductType, ProductTypeDto>().ReverseMap();
                cfg.CreateMap<ProfileLogNote, ProfileLogNoteDto>().ReverseMap();
                cfg.CreateMap<ProfileLogNoteType, ProfileLogNoteTypeDto>().ReverseMap();
                cfg.CreateMap<RegGroup, RegGroupDto>().ReverseMap();
                cfg.CreateMap<RelationshipType, RelationshipTypeDto>().ReverseMap();
                cfg.CreateMap<Report, ReportDto>().ReverseMap();
                cfg.CreateMap<Result, ResultDto>().ReverseMap();
                cfg.CreateMap<ResultSet, ResultSetDto>().ReverseMap();
                cfg.CreateMap<Sale, SaleDto>().ReverseMap();
                cfg.CreateMap<School, SchoolDto>().ReverseMap();
                cfg.CreateMap<SchoolType, SchoolTypeDto>().ReverseMap();
                cfg.CreateMap<SenEvent, SenEventType>().ReverseMap();
                cfg.CreateMap<SenEventType, SenEventTypeDto>().ReverseMap();
                cfg.CreateMap<SenProvision, SenProvisionTypeDto>().ReverseMap();
                cfg.CreateMap<SenProvisionType, SenProvisionTypeDto>().ReverseMap();
                cfg.CreateMap<SenReviewType, SenReviewTypeDto>().ReverseMap();
                cfg.CreateMap<SenStatus, SenStatusDto>().ReverseMap();
                cfg.CreateMap<Session, SessionDto>().ReverseMap();
                cfg.CreateMap<StaffMember, StaffMemberDto>().ReverseMap();
                cfg.CreateMap<Student, StudentDto>().ReverseMap();
                cfg.CreateMap<StudentContact, StudentContactDto>().ReverseMap();
                cfg.CreateMap<StudyTopic, StudyTopicDto>().ReverseMap();
                cfg.CreateMap<Subject, SubjectDto>().ReverseMap();
                cfg.CreateMap<SubjectStaffMember, SubjectStaffMemberDto>().ReverseMap();
                cfg.CreateMap<SubjectStaffMemberRole, SubjectStaffMemberRoleDto>().ReverseMap();
                cfg.CreateMap<SystemArea, SystemAreaDto>().ReverseMap();
                cfg.CreateMap<TrainingCertificate, TrainingCertificateDto>().ReverseMap();
                cfg.CreateMap<TrainingCertificateStatus, TrainingCertificateStatusDto>().ReverseMap();
                cfg.CreateMap<TrainingCourse, TrainingCourseDto>().ReverseMap();
                cfg.CreateMap<YearGroup, YearGroupDto>().ReverseMap();
            });

            return config;
        }

        internal MapperConfiguration GetDataGridConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, DataGridStudentDto>()
                    .ForMember(dest => dest.DisplayName,
                        opts => opts.MapFrom(src => src.GetDisplayName()))
                    .ForMember(dest => dest.HouseName,
                        opts => opts.MapFrom(src => src.House.Name))
                    .ForMember(dest => dest.RegGroupName,
                        opts => opts.MapFrom(src => src.RegGroup.Name))
                    .ForMember(dest => dest.YearGroupName,
                        opts => opts.MapFrom(src => src.YearGroup.Name));

                cfg.CreateMap<ProfileLogNote, DataGridProfileLogDto>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.AuthorName,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName()))
                    .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => src.Date))
                    .ForMember(dest => dest.LogTypeName,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.Name))
                    .ForMember(dest => dest.Message,
                        opts => opts.MapFrom(src => src.Message));

                cfg.CreateMap<Document, DataGridDocumentDto>()
                    .ForMember(dest => dest.DocumentId,
                        opts => opts.MapFrom(src => src.Id));

                cfg.CreateMap<PersonAttachment, DataGridPersonAttachmentDto>()
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

                cfg.CreateMap<Achievement, DataGridAchievementDto>()
                    .ForMember(dest => dest.Location,
                        opts => opts.MapFrom(src => src.Location.Description))
                    .ForMember(dest => dest.TypeName,
                        opts => opts.MapFrom(src => src.Type.Description))
                    .ForMember(dest => dest.RecordedBy,
                        opts => opts.MapFrom(src => src.RecordedBy.GetDisplayName()));

                cfg.CreateMap<Incident, DataGridIncidentDto>()
                    .ForMember(dest => dest.Location,
                        opts => opts.MapFrom(src => src.Location.Description))
                    .ForMember(dest => dest.TypeName,
                        opts => opts.MapFrom(src => src.Type.Description))
                    .ForMember(dest => dest.RecordedBy,
                        opts => opts.MapFrom(src => src.RecordedBy.GetDisplayName()));

                cfg.CreateMap<Observation, DataGridObservationDto>()
                    .ForMember(dest => dest.ObserveeName,
                        opts => opts.MapFrom(src => src.Observee.GetDisplayName()))
                    .ForMember(dest => dest.ObserverName,
                        opts => opts.MapFrom(src => src.Observer.GetDisplayName()))
                    .ForMember(dest => dest.Outcome,
                        opts => opts.MapFrom(src => src.Outcome.ToString()));

                cfg.CreateMap<TrainingCertificate, DataGridTrainingCertificateDto>()
                    .ForMember(dest => dest.Status,
                        opts => opts.MapFrom(src => src.Status.ToString()))
                    .ForMember(dest => dest.CourseCode,
                        opts => opts.MapFrom(src => src.TrainingCourse.Code))
                    .ForMember(dest => dest.CourseDescription,
                        opts => opts.MapFrom(src => src.TrainingCourse.Description));

                cfg.CreateMap<Product, DataGridProductDto>()
                    .ForMember(dest => dest.TypeDescription,
                        opts => opts.MapFrom(src => src.Type.Description));

                cfg.CreateMap<Sale, DataGridSaleDto>()
                    .ForMember(dest => dest.ProductDescription,
                        opts => opts.MapFrom(src => src.Product.Description))
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.GetDisplayName()));

                cfg.CreateMap<ResultSet, DataGridResultSetDto>();

                cfg.CreateMap<Session, DataGridSessionDto>()
                    .ForMember(dest => dest.ClassName,
                        opts => opts.MapFrom(src => src.Class.Name))
                    .ForMember(dest => dest.PeriodName,
                        opts => opts.MapFrom(src => src.Period.Name))
                    .ForMember(dest => dest.Teacher,
                        opts => opts.MapFrom(src => src.Class.Teacher.GetDisplayName()))
                    .ForMember(dest => dest.Time,
                        opts => opts.MapFrom(src => src.Period.GetTimeDisplay()));

                cfg.CreateMap<Class, DataGridClassDto>()
                    .ForMember(dest => dest.Subject,
                        opts => opts.MapFrom(src => src.GetSubjectName()))
                    .ForMember(dest => dest.Teacher,
                        opts => opts.MapFrom(src => src.Teacher.GetDisplayName()));

                cfg.CreateMap<Enrolment, DataGridEnrolmentDto>()
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.GetDisplayName()))
                    .ForMember(dest => dest.ClassName,
                        opts => opts.MapFrom(src => src.Class.Name));

                cfg.CreateMap<LessonPlan, DataGridLessonPlanDto>()
                    .ForMember(dest => dest.StudyTopic,
                        opts => opts.MapFrom(src => src.StudyTopic.Name))
                    .ForMember(dest => dest.Author,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName()));

                cfg.CreateMap<StudyTopic, DataGridStudyTopicDto>()
                    .ForMember(dest => dest.SubjectName,
                        opts => opts.MapFrom(src => src.Subject.Name))
                    .ForMember(dest => dest.YearGroup,
                        opts => opts.MapFrom(src => src.YearGroup.Name));

                cfg.CreateMap<Subject, DataGridSubjectDto>();

                cfg.CreateMap<StaffMember, DataGridStaffMemberDto>()
                    .ForMember(dest => dest.DisplayName,
                        opts => opts.MapFrom(src => src.GetFullName()));

                cfg.CreateMap<TrainingCourse, DataGridTrainingCourseDto>();

                cfg.CreateMap<BasketItem, DataGridBasketItemDto>()
                    .ForMember(dest => dest.ProductDescription,
                        opts => opts.MapFrom(src => src.Product.Description))
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.GetDisplayName()));
                cfg.CreateMap<Comment, DataGridCommentDto>()
                    .ForMember(dest => dest.CommentBankName,
                        opts => opts.MapFrom(src => src.CommentBank.Name));

                cfg.CreateMap<CommentBank, DataGridCommentBankDto>();

                cfg.CreateMap<StudentAttendanceMarkCollection, StudentAttendanceMarkSingular>()
                    .ForMember(dest => dest.Mark,
                        opts => opts.MapFrom(src => src.Marks.ElementAt(0)));

                cfg.CreateMap<StudentAttendanceMarkSingular, StudentAttendanceMarkCollection>()
                    .ForMember(dest => dest.Marks,
                        opts => opts.MapFrom(src => new List<AttendanceMarkLiteDto> { src.Mark }));

                cfg.CreateMap<ResultDto, DataGridResultDto>()
                    .ForMember(dest => dest.ResultSet,
                        opts => opts.MapFrom(src => src.ResultSet.Name))
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.Person.GetDisplayName()))
                    .ForMember(dest => dest.Aspect,
                        opts => opts.MapFrom(src => src.Aspect.Description));

                cfg.CreateMap<PersonCondition, DataGridPersonConditionDto>()
                    .ForMember(dest => dest.Condition,
                        opts => opts.MapFrom(src => src.Condition.Description));

                cfg.CreateMap<PersonDietaryRequirement, DataGridPersonDietaryRequirementDto>()
                    .ForMember(dest => dest.Description,
                        opts => opts.MapFrom(src => src.DietaryRequirement.Description));

                cfg.CreateMap<SubjectStaffMember, DataGridCurriculumSubjectStaffMemberDto>()
                    .ForMember(dest => dest.StaffMemberName,
                        opts => opts.MapFrom(src => src.StaffMember.GetFullName()))
                    .ForMember(dest => dest.Role,
                        opts => opts.MapFrom(src => src.Role.Description));

                cfg.CreateMap<PhoneNumber, DataGridPhoneNumberDto>()
                    .ForMember(dest => dest.Type,
                        opts => opts.MapFrom(src => src.Type.Description));

                cfg.CreateMap<EmailAddress, DataGridEmailAddressDto>()
                    .ForMember(dest => dest.Type,
                        opts => opts.MapFrom(src => src.Type.ToString()));
            });

            return config;
        }

        public TDest Map<TDest>(object source)
        {
            var mapper = new Mapper(_configuration);

            return mapper.Map<TDest>(source);
        }

        public IEnumerable<TDest> MapMultiple<TDest>(IEnumerable<object> source)
        {
            var mapper = new Mapper(_configuration);

            return source.Select(mapper.Map<TDest>).ToList();
        }

        public void ValidateConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
