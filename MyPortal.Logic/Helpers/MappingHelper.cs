using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Business;
using MyPortal.Logic.Models.Dtos;
using MyPortal.Logic.Models.Dtos.DataGrid;
using MyPortal.Logic.Models.Lite;

namespace MyPortal.Logic.Helpers
{
    public class MappingHelper
    {
        public static Mapper GetMapperBusinessConfiguration()
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
                cfg.CreateMap<MedicalCondition, MedicalConditionDto>().ReverseMap();
                cfg.CreateMap<Contact, ContactDto>().ReverseMap();
                cfg.CreateMap<Detention, DetentionDto>().ReverseMap();
                cfg.CreateMap<DetentionType, DetentionTypeDto>().ReverseMap();
                cfg.CreateMap<DiaryEvent, DiaryEventDto>().ReverseMap();
                cfg.CreateMap<DietaryRequirement, DietaryRequirementDto>().ReverseMap();
                cfg.CreateMap<Document, DocumentDto>().ReverseMap();
                cfg.CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
                cfg.CreateMap<EmailAddress, EmailAddressDto>().ReverseMap();
                cfg.CreateMap<EmailAddressType, EmailAddressTypeDto>().ReverseMap();
                cfg.CreateMap<Enrolment, EnrolmentDto>().ReverseMap();
                cfg.CreateMap<GiftedTalented, GiftedTalentedDto>().ReverseMap();
                cfg.CreateMap<GovernanceType, GovernanceTypeDto>().ReverseMap();
                cfg.CreateMap<Grade, GradeDto>().ReverseMap();
                cfg.CreateMap<GradeSet, GradeSetDto>().ReverseMap();
                cfg.CreateMap<House, HouseDto>().ReverseMap();
                cfg.CreateMap<Incident, IncidentDto>().ReverseMap();
                cfg.CreateMap<IncidentType, IncidentTypeDto>().ReverseMap();
                cfg.CreateMap<IncidentDetention, IncidentDetentionDto>().ReverseMap();
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
                cfg.CreateMap<SenEvent, SenEventDto>().ReverseMap();
                cfg.CreateMap<SenEventType, SenEventTypeDto>().ReverseMap();
                cfg.CreateMap<SenProvision, SenProvisionDto>().ReverseMap();
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
                cfg.CreateMap<DiaryEventAttendee, DiaryEventAttendeeDto>().ReverseMap();
                cfg.CreateMap<DiaryEventInvitationResponse, DiaryEventInvitationResponseDto>().ReverseMap();
                cfg.CreateMap<DiaryEventType, DiaryEventTypeDto>().ReverseMap();
                cfg.CreateMap<Task, TaskDto>().ReverseMap();
                cfg.CreateMap<DiaryEventTemplate, DiaryEventTemplateDto>().ReverseMap();
                cfg.CreateMap<Homework, HomeworkDto>().ReverseMap();
                cfg.CreateMap<HomeworkAttachment, HomeworkAttachmentDto>().ReverseMap();
                cfg.CreateMap<HomeworkSubmission, HomeworkSubmissionDto>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static Mapper GetMapperDataGridConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentDto, DataGridStudentDto>()
                    .ForMember(dest => dest.DisplayName,
                        opts => opts.MapFrom(src => src.Person.GetDisplayName()))
                    .ForMember(dest => dest.HouseName,
                        opts => opts.MapFrom(src => src.House.Name))
                    .ForMember(dest => dest.HouseColourCode,
                        opts => opts.MapFrom(src => src.House.ColourCode))
                    .ForMember(dest => dest.RegGroupName,
                        opts => opts.MapFrom(src => src.RegGroup.Name))
                    .ForMember(dest => dest.YearGroupName,
                        opts => opts.MapFrom(src => src.YearGroup.Name));

