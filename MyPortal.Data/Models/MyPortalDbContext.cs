using System.Data.Common;
using System.Data.Entity;

namespace MyPortal.Data.Models
{
    public class MyPortalDbContext : DbContext
    {
        public MyPortalDbContext()
            : base("name=MyPortalDbContext")
        {
            IsDebug = false;
        }

        public MyPortalDbContext(DbConnection connection) : base(connection, true)
        {
            IsDebug = false;
        }

        public bool IsDebug { get; set; }

        public virtual DbSet<Aspect> Aspects { get; set; }
        public virtual DbSet<AspectType> AspectTypes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSet> GradeSets { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
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
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Enrolment> Enrolments { get; set; }
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
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<DietaryRequirement> DietaryRequirements { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<PersonCondition> PersonConditions { get; set; }
        public virtual DbSet<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<YearGroup> YearGroups { get; set; }
        public virtual DbSet<PersonAttachment> PersonAttachments { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCertificates { get; set; }
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
        public virtual DbSet<School> SystemSchools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aspect>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Aspect)
                .HasForeignKey(e => e.AspectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspectType>()
                .HasMany(e => e.Aspects)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Aspects)
                .WithRequired(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ResultSet>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.ResultSet)
                .HasForeignKey(e => e.ResultSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendanceCode>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceMark>()
                .Property(e => e.Mark)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Period>()
                .Property(e => e.StartTime)
                .HasPrecision(2);

            modelBuilder.Entity<Period>()
                .Property(e => e.EndTime)
                .HasPrecision(2);

            modelBuilder.Entity<Period>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Period>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendanceWeek>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.Week)
                .HasForeignKey(e => e.WeekId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AchievementType>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.AchievementTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncidentType>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.BehaviourTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.People)
                .WithRequired(e => e.Address)
                .HasForeignKey(e => e.AddressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumberType>()
                .HasMany(e => e.PhoneNumbers)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommunicationType>()
                .HasMany(e => e.CommunicationLogs)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.CommunicationTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.StudentContacts)
                .WithRequired(e => e.Contact)
                .HasForeignKey(e => e.ContactId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.AttendanceWeeks)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Enrolments)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudyTopic>()
                .HasMany(e => e.LessonPlans)
                .WithRequired(e => e.StudyTopic)
                .HasForeignKey(e => e.StudyTopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Classes)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StaffMembers)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.StudyTopics)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.GiftedTalentedStudents)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubjectStaffMemberRole>()
                .HasMany(e => e.StaffMembers)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.PersonDocuments)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BasketItems)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<ProductType>()
                .HasMany(x => x.Products)
                .WithRequired(x => x.Type)
                .HasForeignKey(x => x.ProductTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.AmountPaid)
                .HasPrecision(10, 2);

            modelBuilder.Entity<LocalAuthority>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.LocalAuthority)
                .HasForeignKey(e => e.LocalAuthorityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Condition>()
                .HasMany(e => e.PersonConditions)
                .WithRequired(e => e.Condition)
                .HasForeignKey(e => e.ConditionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DietaryRequirement>()
                .HasMany(e => e.PersonDietaryRequirements)
                .WithRequired(e => e.DietaryRequirement)
                .HasForeignKey(e => e.DietaryRequirementId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<House>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.House)
                .HasForeignKey(e => e.HouseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.RegGroup)
                .HasForeignKey(e => e.RegGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Classes)
                .WithOptional(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.StudyTopics)
                .WithRequired(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.RegGroups)
                .WithRequired(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.AddressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.ContactDetails)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.EmailAddresses)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.MedicalConditions)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PhoneNumbers)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.StaffMemberDetails)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.StudentDetails)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonalDocuments)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.PersonId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrainingCourse>()
                .HasMany(e => e.Certificates)
                .WithRequired(e => e.TrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommentBank>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.CommentBank)
                .HasForeignKey(e => e.CommentBankId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfileLogNoteType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.ProfileLogNoteType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GovernanceType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.GovernanceType)
                .HasForeignKey(e => e.GovernanceTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntakeType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.IntakeType)
                .HasForeignKey(e => e.IntakeTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourAchievements)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.BehaviourIncidents)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phase>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.Phase)
                .HasForeignKey(e => e.PhaseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SenEventType>()
                .HasMany(e => e.Events)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.EventTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SenProvisionType>()
                .HasMany(e => e.SenProvisions)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.ProvisionTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SenStatus>()
                .HasMany(x => x.Students)
                .WithOptional(x => x.SenStatus)
                .HasForeignKey(x => x.SenStatusId);

            modelBuilder.Entity<SenStatus>()
                .Property(x => x.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.BehaviourAchievements)
                .WithRequired(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.BehaviourIncidents)
                .WithRequired(e => e.RecordedBy)
                .HasForeignKey(e => e.RecordedById)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Bulletins)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumLessonPlans)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Uploader)
                .HasForeignKey(e => e.UploaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralHouses)
                .WithOptional(e => e.HeadOfHouse)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralRegGroups)
                .WithRequired(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralYearGroups)
                .WithRequired(e => e.HeadOfYear)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservationsObserved)
                .WithRequired(e => e.Observer)
                .HasForeignKey(e => e.ObserverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservations)
                .WithRequired(e => e.Observee)
                .HasForeignKey(e => e.ObserveeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithRequired(e => e.StaffMember)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.StaffMember)
                .HasForeignKey(e => e.StaffMemberId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Enrolments)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinanceBasketItems)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentContacts)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.GiftedTalentedSubjects)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.MedicalEvents)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenEvents)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SenProvisions)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.AccountBalance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Student>()
                .Property(e => e.Upn)
                .IsUnicode(false);

            modelBuilder.Entity<SystemArea>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.SystemArea)
                .HasForeignKey(e => e.AreaId)
                .WillCascadeOnDelete(false);
        }
    }
}