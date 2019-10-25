using System.Data.Common;
using System.Data.Entity;
using Antlr.Runtime.Tree;

namespace MyPortal.Models.Database
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

        public virtual DbSet<AssessmentAspect> AssessmentAspects { get; set; }
        public virtual DbSet<AssessmentAspectType> AssessmentAspectTypes { get; set; }
        public virtual DbSet<AssessmentGrade> AssessmentGrades { get; set; }
        public virtual DbSet<AssessmentGradeSet> AssessmentGradeSets { get; set; }
        public virtual DbSet<AssessmentResult> AssessmentResults { get; set; }
        public virtual DbSet<AssessmentResultSet> AssessmentResultSets { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendanceMeaning> AttendanceMeanings { get; set; }
        public virtual DbSet<AttendancePeriod> AttendancePeriods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<BehaviourAchievement> BehaviourAchievements { get; set; }
        public virtual DbSet<BehaviourAchievementType> BehaviourAchievementTypes { get; set; }
        public virtual DbSet<BehaviourIncident> BehaviourIncidents { get; set; }
        public virtual DbSet<BehaviourIncidentType> BehaviourIncidentTypes { get; set; }
        public virtual DbSet<CommunicationAddress> CommunicationAddresses { get; set; }
        public virtual DbSet<CommunicationAddressPerson> CommunicationAddressPersons { get; set; }
        public virtual DbSet<CommunicationEmailAddress> CommunicationEmailAddresses { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<CommunicationPhoneNumber> CommunicationPhoneNumbers { get; set; }
        public virtual DbSet<CommunicationPhoneNumberType> CommunicationPhoneNumberTypes { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<CurriculumAcademicYear> CurriculumAcademicYears { get; set; }
        public virtual DbSet<CurriculumClass> CurriculumClasses { get; set; }
        public virtual DbSet<CurriculumEnrolment> CurriculumEnrolments { get; set; }
        public virtual DbSet<CurriculumLessonPlan> CurriculumLessonPlans { get; set; }
        public virtual DbSet<CurriculumLessonPlanTemplate> CurriculumLessonPlanTemplates { get; set; }
        public virtual DbSet<CurriculumSession> CurriculumSessions { get; set; }
        public virtual DbSet<CurriculumStudyTopic> CurriculumStudyTopics { get; set; }
        public virtual DbSet<CurriculumSubject> CurriculumSubjects { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<FinanceBasketItem> FinanceBasketItems { get; set; }
        public virtual DbSet<FinanceProduct> FinanceProducts { get; set; }
        public virtual DbSet<FinanceProductType> FinanceProductTypes { get; set; }
        public virtual DbSet<FinanceSale> FinanceSales { get; set; }
        public virtual DbSet<MedicalCondition> MedicalConditions { get; set; }
        public virtual DbSet<MedicalDietaryRequirement> MedicalDietaryRequirements { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<MedicalPersonCondition> MedicalPersonConditions { get; set; }
        public virtual DbSet<MedicalPersonDietaryRequirement> MedicalPersonDietaryRequirements { get; set; }
        public virtual DbSet<PastoralHouse> PastoralHouses { get; set; }
        public virtual DbSet<PastoralRegGroup> PastoralRegGroups { get; set; }
        public virtual DbSet<PastoralYearGroup> PastoralYearGroups { get; set; }
        public virtual DbSet<PersonDocument> PersonDocuments { get; set; }
        public virtual DbSet<PersonnelObservation> PersonnelObservations { get; set; }
        public virtual DbSet<PersonnelTrainingCertificate> PersonnelTrainingCertificates { get; set; }
        public virtual DbSet<PersonnelTrainingCourse> PersonnelTrainingCourses { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<ProfileCommentBank> ProfileCommentBanks { get; set; }
        public virtual DbSet<ProfileComment> ProfileComments { get; set; }
        public virtual DbSet<ProfileLog> ProfileLogs { get; set; }
        public virtual DbSet<ProfileLogType> ProfileLogTypes { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<SchoolGovernanceType> SchoolGovernanceTypes { get; set; }
        public virtual DbSet<SchoolIntakeType> SchoolIntakeTypes { get; set; }
        public virtual DbSet<SchoolLocation> SchoolLocations { get; set; }
        public virtual DbSet<SchoolPhase> SchoolPhases { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SenEvent> SenEvents { get; set; }
        public virtual DbSet<SenGiftedTalented> SenGiftedTalented { get; set; }
        public virtual DbSet<SenProvision> SenProvisions { get; set; }
        public virtual DbSet<SenProvisionType> SenProvisionTypes { get; set; }
        public virtual DbSet<SenReviewType> SenReviewTypes { get; set; }
        public virtual DbSet<SenStatus> SenStatuses { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<SystemArea> SystemAreas { get; set; }
        public virtual DbSet<SystemBulletin> SystemBulletins { get; set; }
        public virtual DbSet<SystemReport> SystemReports { get; set; }
        public virtual DbSet<SystemSchool> SystemSchools { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssessmentAspect>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Aspect)
                .HasForeignKey(e => e.AspectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssessmentAspectType>()
                .HasMany(e => e.Aspects)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssessmentGradeSet>()
                .HasMany(e => e.Aspects)
                .WithRequired(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssessmentGradeSet>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.GradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssessmentResultSet>()
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

            modelBuilder.Entity<AttendanceMeaning>()
                .HasMany(e => e.AttendanceCodes)
                .WithRequired(e => e.Meaning)
                .HasForeignKey(e => e.MeaningId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendancePeriod>()
                .Property(e => e.StartTime)
                .HasPrecision(2);

            modelBuilder.Entity<AttendancePeriod>()
                .Property(e => e.EndTime)
                .HasPrecision(2);

            modelBuilder.Entity<AttendancePeriod>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendancePeriod>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Period)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendanceWeek>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.Week)
                .HasForeignKey(e => e.WeekId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BehaviourAchievementType>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.AchievementTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BehaviourIncidentType>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.BehaviourTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommunicationAddress>()
                .HasMany(e => e.People)
                .WithRequired(e => e.Address)
                .HasForeignKey(e => e.AddressId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommunicationPhoneNumberType>()
                .HasMany(e => e.PhoneNumbers)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommunicationType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.CommunicationTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.Achievements)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.AttendanceWeeks)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.Incidents)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.AcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumClass>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumClass>()
                .HasMany(e => e.Enrolments)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumStudyTopic>()
                .HasMany(e => e.LessonPlans)
                .WithRequired(e => e.StudyTopic)
                .HasForeignKey(e => e.StudyTopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.Classes)
                .WithOptional(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.StudyTopics)
                .WithRequired(e => e.CurriculumSubject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.GiftedTalentedStudents)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SubjectId)
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

            modelBuilder.Entity<FinanceProduct>()
                .HasMany(e => e.BasketItems)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceProduct>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceProduct>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FinanceProductType>()
                .HasMany(x => x.Products)
                .WithRequired(x => x.Type)
                .HasForeignKey(x => x.ProductTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceSale>()
                .Property(e => e.AmountPaid)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MedicalCondition>()
                .HasMany(e => e.PersonConditions)
                .WithRequired(e => e.Condition)
                .HasForeignKey(e => e.ConditionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalDietaryRequirement>()
                .HasMany(e => e.PersonDietaryRequirements)
                .WithRequired(e => e.DietaryRequirement)
                .HasForeignKey(e => e.DietaryRequirementId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralHouse>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.House)
                .HasForeignKey(e => e.HouseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralRegGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.PastoralRegGroup)
                .HasForeignKey(e => e.RegGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.Classes)
                .WithOptional(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.StudyTopics)
                .WithRequired(e => e.PastoralYearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.RegGroups)
                .WithRequired(e => e.YearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.PastoralYearGroup)
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

            modelBuilder.Entity<PersonnelTrainingCourse>()
                .HasMany(e => e.Certificates)
                .WithRequired(e => e.TrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfileCommentBank>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.CommentBank)
                .HasForeignKey(e => e.CommentBankId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfileLogType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolGovernanceType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.GovernanceType)
                .HasForeignKey(e => e.GovernanceTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolIntakeType>()
                .HasMany(e => e.Schools)
                .WithRequired(e => e.IntakeType)
                .HasForeignKey(e => e.IntakeTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolLocation>()
                .HasMany(e => e.BehaviourAchievements)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolLocation>()
                .HasMany(e => e.BehaviourIncidents)
                .WithRequired(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolPhase>()
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
                .WithRequired(x => x.SenStatus)
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
                .HasMany(e => e.CurriculumSubjects)
                .WithRequired(e => e.Leader)
                .HasForeignKey(e => e.LeaderId)
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
                .HasMany(e => e.PersonnelObservationsOwn)
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

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AssessmentResults)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AttendanceRegisterMarks)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.BehaviourAchievements)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.BehaviourIncidents)
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
                .HasMany(e => e.FinanceSales)
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