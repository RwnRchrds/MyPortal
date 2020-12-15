using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole,
        UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public bool Debug { get; set; }

        public virtual DbSet<AcademicTerm> AcademicTerms { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<AccountTransaction> AccountTransactions { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<AchievementOutcome> AchievementOutcomes { get; set; }
        public virtual DbSet<AchievementType> AchievementTypes { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityEvent> ActivityEvents { get; set; }
        public virtual DbSet<ActivityMembership> ActivityMemberships { get; set; }
        public virtual DbSet<ActivitySupervisor> ActivitySupervisors { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressPerson> AddressPersons { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<AgencyType> AgencyTypes { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentRelationshipType> AgentRelationshipTypes { get; set; }
        public virtual DbSet<Aspect> Aspects { get; set; }
        public virtual DbSet<AspectType> AspectTypes { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceCodeMeaning> AttendanceCodeMeanings { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendancePeriod> AttendancePeriods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<AttendanceWeekPattern> AttendanceWeekPatterns { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<BehaviourOutcome> BehaviourOutcomes { get; set; }
        public virtual DbSet<BehaviourStatus> BehaviourStatus { get; set; }
        public virtual DbSet<BehaviourTarget> BehaviourTargets { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillCharge> BillCharges { get; set; }
        public virtual DbSet<StudentDiscount> BillDiscounts { get; set; }
        public virtual DbSet<BillAccountTransaction> BillAccountTransactions { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<ChargeDiscount> ChargeDiscounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentBank> CommentBanks { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactRelationshipType> ContactRelationshipTypes { get; set; }
        public virtual DbSet<ContactRelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CoverArrangement> CoverArrangements { get; set; }
        public virtual DbSet<CurriculumBand> CurriculumBands { get; set; }
        public virtual DbSet<CurriculumBandBlockAssignment> CurriculumBandBlocks { get; set; }
        public virtual DbSet<CurriculumBandMembership> Enrolments { get; set; }
        public virtual DbSet<CurriculumBlock> CurriculumBlocks { get; set; }
        public virtual DbSet<CurriculumGroup> CurriculumGroups { get; set; }
        public virtual DbSet<CurriculumGroupMembership> CurriculumGroupMemberships { get; set; }
        public virtual DbSet<CurriculumYearGroup> CurriculumYearGroups { get; set; }
        public virtual DbSet<Detention> Detentions { get; set; }
        public virtual DbSet<DetentionType> DetentionTypes { get; set; }
        public virtual DbSet<DiaryEvent> DiaryEvents { get; set; }
        public virtual DbSet<DiaryEventAttendee> DiaryEventAttendees { get; set; }
        public virtual DbSet<DiaryEventAttendeeResponse> DiaryEventInvitationResponses { get; set; }
        public virtual DbSet<DiaryEventTemplate> DiaryEventTemplates { get; set; }
        public virtual DbSet<DiaryEventType> DiaryEventTypes { get; set; }
        public virtual DbSet<DietaryRequirement> DietaryRequirements { get; set; }
        public virtual DbSet<Directory> Directories { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmailAddress> EmailAddresses { get; set; }
        public virtual DbSet<EmailAddressType> EmailAddressTypes { get; set; }
        public virtual DbSet<Ethnicity> Ethnicities { get; set; }
        public virtual DbSet<ExamAssessment> ExamAssessments { get; set; }
        public virtual DbSet<ExamAssessmentAspect> ExamAssessmentAspects { get; set; }
        public virtual DbSet<ExamAssessmentMode> ExamAssessmentModes { get; set; }
        public virtual DbSet<ExamAward> ExamAwards { get; set; }
        public virtual DbSet<ExamAwardElement> ExamAwardElements { get; set; }
        public virtual DbSet<ExamAwardSeries> ExamAwardSeries { get; set; }
        public virtual DbSet<ExamBaseComponent> ExamBaseComponents { get; set; }
        public virtual DbSet<ExamBaseElement> ExamBaseElements { get; set; }
        public virtual DbSet<ExamBoard> ExamBoards { get; set; }
        public virtual DbSet<ExamCandidate> ExamCandidates { get; set; }
        public virtual DbSet<ExamCandidateSeries> ExamCandidateSeries { get; set; }
        public virtual DbSet<ExamCandidateSpecialArrangement> ExamCandidateSpecialArrangements { get; set; }
        public virtual DbSet<ExamComponent> ExamComponents { get; set; }
        public virtual DbSet<ExamComponentSitting> ExamComponentSittings { get; set; }
        public virtual DbSet<ExamElement> ExamElements { get; set; }
        public virtual DbSet<ExamElementComponent> ExamElementComponents { get; set; }
        public virtual DbSet<ExamEnrolment> ExamEnrolments { get; set; }
        public virtual DbSet<ExamQualification> ExamQualifications { get; set; }
        public virtual DbSet<ExamQualificationLevel> ExamQualificationLevels { get; set; }
        public virtual DbSet<ExamResultEmbargo> ExamResultEmbargoes { get; set; }
        public virtual DbSet<ExamRoom> ExamRooms { get; set; }
        public virtual DbSet<ExamRoomSeat> ExamRoomSeats { get; set; }
        public virtual DbSet<ExamSeason> ExamSeasons { get; set; }
        public virtual DbSet<ExamSeatAllocation> ExamSeatAllocations { get; set; }
        public virtual DbSet<ExamSeries> ExamSeries { get; set; }
        public virtual DbSet<ExamSession> ExamSessions { get; set; }
        public virtual DbSet<ExamSpecialArrangement> ExamSpecialArrangements { get; set; }
        public virtual DbSet<ExclusionReason> ExclusionReasons { get; set; }
        public virtual DbSet<ExclusionType> ExclusionTypes { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<GiftedTalented> GiftedTalented { get; set; }
        public virtual DbSet<GovernanceType> GovernanceTypes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSet> GradeSets { get; set; }
        public virtual DbSet<HomeworkItem> Homework { get; set; }
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
        public virtual DbSet<LogNote> ProfileLogNotes { get; set; }
        public virtual DbSet<LogNoteType> ProfileLogNoteTypes { get; set; }
        public virtual DbSet<MarksheetColumn> MarksheetColumns { get; set; }
        public virtual DbSet<MarksheetTemplate> Marksheets { get; set; }
        public virtual DbSet<MarksheetTemplateGroup> MarksheetTemplateGroups { get; set; }
        public virtual DbSet<MedicalCondition> Conditions { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<ObservationOutcome> ObservationOutcomes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonCondition> PersonConditions { get; set; }
        public virtual DbSet<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportCard> ReportCards { get; set; }
        public virtual DbSet<ReportCardSubmission> ReportCardSubmissions { get; set; }
        public virtual DbSet<ReportCardTarget> ReportCardTargets { get; set; }
        public virtual DbSet<ReportCardTargetSubmission> ReportCardTargetSubmissions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomClosure> RoomClosures { get; set; }
        public virtual DbSet<RoomClosureReason> RoomClosureReasons { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolPhase> Phases { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SenEvent> SenEvents { get; set; }
        public virtual DbSet<SenProvision> SenProvisions { get; set; }
        public virtual DbSet<SenProvisionType> SenProvisionTypes { get; set; }
        public virtual DbSet<SenReview> SenReviews { get; set; }
        public virtual DbSet<SenReviewType> SenReviewTypes { get; set; }
        public virtual DbSet<SenStatus> SenStatuses { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<StaffAbsence> StaffAbsences { get; set; }
        public virtual DbSet<StaffAbsenceType> StaffAbsenceTypes { get; set; }
        public virtual DbSet<StaffIllnessType> StaffIllnessTypes { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAgentRelationship> StudentAgentRelationships { get; set; }
        public virtual DbSet<StudentCharge> StudentCharges { get; set; }
        public virtual DbSet<StudentContactRelationship> StudentContactRelationships { get; set; }
        public virtual DbSet<StudentContactRelationship> StudentContacts { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudyTopic> StudyTopics { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectCode> SubjectCodes { get; set; }
        public virtual DbSet<SubjectCodeSet> SubjectCodeSets { get; set; }
        public virtual DbSet<SubjectStaffMember> SubjectStaffMembers { get; set; }
        public virtual DbSet<SubjectStaffMemberRole> SubjectStaffMemberRoles { get; set; }
        public virtual DbSet<SystemArea> SystemAreas { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCertificates { get; set; }
        public virtual DbSet<TrainingCertificateStatus> TrainingCertificateStatus { get; set; }
        public virtual DbSet<TrainingCourse> TrainingCourses { get; set; }
        public virtual DbSet<VatRate> VatRates { get; set; }
        public virtual DbSet<YearGroup> YearGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (!Debug)
            {
                modelBuilder.Entity<AcademicTerm>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.AcademicYear)
                        .WithMany(x => x.AcademicTerms)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AcademicYear>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.AcademicYear)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AttendanceWeekPatterns)
                        .WithOne(x => x.AcademicYear)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.AcademicYear)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LogNotes)
                        .WithOne(x => x.AcademicYear)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AccountTransaction>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.AccountTransactions)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Achievement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<AchievementOutcome>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(ao => ao.Achievements)
                        .WithOne(a => a.Outcome)
                        .HasForeignKey(a => a.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AchievementType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.AchievementTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Activity>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Events)
                        .WithOne(x => x.Activity)
                        .HasForeignKey(x => x.ActivityId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Memberships)
                        .WithOne(x => x.Activity)
                        .HasForeignKey(x => x.ActivityId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Supervisors)
                        .WithOne(x => x.Activity)
                        .HasForeignKey(x => x.ActivityId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ActivityEvent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Event)
                        .WithOne(x => x.Activity)
                        .HasForeignKey<ActivityEvent>(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ActivityMembership>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.ActivityMemberships)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ActivitySupervisor>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Supervisor)
                        .WithMany(x => x.Activities)
                        .HasForeignKey(x => x.SupervisorId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Address>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.People)
                        .WithOne(x => x.Address)
                        .HasForeignKey(x => x.AddressId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AddressPerson>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<Agency>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Agents)
                        .WithOne(x => x.Agency)
                        .HasForeignKey(x => x.AgencyId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Address)
                        .WithMany(x => x.Agencies)
                        .HasForeignKey(x => x.AddressId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.AgencyType)
                        .WithMany(x => x.Agencies)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Directory)
                        .WithOne(x => x.Agency)
                        .HasForeignKey<Agency>(x => x.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AgencyType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<Agent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Person)
                        .WithOne(x => x.AgentDetails)
                        .HasForeignKey<Agent>(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LinkedStudents)
                        .WithOne(x => x.Agent)
                        .HasForeignKey(x => x.AgentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AgentRelationshipType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Relationships)
                        .WithOne(x => x.RelationshipType)
                        .HasForeignKey(x => x.RelationshipTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Aspect>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(a => a.Results)
                        .WithOne(r => r.Aspect)
                        .HasForeignKey(r => r.AspectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AspectType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(at => at.Aspects)
                        .WithOne(a => a.Type)
                        .HasForeignKey(a => a.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceCode>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<AttendanceCodeMeaning>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(acm => acm.Codes)
                        .WithOne(ac => ac.CodeMeaning)
                        .HasForeignKey(ac => ac.MeaningId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceMark>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<AttendancePeriod>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(ap => ap.AttendanceMarks)
                        .WithOne(am => am.AttendancePeriod)
                        .HasForeignKey(am => am.PeriodId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(ap => ap.Sessions)
                        .WithOne(s => s.AttendancePeriod)
                        .HasForeignKey(s => s.PeriodId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceWeek>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(aw => aw.AttendanceMarks)
                        .WithOne(am => am.Week)
                        .HasForeignKey(am => am.WeekId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceWeekPattern>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(awp => awp.Periods)
                        .WithOne(ap => ap.WeekPattern)
                        .HasForeignKey(ap => ap.WeekPatternId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(awp => awp.AttendanceWeeks)
                        .WithOne(aw => aw.WeekPattern)
                        .HasForeignKey(aw => aw.WeekPatternId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BasketItem>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<BehaviourOutcome>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Outcome)
                        .HasForeignKey(x => x.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BehaviourStatus>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Status)
                        .HasForeignKey(x => x.StatusId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BehaviourTarget>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.ReportCardLinks)
                        .WithOne(x => x.Target)
                        .HasForeignKey(x => x.TargetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Bill>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.BillItems)
                        .WithOne(x => x.Bill)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillAccountTransaction>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Bill)
                        .WithMany(x => x.AccountTransactions)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.AccountTransaction)
                        .WithMany(x => x.BillAccountTransactions)
                        .HasForeignKey(x => x.AccountTransactionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillCharge>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Bill)
                        .WithMany(x => x.BillCharges)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Charge)
                        .WithMany(x => x.BillCharges)
                        .HasForeignKey(x => x.ChargeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillDiscount>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Discount)
                        .WithMany(x => x.BillDiscounts)
                        .HasForeignKey(x => x.DiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Bill)
                        .WithMany(x => x.BillDiscounts)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillItem>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<Bulletin>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<Charge>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<ChargeDiscount>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Charge)
                        .WithMany(x => x.ChargeDiscounts)
                        .HasForeignKey(x => x.ChargeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Discount)
                        .WithMany(x => x.ChargeDiscounts)
                        .HasForeignKey(x => x.DiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Class>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Sessions)
                        .WithOne(x => x.Class)
                        .HasForeignKey(x => x.ClassId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Comment>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<CommentBank>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Comments)
                        .WithOne(x => x.CommentBank)
                        .HasForeignKey(x => x.CommentBankId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CommunicationLog>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<CommunicationType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.CommunicationLogs)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.CommunicationTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Contact>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.LinkedStudents)
                        .WithOne(x => x.Contact)
                        .HasForeignKey(x => x.ContactId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Person)
                        .WithOne(x => x.ContactDetails)
                        .HasForeignKey<Contact>(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ContactRelationshipType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.StudentContacts)
                        .WithOne(x => x.RelationshipType)
                        .HasForeignKey(x => x.RelationshipTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Course>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Classes)
                        .WithOne(x => x.Course)
                        .HasForeignKey(x => x.CourseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Subject)
                        .WithMany(x => x.Courses)
                        .HasForeignKey(x => x.SubjectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Awards)
                        .WithOne(x => x.Course)
                        .HasForeignKey(x => x.CourseId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CoverArrangement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<CurriculumBand>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasIndex(i => new {i.AcademicYearId, i.Code}).IsUnique();

                    e.HasMany(x => x.Enrolments)
                        .WithOne(x => x.Band)
                        .HasForeignKey(x => x.BandId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AssignedBlocks)
                        .WithOne(x => x.Band)
                        .HasForeignKey(x => x.BandId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CurriculumBandBlockAssignment>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<CurriculumBandMembership>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<CurriculumBlock>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.BandAssignments)
                        .WithOne(x => x.Block)
                        .HasForeignKey(x => x.BlockId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Groups)
                        .WithOne(x => x.Block)
                        .HasForeignKey(x => x.BlockId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CurriculumGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Memberships)
                        .WithOne(x => x.CurriculumGroup)
                        .HasForeignKey(x => x.GroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Classes)
                        .WithOne(x => x.Group)
                        .HasForeignKey(x => x.GroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CurriculumGroupMembership>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<CurriculumYearGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.YearGroups)
                        .WithOne(x => x.CurriculumYearGroup)
                        .HasForeignKey(x => x.CurriculumYearGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Bands)
                        .WithOne(x => x.CurriculumYearGroup)
                        .HasForeignKey(x => x.CurriculumYearGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Detention>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Detention)
                        .HasForeignKey(x => x.DetentionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DetentionType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Detentions)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.DetentionTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEvent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Detention)
                        .WithOne(x => x.Event)
                        .HasForeignKey<Detention>(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Attendees)
                        .WithOne(x => x.Event)
                        .HasForeignKey(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEventAttendee>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<DiaryEventAttendeeResponse>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.DiaryEventAttendees)
                        .WithOne(x => x.Response)
                        .HasForeignKey(x => x.ResponseId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEventTemplate>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<DiaryEventType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.DiaryEventTemplates)
                        .WithOne(x => x.DiaryEventType)
                        .HasForeignKey(x => x.EventTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.DiaryEvents)
                        .WithOne(x => x.EventType)
                        .HasForeignKey(x => x.EventTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DietaryRequirement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.PersonDietaryRequirements)
                        .WithOne(x => x.DietaryRequirement)
                        .HasForeignKey(x => x.DietaryRequirementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Directory>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(d => d.Bulletin)
                        .WithOne(b => b.Directory)
                        .HasForeignKey<Bulletin>(b => b.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(d => d.Person)
                        .WithOne(p => p.Directory)
                        .HasForeignKey<Person>(p => p.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(d => d.HomeworkItem)
                        .WithOne(hi => hi.Directory)
                        .HasForeignKey<HomeworkItem>(hi => hi.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(d => d.LessonPlan)
                        .WithOne(lp => lp.Directory)
                        .HasForeignKey<LessonPlan>(lp => lp.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(d => d.Subdirectories)
                        .WithOne(d => d.Parent)
                        .HasForeignKey(d => d.ParentId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(d => d.Documents)
                        .WithOne(d => d.Directory)
                        .HasForeignKey(d => d.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Discount>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<Document>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<DocumentType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Documents)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<EmailAddress>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<EmailAddressType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.EmailAddresses)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Ethnicity>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.People)
                        .WithOne(x => x.Ethnicity)
                        .HasForeignKey(x => x.EthnicityId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessment>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Aspects)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey(x => x.AssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExamBoard)
                        .WithMany(x => x.ExamAssessments)
                        .HasForeignKey(x => x.ExamBoardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExamAward)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey<ExamAward>(x => x.AssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExamBaseElement)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey<ExamBaseElement>(x => x.AssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExamBaseComponent)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey<ExamBaseComponent>(x => x.ExamAssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessmentAspect>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Aspect)
                        .WithMany(x => x.AssessmentAspects)
                        .HasForeignKey(x => x.AspectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessmentMode>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Components)
                        .WithOne(x => x.AssessmentMode)
                        .HasForeignKey(x => x.AssessmentModeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamBaseComponents)
                        .WithOne(x => x.AssessmentMode)
                        .HasForeignKey(x => x.AssessmentModeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAward>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.ExamAwardElements)
                        .WithOne(x => x.Award)
                        .HasForeignKey(x => x.AwardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamAwardSeries)
                        .WithOne(x => x.Award)
                        .HasForeignKey(x => x.AwardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamEnrolments)
                        .WithOne(x => x.Award)
                        .HasForeignKey(x => x.AwardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Qualification)
                        .WithMany(x => x.Awards)
                        .HasForeignKey(x => x.QualificationId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Course)
                        .WithMany(x => x.Awards)
                        .HasForeignKey(x => x.CourseId)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                modelBuilder.Entity<ExamAwardElement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Element)
                        .WithMany(x => x.ExamAwardElements)
                        .HasForeignKey(x => x.ElementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAwardSeries>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamAwardSeries)
                        .HasForeignKey(x => x.AwardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamBaseComponent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.ExamComponents)
                        .WithOne(x => x.BaseComponent)
                        .HasForeignKey(x => x.BaseComponentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamBaseElement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Elements)
                        .WithOne(x => x.BaseElement)
                        .HasForeignKey(x => x.BaseElementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.QcaCode)
                        .WithMany(x => x.Elements)
                        .HasForeignKey(x => x.QcaCodeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Level)
                        .WithMany(x => x.ExamBaseElements)
                        .HasForeignKey(x => x.LevelId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamBoard>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.ExamSeries)
                        .WithOne(x => x.ExamBoard)
                        .HasForeignKey(x => x.ExamBoardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamCandidate>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.LinkedSeries)
                        .WithOne(x => x.Candidate)
                        .HasForeignKey(x => x.CandidateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SeatAllocations)
                        .WithOne(x => x.Candidate)
                        .HasForeignKey(x => x.CandidateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SpecialArrangements)
                        .WithOne(x => x.Candidate)
                        .HasForeignKey(x => x.CandidateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithOne(x => x.Candidate)
                        .HasForeignKey<ExamCandidate>(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamEnrolments)
                        .WithOne(x => x.Candidate)
                        .HasForeignKey(x => x.CandidateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamCandidateSeries>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamCandidateSeries)
                        .HasForeignKey(x => x.SeriesId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamCandidateSpecialArrangement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.SpecialArrangement)
                        .WithMany(x => x.Candidates)
                        .HasForeignKey(x => x.SpecialArrangementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamComponent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Sittings)
                        .WithOne(x => x.Component)
                        .HasForeignKey(x => x.ComponentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamElementComponents)
                        .WithOne(x => x.Component)
                        .HasForeignKey(x => x.ComponentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamComponents)
                        .HasForeignKey(x => x.ExamSeriesId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Session)
                        .WithMany(x => x.Components)
                        .HasForeignKey(x => x.SessionId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamComponentSitting>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Room)
                        .WithMany(x => x.ExamComponentSittings)
                        .HasForeignKey(x => x.ExamRoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SeatAllocations)
                        .WithOne(x => x.Sitting)
                        .HasForeignKey(x => x.SittingId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamElement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamElements)
                        .HasForeignKey(x => x.SeriesId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamElementComponents)
                        .WithOne(x => x.Element)
                        .HasForeignKey(x => x.ElementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamElementComponent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExamEnrolment>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExamQualification>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Levels)
                        .WithOne(x => x.Qualification)
                        .HasForeignKey(x => x.QualificationId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamQualificationLevel>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.DefaultGradeSet)
                        .WithMany(x => x.ExamQualificationLevels)
                        .HasForeignKey(x => x.DefaultGradeSetId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamResultEmbargo>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.ResultSet)
                        .WithMany(x => x.ExamResultEmbargoes)
                        .HasForeignKey(x => x.ResultSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamRoom>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Room)
                        .WithOne(x => x.ExamRoom)
                        .HasForeignKey<ExamRoom>(x => x.RoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamRoomSeat>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.ExamRoom)
                        .WithMany(x => x.Seats)
                        .HasForeignKey(x => x.ExamRoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SeatAllocations)
                        .WithOne(x => x.Seat)
                        .HasForeignKey(x => x.SeatId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamSeason>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.ResultSet)
                        .WithMany(x => x.ExamSeasons)
                        .HasForeignKey(x => x.ResultSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamSeries)
                        .WithOne(x => x.Season)
                        .HasForeignKey(x => x.ExamSeasonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamSeatAllocation>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExamSeries>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExamSession>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExamSpecialArrangement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExclusionReason>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ExclusionType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<File>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Document)
                        .WithOne(x => x.Attachment)
                        .HasForeignKey<File>(x => x.DocumentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<GiftedTalented>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<GovernanceType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.GovernanceType)
                        .HasForeignKey(x => x.GovernanceTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Grade>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(e => e.Results)
                        .WithOne(e => e.Grade)
                        .HasForeignKey(e => e.GradeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<GradeSet>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(gs => gs.Aspects)
                        .WithOne(a => a.GradeSet)
                        .HasForeignKey(a => a.GradeSetId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(gs => gs.Grades)
                        .WithOne(g => g.GradeSet)
                        .HasForeignKey(g => g.GradeSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<HomeworkItem>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Submissions)
                        .WithOne(x => x.HomeworkItem)
                        .HasForeignKey(x => x.HomeworkId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<HomeworkSubmission>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Task)
                        .WithOne(x => x.HomeworkSubmission)
                        .HasForeignKey<HomeworkSubmission>(x => x.TaskId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.HomeworkSubmissions)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.SubmittedWork)
                        .WithOne(x => x.HomeworkSubmission)
                        .HasForeignKey<HomeworkSubmission>(x => x.DocumentId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<House>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.House)
                        .HasForeignKey(x => x.HouseId);
                });

                modelBuilder.Entity<Incident>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Detentions)
                        .WithOne(x => x.Incident)
                        .HasForeignKey(x => x.IncidentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<IncidentDetention>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<IncidentType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.BehaviourTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<IntakeType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.IntakeType)
                        .HasForeignKey(x => x.IntakeTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<LessonPlan>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<LessonPlanTemplate>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<LocalAuthority>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.LocalAuthority)
                        .HasForeignKey(x => x.LocalAuthorityId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Location>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.BehaviourAchievements)
                        .WithOne(x => x.Location)
                        .HasForeignKey(x => x.LocationId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.BehaviourIncidents)
                        .WithOne(e => e.Location)
                        .HasForeignKey(x => x.LocationId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Rooms)
                        .WithOne(x => x.Location)
                        .HasForeignKey(x => x.LocationId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<LogNote>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<LogNoteType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.LogNotes)
                        .WithOne(x => x.LogNoteType)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MarksheetColumn>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<MarksheetTemplate>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Columns)
                        .WithOne(x => x.Template)
                        .HasForeignKey(x => x.TemplateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MarksheetTemplateGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Template)
                        .WithMany(x => x.TemplateGroups)
                        .HasForeignKey(x => x.MarksheetTemplateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.MarksheetTemplates)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MedicalCondition>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.PersonConditions)
                        .WithOne(x => x.MedicalCondition)
                        .HasForeignKey(x => x.ConditionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MedicalEvent>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<Observation>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<ObservationOutcome>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Observations)
                        .WithOne(x => x.Outcome)
                        .HasForeignKey(x => x.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Permission>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.SystemArea)
                        .WithMany(x => x.Permissions)
                        .HasForeignKey(x => x.AreaId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.RolePermissions)
                        .WithOne(x => x.Permission)
                        .HasForeignKey(x => x.PermissionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Person>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(p => p.Addresses)
                        .WithOne(a => a.Person)
                        .HasForeignKey(a => a.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.EmailAddresses)
                        .WithOne(ea => ea.Person)
                        .HasForeignKey(ea => ea.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.MedicalConditions)
                        .WithOne(pc => pc.Person)
                        .HasForeignKey(pc => pc.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.PhoneNumbers)
                        .WithOne(pn => pn.Person)
                        .HasForeignKey(pn => pn.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.Property(p => p.Gender)
                        .IsFixedLength()
                        .IsUnicode(false);

                    e.HasMany(x => x.DiaryEventInvitations)
                        .WithOne(x => x.Person)
                        .HasForeignKey(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AssignedTo)
                        .WithOne(x => x.AssignedTo)
                        .HasForeignKey(x => x.AssignedToId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<PersonCondition>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<PersonDietaryRequirement>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<PhoneNumber>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<PhoneNumberType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.PhoneNumbers)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Photo>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.People)
                        .WithOne(x => x.Photo)
                        .HasForeignKey(x => x.PhotoId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Product>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.BasketItems)
                        .WithOne(x => x.Product)
                        .HasForeignKey(x => x.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.BillItems)
                        .WithOne(x => x.Product)
                        .HasForeignKey(x => x.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.VatRate)
                        .WithMany(x => x.Products)
                        .HasForeignKey(x => x.VatRateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ProductDiscount>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Product)
                        .WithMany(x => x.ProductDiscounts)
                        .HasForeignKey(x => x.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Discount)
                        .WithMany(x => x.ProductDiscounts)
                        .HasForeignKey(x => x.DiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ProductType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Products)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.ProductTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<RefreshToken>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<RegGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.RegGroup)
                        .HasForeignKey(x => x.RegGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Report>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<ReportCard>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Targets)
                        .WithOne(x => x.ReportCard)
                        .HasForeignKey(x => x.ReportCardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.ReportCards)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.BehaviourType)
                        .WithMany(x => x.ReportCards)
                        .HasForeignKey(x => x.BehaviourTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Submissions)
                        .WithOne(x => x.ReportCard)
                        .HasForeignKey(x => x.ReportCardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCardSubmission>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.TargetSubmissions)
                        .WithOne(x => x.Submission)
                        .HasForeignKey(x => x.SubmissionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.SubmittedBy)
                        .WithMany(x => x.ReportCardSubmissions)
                        .HasForeignKey(x => x.SubmittedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCardTarget>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Submissions)
                        .WithOne(x => x.Target)
                        .HasForeignKey(x => x.TargetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCardTargetSubmission>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<Result>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<ResultSet>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(rs => rs.Results)
                        .WithOne(r => r.ResultSet)
                        .HasForeignKey(r => r.ResultSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Role>(e =>
                {
                    e.ToTable("Roles");

                    e.HasMany(x => x.UserRoles)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.RolePermissions)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.RoleClaims)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired();
                });

                modelBuilder.Entity<RoleClaim>(e =>
                {
                    e.ToTable("RoleClaims");
                });

                modelBuilder.Entity<RolePermission>(e =>
                {
                    e.HasKey(x => new { x.RoleId, x.PermissionId });
                });

                modelBuilder.Entity<Room>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.CoverArrangements)
                        .WithOne(x => x.Room)
                        .HasForeignKey(x => x.RoomId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Sessions)
                        .WithOne(x => x.Room)
                        .HasForeignKey(x => x.RoomId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.DiaryEvents)
                        .WithOne(x => x.Room)
                        .HasForeignKey(x => x.RoomId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Closures)
                        .WithOne(x => x.Room)
                        .HasForeignKey(x => x.RoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<RoomClosure>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<RoomClosureReason>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Closures)
                        .WithOne(x => x.Reason)
                        .HasForeignKey(x => x.ReasonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<School>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<SchoolPhase>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.SchoolPhase)
                        .HasForeignKey(x => x.PhaseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SchoolType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenEvent>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<SenEventType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Events)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.EventTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenProvision>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<SenProvisionType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.SenProvisions)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.ProvisionTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenReview>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<SenReviewType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Reviews)
                        .WithOne(x => x.ReviewType)
                        .HasForeignKey(x => x.ReviewTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenStatus>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                    
                    e.HasMany(x => x.Students)
                        .WithOne(x => x.SenStatus)
                        .HasForeignKey(x => x.SenStatusId);

                    e.Property(x => x.Code)
                        .IsFixedLength()
                        .IsUnicode(false);
                });

                modelBuilder.Entity<Session>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<StaffAbsence>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<StaffAbsenceType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Absences)
                        .WithOne(x => x.AbsenceType)
                        .HasForeignKey(x => x.AbsenceTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StaffIllnessType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Absences)
                        .WithOne(x => x.IllnessType)
                        .HasForeignKey(x => x.IllnessTypeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StaffMember>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(sm => sm.Sessions)
                        .WithOne(s => s.Teacher)
                        .HasForeignKey(s => s.TeacherId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(sm => sm.Person)
                        .WithOne(p => p.StaffMemberDetails)
                        .HasForeignKey<StaffMember>(sm => sm.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PastoralHouses)
                        .WithOne(h => h.HeadOfHouse)
                        .HasForeignKey(h => h.HeadId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PastoralRegGroups)
                        .WithOne(rg => rg.Tutor)
                        .HasForeignKey(rg => rg.TutorId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PastoralYearGroups)
                        .WithOne(yg => yg.HeadOfYear)
                        .HasForeignKey(yg => yg.HeadId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PersonnelObservationsObserved)
                        .WithOne(o => o.Observer)
                        .HasForeignKey(o => o.ObserverId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PersonnelObservations)
                        .WithOne(o => o.Observee)
                        .HasForeignKey(o => o.ObserveeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.PersonnelTrainingCertificates)
                        .WithOne(tc => tc.StaffMember)
                        .HasForeignKey(tc => tc.StaffId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.CoverArrangements)
                        .WithOne(ca => ca.Teacher)
                        .HasForeignKey(ca => ca.TeacherId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.Subordinates)
                        .WithOne(sm => sm.LineManager)
                        .HasForeignKey(sm => sm.LineManagerId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(sm => sm.Absences)
                        .WithOne(sa => sa.StaffMember)
                        .HasForeignKey(sm => sm.StaffMemberId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Subjects)
                        .WithOne(x => x.StaffMember)
                        .HasForeignKey(x => x.StaffMemberId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Student>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(s => s.Results)
                        .WithOne(r => r.Student)
                        .HasForeignKey(s => s.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AttendanceMarks)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Enrolments)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.FinanceBasketItems)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Bills)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentContacts)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.GiftedTalentedSubjects)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Person)
                        .WithOne(x => x.StudentDetails)
                        .HasForeignKey<Student>(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.MedicalEvents)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ProfileLogs)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SenEvents)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SenProvisions)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.GroupMemberships)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.Property(x => x.Upn)
                        .IsUnicode(false);
                });

                modelBuilder.Entity<StudentAgentRelationship>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.AgentRelationships)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentCharge>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Charge)
                        .WithMany(x => x.StudentCharges)
                        .HasForeignKey(x => x.ChargeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.Charges)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentContactRelationship>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Contact)
                        .WithMany(x => x.LinkedStudents)
                        .HasForeignKey(x => x.ContactId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.StudentContacts)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentDiscount>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.Discounts)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Discount)
                        .WithMany(x => x.StudentDiscounts)
                        .HasForeignKey(x => x.DiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.MarksheetTemplates)
                        .WithOne(x => x.StudentGroup)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudyTopic>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.LessonPlans)
                        .WithOne(x => x.StudyTopic)
                        .HasForeignKey(x => x.StudyTopicId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Course)
                        .WithMany(x => x.StudyTopics)
                        .HasForeignKey(x => x.CourseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Subject>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Courses)
                        .WithOne(x => x.Subject)
                        .HasForeignKey(x => x.SubjectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StaffMembers)
                        .WithOne(x => x.Subject)
                        .HasForeignKey(x => x.SubjectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.GiftedTalentedStudents)
                        .WithOne(x => x.Subject)
                        .HasForeignKey(x => x.SubjectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SubjectCode>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<SubjectCodeSet>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.SubjectCodes)
                        .WithOne(x => x.SubjectCodeSet)
                        .HasForeignKey(x => x.SubjectCodeSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SubjectStaffMember>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<SubjectStaffMemberRole>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.StaffMembers)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SystemArea>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Reports)
                        .WithOne(x => x.SystemArea)
                        .HasForeignKey(x => x.AreaId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SubAreas)
                        .WithOne(x => x.Parent)
                        .HasForeignKey(x => x.ParentId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Task>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<TaskType>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Tasks)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<TrainingCertificate>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
                });

                modelBuilder.Entity<TrainingCertificateStatus>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Certificates)
                        .WithOne(x => x.Status)
                        .HasForeignKey(x => x.StatusId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<TrainingCourse>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.Certificates)
                        .WithOne(x => x.TrainingCourse)
                        .HasForeignKey(x => x.CourseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<User>(e =>
                {
                    e.ToTable("Users");

                    e.HasOne(x => x.Person)
                        .WithOne(x => x.User)
                        .HasForeignKey<User>(x => x.PersonId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.UserRoles)
                        .WithOne(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LogNotesCreated)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LogNotesUpdated)
                        .WithOne(x => x.UpdatedBy)
                        .HasForeignKey(x => x.UpdatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Documents)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.MedicalEvents)
                        .WithOne(x => x.RecordedBy)
                        .HasForeignKey(x => x.RecordedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.RecordedBy)
                        .HasForeignKey(x => x.RecordedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.RecordedBy)
                        .HasForeignKey(x => x.RecordedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LessonPlans)
                        .WithOne(x => x.Author)
                        .HasForeignKey(x => x.AuthorId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Bulletins)
                        .WithOne(x => x.Author)
                        .HasForeignKey(x => x.AuthorId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AssignedBy)
                        .WithOne(x => x.AssignedBy)
                        .HasForeignKey(x => x.AssignedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.UserClaims)
                        .WithOne(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .IsRequired();

                    e.HasMany(x => x.UserLogins)
                        .WithOne(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .IsRequired();

                    e.HasMany(x => x.UserTokens)
                        .WithOne(x => x.User)
                        .HasForeignKey(x => x.UserId)
                        .IsRequired();
                });

                modelBuilder.Entity<UserClaim>(e =>
                {
                    e.ToTable("UserClaims");
                });

                modelBuilder.Entity<UserLogin>(e =>
                {
                    e.ToTable("UserLogins");
                });

                modelBuilder.Entity<UserRole>(e =>
                {
                    e.ToTable("UserRoles");
                });

                modelBuilder.Entity<UserToken>(e =>
                {
                    e.ToTable("UserTokens");
                });

                modelBuilder.Entity<VatRate>(e => { e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()"); });

                modelBuilder.Entity<YearGroup>(e =>
                {
                    e.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

                    e.HasMany(x => x.RegGroups)
                        .WithOne(x => x.YearGroup)
                        .HasForeignKey(x => x.YearGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.YearGroup)
                        .HasForeignKey(x => x.YearGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });
            }
        }
    }
}