using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Aspect> Aspects { get; set; }
        public virtual DbSet<AspectType> AspectTypes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSet> GradeSets { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceCodeMeaning> AttendanceCodeMeanings { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<AchievementType> AchievementTypes { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<IncidentType> IncidentTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressPerson> AddressPersons { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<EmailAddressType> EmailAddressTypes { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<DetentionType> DetentionTypes { get; set; }
        public virtual DbSet<Detention> Detentions { get; set; }
        public virtual DbSet<DetentionAttendanceStatus> DetentionAttendanceStatus { get; set; }
        public virtual DbSet<DiaryEvent> DiaryEvents { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }
        public virtual DbSet<IncidentDetention> IncidentDetentions { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPlanTemplate> LessonPlanTemplates { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<StudyTopic> StudyTopics { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectStaffMember> SubjectStaffMembers { get; set; }
        public virtual DbSet<SubjectStaffMemberRole> SubjectStaffMemberRoles { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<LocalAuthority> LocalAuthorities { get; set; }
        public virtual DbSet<MedicalCondition> Conditions { get; set; }
        public virtual DbSet<DietaryRequirement> DietaryRequirements { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<PersonCondition> PersonConditions { get; set; }
        public virtual DbSet<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<YearGroup> YearGroups { get; set; }
        public virtual DbSet<PersonAttachment> PersonAttachments { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<ObservationOutcome> ObservationOutcomes { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCertificates { get; set; }
        public virtual DbSet<TrainingCertificateStatus> TrainingCertificateStatus { get; set; }
        public virtual DbSet<TrainingCourse> TrainingCourses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<CommentBank> CommentBanks { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ProfileLogNote> ProfileLogNotes { get; set; }
        public virtual DbSet<ProfileLogNoteType> ProfileLogNoteTypes { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<GovernanceType> GovernanceTypes { get; set; }
        public virtual DbSet<IntakeType> IntakeTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SenEvent> SenEvents { get; set; }
        public virtual DbSet<GiftedTalented> GiftedTalented { get; set; }
        public virtual DbSet<SenProvision> SenProvisions { get; set; }
        public virtual DbSet<SenProvisionType> SenProvisionTypes { get; set; }
        public virtual DbSet<SenReviewType> SenReviewTypes { get; set; }
        public virtual DbSet<SenStatus> SenStatuses { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<StudentContact> StudentContacts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<SystemArea> SystemAreas { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aspect>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Aspect)
                .HasForeignKey(e => e.AspectId)
                .IsRequired();

            modelBuilder.Entity<AspectType>()
                .HasMany(e => e.Aspects)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Aspects)
                .WithOne(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .IsRequired();

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Grades)
                .WithOne(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .IsRequired();

            modelBuilder.Entity<ResultSet>()
                .HasMany(e => e.Results)
                .WithOne(e => e.ResultSet)
                .HasForeignKey(e => e.ResultSetId)
                .IsRequired();

            modelBuilder.Entity<AttendanceCodeMeaning>()
                .HasMany(e => e.Codes)
                .WithOne(e => e.CodeMeaning)
                .HasForeignKey(e => e.MeaningId)
                .IsRequired();

            modelBuilder.Entity<Period>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .IsRequired();

            modelBuilder.Entity<Period>()
                .HasMany(e => e.Sessions)
                .WithOne(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .IsRequired();

            modelBuilder.Entity<AttendanceWeek>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Week)
                .HasForeignKey(e => e.WeekId)
                .IsRequired();

            modelBuilder.Entity<AchievementType>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.AchievementTypeId)
                .IsRequired();

            modelBuilder.Entity<IncidentType>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.BehaviourTypeId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .HasMany(e => e.People)
                .WithOne(e => e.Address)
                .HasForeignKey(e => e.AddressId)
                .IsRequired();

            modelBuilder.Entity<EmailAddressType>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<PhoneNumberType>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<CommunicationType>()
                .HasMany(e => e.CommunicationLogs)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.CommunicationTypeId)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.Contact)
                .HasForeignKey(e => e.ContactId)
                .IsRequired();

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.Person)
                .WithOne(e => e.ContactDetails)
                .HasForeignKey<Contact>(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.AttendanceWeeks)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Logs)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired();

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Sessions)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .IsRequired();

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Enrolments)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .IsRequired();

            modelBuilder.Entity<Detention>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Detention)
                .HasForeignKey(e => e.DetentionId)
                .IsRequired();

            modelBuilder.Entity<DetentionType>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.DetentionTypeId)
                .IsRequired();

            modelBuilder.Entity<DetentionAttendanceStatus>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.AttendanceStatus)
                .HasForeignKey(e => e.AttendanceStatusId)
                .IsRequired();

            modelBuilder.Entity<DiaryEvent>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

            modelBuilder.Entity<Incident>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.Incident)
                .HasForeignKey(e => e.IncidentId)
                .IsRequired();

            modelBuilder.Entity<StudyTopic>()
                .HasMany(e => e.LessonPlans)
                .WithOne(e => e.StudyTopic)
                .HasForeignKey(e => e.StudyTopicId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StaffMembers)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StudyTopics)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.GiftedTalentedStudents)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired();

            modelBuilder.Entity<SubjectStaffMemberRole>()
                .HasMany(e => e.StaffMembers)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<Document>()
                .HasMany(e => e.PersonDocuments)
                .WithOne(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .IsRequired();

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Documents)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BasketItems)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .IsRequired();

            modelBuilder.Entity<ProductType>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.ProductTypeId)
                .IsRequired();

            modelBuilder.Entity<LocalAuthority>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.LocalAuthority)
                .HasForeignKey(e => e.LocalAuthorityId)
                .IsRequired();

            modelBuilder.Entity<MedicalCondition>()
                .HasMany(e => e.PersonConditions)
                .WithOne(e => e.MedicalCondition)
                .HasForeignKey(e => e.ConditionId)
                .IsRequired();

            modelBuilder.Entity<DietaryRequirement>()
                .HasMany(e => e.PersonDietaryRequirements)
                .WithOne(e => e.DietaryRequirement)
                .HasForeignKey(e => e.DietaryRequirementId)
                .IsRequired();

            modelBuilder.Entity<House>()
                .HasMany(e => e.Students)
                .WithOne(e => e.House)
                .HasForeignKey(e => e.HouseId);

            modelBuilder.Entity<RegGroup>()
                .HasMany(e => e.Students)
                .WithOne(e => e.RegGroup)
                .HasForeignKey(e => e.RegGroupId)
                .IsRequired();

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.StudyTopics)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired();

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.RegGroups)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired();

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Students)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Addresses)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.AddressId)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.MedicalConditions)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<Person>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonalDocuments)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<ObservationOutcome>()
                .HasMany(e => e.Observations)
                .WithOne(e => e.Outcome)
                .HasForeignKey(e => e.OutcomeId)
                .IsRequired();

            modelBuilder.Entity<TrainingCertificateStatus>()
                .HasMany(e => e.Certificates)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .IsRequired();

            modelBuilder.Entity<TrainingCourse>()
                .HasMany(e => e.Certificates)
                .WithOne(e => e.TrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();

            modelBuilder.Entity<CommentBank>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.CommentBank)
                .HasForeignKey(e => e.CommentBankId)
                .IsRequired();

            modelBuilder.Entity<ProfileLogNoteType>()
                .HasMany(e => e.Logs)
                .WithOne(e => e.ProfileLogNoteType)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<GovernanceType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.GovernanceType)
                .HasForeignKey(e => e.GovernanceTypeId)
                .IsRequired();

            modelBuilder.Entity<Grade>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Grade)
                .HasForeignKey(e => e.GradeId)
                .IsRequired();

            modelBuilder.Entity<IntakeType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.IntakeType)
                .HasForeignKey(e => e.IntakeTypeId)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourAchievements)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .IsRequired();

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourIncidents)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .IsRequired();

            modelBuilder.Entity<Phase>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.Phase)
                .HasForeignKey(e => e.PhaseId)
                .IsRequired();

            modelBuilder.Entity<SchoolType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired();

            modelBuilder.Entity<SenEventType>()
                .HasMany(e => e.Events)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.EventTypeId)
                .IsRequired();

            modelBuilder.Entity<SenProvisionType>()
                .HasMany(e => e.SenProvisions)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.ProvisionTypeId)
                .IsRequired();

            modelBuilder.Entity<SenStatus>()
                .HasMany(x => x.Students)
                .WithOne(x => x.SenStatus)
                .HasForeignKey(x => x.SenStatusId);

            modelBuilder.Entity<SenStatus>()
                .Property(x => x.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.BehaviourAchievements)
                .WithOne(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.BehaviourIncidents)
                .WithOne(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Bulletins)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumClasses)
                .WithOne(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumLessonPlans)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Documents)
                .WithOne(e => e.Uploader)
                .HasForeignKey(e => e.UploaderId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasOne(e => e.Person)
                .WithOne(e => e.StaffMemberDetails)
                .HasForeignKey<StaffMember>(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralHouses)
                .WithOne(e => e.HeadOfHouse)
                .HasForeignKey(e => e.HeadId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralRegGroups)
                .WithOne(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralYearGroups)
                .WithOne(e => e.HeadOfYear)
                .HasForeignKey(e => e.HeadId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservationsObserved)
                .WithOne(e => e.Observer)
                .HasForeignKey(e => e.ObserverId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservations)
                .WithOne(e => e.Observee)
                .HasForeignKey(e => e.ObserveeId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithOne(e => e.StaffMember)
                .HasForeignKey(e => e.StaffId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.ProfileLogs)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Subjects)
                .WithOne(e => e.StaffMember)
                .HasForeignKey(e => e.StaffMemberId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Enrolments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinanceBasketItems)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.GiftedTalentedSubjects)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Person)
                .WithOne(e => e.StudentDetails)
                .HasForeignKey<Student>(e => e.PersonId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.MedicalEvents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ProfileLogs)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenEvents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenProvisions)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(e => e.Upn)
                .IsUnicode(false);

            modelBuilder.Entity<SystemArea>()
                .HasMany(e => e.Reports)
                .WithOne(e => e.SystemArea)
                .HasForeignKey(e => e.AreaId)
                .IsRequired();

            modelBuilder.Entity<RelationshipType>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.RelationshipType)
                .HasForeignKey(e => e.RelationshipTypeId)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.Person)
                .WithOne(e => e.User)
                .HasForeignKey<Person>(e => e.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
