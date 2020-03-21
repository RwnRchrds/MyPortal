using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<AchievementType> AchievementTypes { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressPerson> AddressPersons { get; set; }
        public virtual DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public virtual DbSet<ApplicationRolePermission> RolePermissions { get; set; }
        public virtual DbSet<Aspect> Aspects { get; set; }
        public virtual DbSet<AspectType> AspectTypes { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceCodeMeaning> AttendanceCodeMeanings { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentBank> CommentBanks { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CurriculumBand> CurriculumBands { get; set; }
        public virtual DbSet<CurriculumYearGroup> CurriculumYearGroups { get; set; }
        public virtual DbSet<Detention> Detentions { get; set; }
        public virtual DbSet<DetentionType> DetentionTypes { get; set; }
        public virtual DbSet<DiaryEvent> DiaryEvents { get; set; }
        public virtual DbSet<DiaryEventAttendee> DiaryEventAttendees { get; set; }
        public virtual DbSet<DiaryEventAttendeeResponse> DiaryEventInvitationResponses { get; set; }
        public virtual DbSet<DiaryEventType> DiaryEventTypes { get; set; }
        public virtual DbSet<DiaryEventTemplate> DiaryEventTemplates { get; set; }
        public virtual DbSet<DietaryRequirement> DietaryRequirements { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<EmailAddressType> EmailAddressTypes { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }
        public virtual DbSet<GiftedTalented> GiftedTalented { get; set; }
        public virtual DbSet<GovernanceType> GovernanceTypes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSet> GradeSets { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<HomeworkAttachment> HomeworkAttachments { get; set; }
        public virtual DbSet<HomeworkSubmission> HomeworkSubmissions { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<IncidentDetention> IncidentDetentions { get; set; }
        public virtual DbSet<IncidentType> IncidentTypes { get; set; }
        public virtual DbSet<IntakeType> IntakeTypes { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPlanTemplate> LessonPlanTemplates { get; set; }
        public virtual DbSet<LocalAuthority> LocalAuthorities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Marksheet> Marksheets { get; set; }
        public virtual DbSet<MarksheetColumn> MarksheetColumns { get; set; }
        public virtual DbSet<MedicalCondition> Conditions { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<ObservationOutcome> ObservationOutcomes { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonAttachment> PersonAttachments { get; set; }
        public virtual DbSet<PersonCondition> PersonConditions { get; set; }
        public virtual DbSet<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<ProfileLogNote> ProfileLogNotes { get; set; }
        public virtual DbSet<ProfileLogNoteType> ProfileLogNoteTypes { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SenEvent> SenEvents { get; set; }
        public virtual DbSet<SenProvision> SenProvisions { get; set; }
        public virtual DbSet<SenProvisionType> SenProvisionTypes { get; set; }
        public virtual DbSet<SenReview> SenReviews { get; set; }
        public virtual DbSet<SenReviewType> SenReviewTypes { get; set; }
        public virtual DbSet<SenStatus> SenStatuses { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentContact> StudentContacts { get; set; }
        public virtual DbSet<StudyTopic> StudyTopics { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectStaffMember> SubjectStaffMembers { get; set; }
        public virtual DbSet<SubjectStaffMemberRole> SubjectStaffMemberRoles { get; set; }
        public virtual DbSet<SystemArea> SystemAreas { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCertificates { get; set; }
        public virtual DbSet<TrainingCertificateStatus> TrainingCertificateStatus { get; set; }
        public virtual DbSet<TrainingCourse> TrainingCourses { get; set; }
        public virtual DbSet<YearGroup> YearGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYear>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Achievement>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AchievementType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Address>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AddressPerson>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ApplicationPermission>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ApplicationRolePermission>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Aspect>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AspectType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AttendanceCode>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AttendanceCodeMeaning>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AttendanceMark>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<AttendanceWeek>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<BasketItem>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Bulletin>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Class>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Comment>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<CommentBank>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<CommunicationLog>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<CommunicationType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Contact>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<CurriculumBand>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<CurriculumYearGroup>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Detention>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DetentionType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DiaryEvent>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DiaryEventAttendee>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DiaryEventAttendeeResponse>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DiaryEventTemplate>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DiaryEventType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DietaryRequirement>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Document>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<EmailAddress>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<EmailAddressType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Enrolment>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<GiftedTalented>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<GovernanceType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Grade>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<GradeSet>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Homework>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<HomeworkAttachment>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<HomeworkSubmission>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<House>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Incident>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<IncidentDetention>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<IncidentType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<IntakeType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<LessonPlan>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<LessonPlanTemplate>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<LocalAuthority>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Location>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Marksheet>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            
            modelBuilder.Entity<MarksheetColumn>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<MedicalCondition>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<MedicalEvent>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Observation>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ObservationOutcome>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Period>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Person>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<PersonAttachment>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<PersonCondition>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<PersonDietaryRequirement>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Phase>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<PhoneNumber>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<PhoneNumberType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Product>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ProductType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ProfileLogNote>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ProfileLogNoteType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<RegGroup>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<RelationshipType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Report>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Result>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<ResultSet>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Sale>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<School>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SchoolType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenEvent>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenEventType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenProvision>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenProvisionType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenReview>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenReviewType>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SenStatus>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Session>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Student>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<StudentContact>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<StudyTopic>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Subject>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SubjectStaffMember>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SubjectStaffMemberRole>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<SystemArea>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Task>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<TrainingCertificate>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<TrainingCertificateStatus>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<TrainingCourse>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<YearGroup>()
                .Property(e => e.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            modelBuilder.Entity<Aspect>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Aspect)
                .HasForeignKey(e => e.AspectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AspectType>()
                .HasMany(e => e.Aspects)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Aspects)
                .WithOne(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Grades)
                .WithOne(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ResultSet>()
                .HasMany(e => e.Results)
                .WithOne(e => e.ResultSet)
                .HasForeignKey(e => e.ResultSetId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AttendanceCodeMeaning>()
                .HasMany(e => e.Codes)
                .WithOne(e => e.CodeMeaning)
                .HasForeignKey(e => e.MeaningId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Period>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Period>()
                .HasMany(e => e.Sessions)
                .WithOne(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AttendanceWeek>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Week)
                .HasForeignKey(e => e.WeekId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AchievementType>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.AchievementTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IncidentType>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.BehaviourTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.People)
                .WithOne(e => e.Address)
                .HasForeignKey(e => e.AddressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmailAddressType>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PhoneNumberType>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommunicationType>()
                .HasMany(e => e.CommunicationLogs)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.CommunicationTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.Contact)
                .HasForeignKey(e => e.ContactId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contact>()
                .HasOne(e => e.Person)
                .WithOne(e => e.ContactDetails)
                .HasForeignKey<Contact>(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.AttendanceWeeks)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Logs)
                .WithOne(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Users)
                .WithOne(e => e.SelectedAcademicYear)
                .HasForeignKey(e => e.SelectedAcademicYearId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Sessions)
                .WithOne(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Detention>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Detention)
                .HasForeignKey(e => e.DetentionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DetentionType>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.DetentionTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiaryEvent>()
                .HasOne(e => e.Detention)
                .WithOne(e => e.Event)
                .HasForeignKey<Detention>(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiaryEvent>()
                .HasMany(e => e.Attendees)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiaryEventType>()
                .HasMany(e => e.DiaryEventTemplates)
                .WithOne(e => e.DiaryEventType)
                .HasForeignKey(e => e.EventTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiaryEventType>()
                .HasMany(e => e.DiaryEvents)
                .WithOne(e => e.EventType)
                .HasForeignKey(e => e.EventTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DiaryEventAttendeeResponse>()
                .HasMany(e => e.DiaryEventAttendees)
                .WithOne(e => e.Response)
                .HasForeignKey(e => e.ResponseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incident>()
                .HasMany(e => e.Detentions)
                .WithOne(e => e.Incident)
                .HasForeignKey(e => e.IncidentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudyTopic>()
                .HasMany(e => e.LessonPlans)
                .WithOne(e => e.StudyTopic)
                .HasForeignKey(e => e.StudyTopicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StaffMembers)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StudyTopics)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.GiftedTalentedStudents)
                .WithOne(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubjectStaffMemberRole>()
                .HasMany(e => e.StaffMembers)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(e => e.PersonAttachment)
                .WithOne(e => e.Document)
                .HasForeignKey<PersonAttachment>(e => e.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasOne(e => e.HomeworkAttachment)
                .WithOne(e => e.Document)
                .HasForeignKey<HomeworkAttachment>(e => e.DocumentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Documents)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BasketItems)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductType>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.ProductTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LocalAuthority>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.LocalAuthority)
                .HasForeignKey(e => e.LocalAuthorityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalCondition>()
                .HasMany(e => e.PersonConditions)
                .WithOne(e => e.MedicalCondition)
                .HasForeignKey(e => e.ConditionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DietaryRequirement>()
                .HasMany(e => e.PersonDietaryRequirements)
                .WithOne(e => e.DietaryRequirement)
                .HasForeignKey(e => e.DietaryRequirementId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<House>()
                .HasMany(e => e.Students)
                .WithOne(e => e.House)
                .HasForeignKey(e => e.HouseId);

            modelBuilder.Entity<RegGroup>()
                .HasMany(e => e.Students)
                .WithOne(e => e.RegGroup)
                .HasForeignKey(e => e.RegGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.StudyTopics)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.RegGroups)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Students)
                .WithOne(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Addresses)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.AddressId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.EmailAddresses)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.MedicalConditions)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PhoneNumbers)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Attachments)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ObservationOutcome>()
                .HasMany(e => e.Observations)
                .WithOne(e => e.Outcome)
                .HasForeignKey(e => e.OutcomeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainingCertificateStatus>()
                .HasMany(e => e.Certificates)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TrainingCourse>()
                .HasMany(e => e.Certificates)
                .WithOne(e => e.TrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentBank>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.CommentBank)
                .HasForeignKey(e => e.CommentBankId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProfileLogNoteType>()
                .HasMany(e => e.Logs)
                .WithOne(e => e.ProfileLogNoteType)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GovernanceType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.GovernanceType)
                .HasForeignKey(e => e.GovernanceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Grade)
                .HasForeignKey(e => e.GradeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IntakeType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.IntakeType)
                .HasForeignKey(e => e.IntakeTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourAchievements)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourIncidents)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marksheet>()
                .HasMany(e => e.Columns)
                .WithOne(e => e.Marksheet)
                .HasForeignKey(e => e.MarksheetId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Phase>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.Phase)
                .HasForeignKey(e => e.PhaseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SchoolType>()
                .HasMany(e => e.Schools)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SenEventType>()
                .HasMany(e => e.Events)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.EventTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SenProvisionType>()
                .HasMany(e => e.SenProvisions)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.ProvisionTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SenReviewType>()
                .HasMany(e => e.Reviews)
                .WithOne(e => e.ReviewType)
                .HasForeignKey(e => e.ReviewTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SenStatus>()
                .HasMany(x => x.Students)
                .WithOne(x => x.SenStatus)
                .HasForeignKey(x => x.SenStatusId);

            modelBuilder.Entity<SenStatus>()
                .Property(x => x.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Bulletins)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Sessions)
                .WithOne(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.LessonPlans)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Documents)
                .WithOne(e => e.Uploader)
                .HasForeignKey(e => e.UploaderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasOne(e => e.Person)
                .WithOne(e => e.StaffMemberDetails)
                .HasForeignKey<StaffMember>(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralHouses)
                .WithOne(e => e.HeadOfHouse)
                .HasForeignKey(e => e.HeadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralRegGroups)
                .WithOne(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralYearGroups)
                .WithOne(e => e.HeadOfYear)
                .HasForeignKey(e => e.HeadId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservationsObserved)
                .WithOne(e => e.Observer)
                .HasForeignKey(e => e.ObserverId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservations)
                .WithOne(e => e.Observee)
                .HasForeignKey(e => e.ObserveeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithOne(e => e.StaffMember)
                .HasForeignKey(e => e.StaffId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.ProfileLogNotes)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Subjects)
                .WithOne(e => e.StaffMember)
                .HasForeignKey(e => e.StaffMemberId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AttendanceMarks)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Achievements)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Incidents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Enrolments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinanceBasketItems)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Sales)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.GiftedTalentedSubjects)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Person)
                .WithOne(e => e.StudentDetails)
                .HasForeignKey<Student>(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.MedicalEvents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ProfileLogs)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenEvents)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenProvisions)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .Property(e => e.Upn)
                .IsUnicode(false);

            modelBuilder.Entity<SystemArea>()
                .HasMany(e => e.Reports)
                .WithOne(e => e.SystemArea)
                .HasForeignKey(e => e.AreaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemArea>()
                .HasMany(e => e.SubAreas)
                .WithOne(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelationshipType>()
                .HasMany(e => e.StudentContacts)
                .WithOne(e => e.RelationshipType)
                .HasForeignKey(e => e.RelationshipTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(e => e.Person)
                .WithOne(e => e.User)
                .HasForeignKey<Person>(e => e.UserId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.DiaryEventInvitations)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Tasks)
                .WithOne(e => e.Person)
                .HasForeignKey(e => e.AssignedToId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Homework>()
                .HasMany(e => e.Attachments)
                .WithOne(e => e.Homework)
                .HasForeignKey(e => e.HomeworkId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Homework>()
                .HasMany(e => e.Submissions)
                .WithOne(e => e.Homework)
                .HasForeignKey(e => e.HomeworkId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HomeworkSubmission>()
                .HasOne(e => e.Task)
                .WithOne(e => e.HomeworkSubmission)
                .HasForeignKey<HomeworkSubmission>(e => e.TaskId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HomeworkSubmission>()
                .HasOne(e => e.Student)
                .WithMany(e => e.HomeworkSubmissions)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationPermission>()
                .HasOne(e => e.Area)
                .WithMany(e => e.Permissions)
                .HasForeignKey(e => e.AreaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationPermission>()
                .HasMany(e => e.RolePermissions)
                .WithOne(e => e.Permission)
                .HasForeignKey(e => e.PermissionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationRole>()
                .HasMany(e => e.RolePermissions)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurriculumBand>()
                .HasMany(e => e.Enrolments)
                .WithOne(e => e.Band)
                .HasForeignKey(e => e.BandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurriculumBand>()
                .HasMany(e => e.Classes)
                .WithOne(e => e.Band)
                .HasForeignKey(e => e.BandId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CurriculumYearGroup>()
                .HasMany(e => e.YearGroups)
                .WithOne(e => e.CurriculumYearGroup)
                .HasForeignKey(e => e.CurriculumYearGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