                cfg.CreateMap<ProfileLogNoteDto, DataGridProfileLogNoteDto>()
                    .ForMember(dest => dest.Id,
                        opts => opts.MapFrom(src => src.Id))
                    .ForMember(dest => dest.AuthorName,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName()))
                    .ForMember(dest => dest.Date,
                        opts => opts.MapFrom(src => src.Date))
                    .ForMember(dest => dest.LogTypeName,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.Name))
                    .ForMember(dest => dest.LogTypeColourCode,
                        opts => opts.MapFrom(src => src.ProfileLogNoteType.ColourCode))
                    .ForMember(dest => dest.Message,
                        opts => opts.MapFrom(src => src.Message));

                cfg.CreateMap<DocumentDto, DataGridDocumentDto>()
                    .ForMember(dest => dest.DocumentId,
                        opts => opts.MapFrom(src => src.Id));

                cfg.CreateMap<PersonAttachmentDto, DataGridPersonAttachmentDto>()
                    .ForMember(dest => dest.Approved,
                        opts => opts.MapFrom(src => src.Document.Approved))
                    .ForMember(dest => dest.UploadedDate,
                        opts => opts.MapFrom(src => src.Document.UploadedDate))
                    .ForMember(dest => dest.Description,
                        opts => opts.MapFrom(src => src.Document.Description))
                    .ForMember(dest => dest.DownloadUrl,
                        opts => opts.MapFrom(src => src.Document.DownloadUrl))
                    .ForMember(dest => dest.DocumentId,
                        opts => opts.MapFrom(src => src.Document.Id));

                cfg.CreateMap<AchievementDto, DataGridAchievementDto>()
                    .ForMember(dest => dest.Location,
                        opts => opts.MapFrom(src => src.Location.Description))
                    .ForMember(dest => dest.TypeName,
                        opts => opts.MapFrom(src => src.Type.Description))
                    .ForMember(dest => dest.RecordedBy,
                        opts => opts.MapFrom(src => src.RecordedBy.GetDisplayName()));

                cfg.CreateMap<IncidentDto, DataGridIncidentDto>()
                    .ForMember(dest => dest.Location,
                        opts => opts.MapFrom(src => src.Location.Description))
                    .ForMember(dest => dest.TypeName,
                        opts => opts.MapFrom(src => src.Type.Description))
                    .ForMember(dest => dest.RecordedBy,
                        opts => opts.MapFrom(src => src.RecordedBy.GetDisplayName()));

                cfg.CreateMap<ObservationDto, DataGridObservationDto>()
                    .ForMember(dest => dest.ObserveeName,
                        opts => opts.MapFrom(src => src.Observee.GetDisplayName()))
                    .ForMember(dest => dest.ObserverName,
                        opts => opts.MapFrom(src => src.Observer.GetDisplayName()))
                    .ForMember(dest => dest.Outcome,
                        opts => opts.MapFrom(src => src.Outcome.Description))
                    .ForMember(dest => dest.OutcomeColourCode,
                        opts => opts.MapFrom(src => src.Outcome.ColourCode));

                cfg.CreateMap<TrainingCertificateDto, DataGridTrainingCertificateDto>()
                    .ForMember(dest => dest.Status,
                        opts => opts.MapFrom(src => src.Status.Description))
                    .ForMember(dest => dest.CourseCode,
                        opts => opts.MapFrom(src => src.TrainingCourse.Code))
                    .ForMember(dest => dest.CourseDescription,
                        opts => opts.MapFrom(src => src.TrainingCourse.Description))
                    .ForMember(dest => dest.StatusColourCode,
                        opts => opts.MapFrom(src => src.Status.ColourCode));

                cfg.CreateMap<ProductDto, DataGridProductDto>()
                    .ForMember(dest => dest.TypeDescription,
                        opts => opts.MapFrom(src => src.Type.Description));

                cfg.CreateMap<SaleDto, DataGridSaleDto>()
                    .ForMember(dest => dest.ProductDescription,
                        opts => opts.MapFrom(src => src.Product.Description))
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.Person.GetDisplayName()));

                cfg.CreateMap<ResultSetDto, DataGridResultSetDto>();

                cfg.CreateMap<SessionDto, DataGridSessionDto>()
                    .ForMember(dest => dest.ClassName,
                        opts => opts.MapFrom(src => src.Class.Name))
                    .ForMember(dest => dest.PeriodName,
                        opts => opts.MapFrom(src => src.Period.Name))
                    .ForMember(dest => dest.Teacher,
                        opts => opts.MapFrom(src => src.Class.Teacher.GetDisplayName()))
                    .ForMember(dest => dest.Time,
                        opts => opts.MapFrom(src => src.Period.GetTimeDisplay()));

                cfg.CreateMap<ClassDto, DataGridClassDto>()
                    .ForMember(dest => dest.Subject,
                        opts => opts.MapFrom(src => src.Subject.Name))
                    .ForMember(dest => dest.Teacher,
                        opts => opts.MapFrom(src => src.Teacher.GetDisplayName()));

                cfg.CreateMap<EnrolmentDto, DataGridEnrolmentDto>()
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.Person.GetDisplayName()))
                    .ForMember(dest => dest.ClassName,
                        opts => opts.MapFrom(src => src.Class.Name));

                cfg.CreateMap<LessonPlanDto, DataGridLessonPlanDto>()
                    .ForMember(dest => dest.StudyTopic,
                        opts => opts.MapFrom(src => src.StudyTopic.Name))
                    .ForMember(dest => dest.Author,
                        opts => opts.MapFrom(src => src.Author.GetDisplayName()));

                cfg.CreateMap<StudyTopicDto, DataGridStudyTopicDto>()
                    .ForMember(dest => dest.SubjectName,
                        opts => opts.MapFrom(src => src.Subject.Name))
                    .ForMember(dest => dest.YearGroup,
                        opts => opts.MapFrom(src => src.YearGroup.Name));

                cfg.CreateMap<SubjectDto, DataGridSubjectDto>();

                cfg.CreateMap<StaffMemberDto, DataGridStaffMemberDto>()
                    .ForMember(dest => dest.DisplayName,
                        opts => opts.MapFrom(src => src.Person.GetDisplayName()));

                cfg.CreateMap<TrainingCourseDto, DataGridTrainingCourseDto>();

                cfg.CreateMap<BasketItemDto, DataGridBasketItemDto>()
                    .ForMember(dest => dest.ProductDescription,
                        opts => opts.MapFrom(src => src.Product.Description))
                    .ForMember(dest => dest.StudentName,
                        opts => opts.MapFrom(src => src.Student.Person.GetDisplayName()));
                cfg.CreateMap<CommentDto, DataGridCommentDto>()
                    .ForMember(dest => dest.CommentBankName,
                        opts => opts.MapFrom(src => src.CommentBank.Name));

                cfg.CreateMap<CommentBankDto, DataGridCommentBankDto>();

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
                        opts => opts.MapFrom(src => src.Aspect.Description))
                    .ForMember(dest => dest.Grade,
                        opts => opts.MapFrom(src => src.Grade.Code));

                cfg.CreateMap<PersonConditionDto, DataGridPersonConditionDto>()
                    .ForMember(dest => dest.Condition,
                        opts => opts.MapFrom(src => src.MedicalCondition.Description));

                cfg.CreateMap<PersonDietaryRequirementDto, DataGridPersonDietaryRequirementDto>()
                    .ForMember(dest => dest.Description,
                        opts => opts.MapFrom(src => src.DietaryRequirement.Description));

                cfg.CreateMap<SubjectStaffMemberDto, DataGridSubjectStaffMemberDto>()
                    .ForMember(dest => dest.StaffMemberName,
                        opts => opts.MapFrom(src => src.StaffMember.Person.GetDisplayName()))
                    .ForMember(dest => dest.Role,
                        opts => opts.MapFrom(src => src.Role.Description));

                cfg.CreateMap<PhoneNumberDto, DataGridPhoneNumberDto>()
                    .ForMember(dest => dest.Type,
                        opts => opts.MapFrom(src => src.Type.Description));

                cfg.CreateMap<EmailAddressDto, DataGridEmailAddressDto>()
                    .ForMember(dest => dest.Type,
                        opts => opts.MapFrom(src => src.Type.Description));

                cfg.CreateMap<ApplicationUser, DataGridApplicationUserDto>();
            });

            return new Mapper(config);
        }
    }
}
