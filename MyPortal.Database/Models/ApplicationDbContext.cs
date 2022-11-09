using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortal.Database.Interfaces;
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
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressLink> AddressLinks { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<AgencyType> AgencyTypes { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<AgentType> AgentTypes { get; set; }
        public virtual DbSet<Aspect> Aspects { get; set; }
        public virtual DbSet<AspectType> AspectTypes { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceCodeType> AttendanceCodeMeanings { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendancePeriod> AttendancePeriods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<AttendanceWeekPattern> AttendanceWeekPatterns { get; set; }
        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<BehaviourOutcome> BehaviourOutcomes { get; set; }
        public virtual DbSet<BehaviourRoleType> BehaviourRoleTypes { get; set; }
        public virtual DbSet<BehaviourStatus> BehaviourStatus { get; set; }
        public virtual DbSet<BehaviourTarget> BehaviourTargets { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillStudentCharge> BillCharges { get; set; }
        public virtual DbSet<BillDiscount> BillChargeDiscounts { get; set; }
        public virtual DbSet<BillDiscount> BillDiscounts { get; set; }
        public virtual DbSet<BillAccountTransaction> BillAccountTransactions { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<BoarderStatus> BoarderStatuses { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<BuildingFloor> BuildingFloors { get; set; }
        public virtual DbSet<Bulletin> Bulletins { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<ChargeBillingPeriod> ChargeBillingPeriods { get; set; }
        public virtual DbSet<ChargeDiscount> ChargeDiscounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentBank> CommentBanks { get; set; }
        public virtual DbSet<CommentBankArea> CommentBankAreas { get; set; }
        public virtual DbSet<CommentBankSection> CommentBankSections { get; set; }
        public virtual DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<RelationshipType> ContactRelationshipTypes { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CoverArrangement> CoverArrangements { get; set; }
        public virtual DbSet<CurriculumBand> CurriculumBands { get; set; }
        public virtual DbSet<CurriculumBandBlockAssignment> CurriculumBandBlocks { get; set; }
        public virtual DbSet<CurriculumBlock> CurriculumBlocks { get; set; }
        public virtual DbSet<CurriculumGroup> CurriculumGroups { get; set; }
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
        public virtual DbSet<EnrolmentStatus> EnrolmentStatuses { get; set; }
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
        public virtual DbSet<ExamDate> ExamDates { get; set; }
        public virtual DbSet<ExamElement> ExamElements { get; set; }
        public virtual DbSet<ExamElementComponent> ExamElementComponents { get; set; }
        public virtual DbSet<ExamEnrolment> ExamEnrolments { get; set; }
        public virtual DbSet<ExamQualification> ExamQualifications { get; set; }
        public virtual DbSet<ExamQualificationLevel> ExamQualificationLevels { get; set; }
        public virtual DbSet<ExamResultEmbargo> ExamResultEmbargoes { get; set; }
        public virtual DbSet<ExamRoom> ExamRooms { get; set; }
        public virtual DbSet<ExamRoomSeatBlock> ExamRoomSeatBlocks { get; set; }
        public virtual DbSet<ExamSeason> ExamSeasons { get; set; }
        public virtual DbSet<ExamSeatAllocation> ExamSeatAllocations { get; set; }
        public virtual DbSet<ExamSeries> ExamSeries { get; set; }
        public virtual DbSet<ExamSession> ExamSessions { get; set; }
        public virtual DbSet<ExamSpecialArrangement> ExamSpecialArrangements { get; set; }
        public virtual DbSet<Exclusion> Exclusions { get; set; }
        public virtual DbSet<ExclusionAppealResult> ExclusionAppealResults { get; set; }
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
        public virtual DbSet<StudentIncidentDetention> IncidentDetentions { get; set; }
        public virtual DbSet<IncidentType> IncidentTypes { get; set; }
        public virtual DbSet<IntakeType> IntakeTypes { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<LessonPlan> LessonPlans { get; set; }
        public virtual DbSet<LessonPlanHomeworkItem> LessonPlanHomeworkItems { get; set; }
        public virtual DbSet<LessonPlanTemplate> LessonPlanTemplates { get; set; }
        public virtual DbSet<LocalAuthority> LocalAuthorities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LogNote> LogNotes { get; set; }
        public virtual DbSet<LogNoteType> LogNoteTypes { get; set; }
        public virtual DbSet<MarksheetColumn> MarksheetColumns { get; set; }
        public virtual DbSet<MarksheetTemplate> MarksheetTemplates { get; set; }
        public virtual DbSet<Marksheet> MarksheetTemplateGroups { get; set; }
        public virtual DbSet<MedicalCondition> Conditions { get; set; }
        public virtual DbSet<MedicalEvent> MedicalEvents { get; set; }
        public virtual DbSet<NextOfKinRelationshipType> NextOfKinRelationshipTypes { get; set; }
        public virtual DbSet<NextOfKin> NextOfKin { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<ObservationOutcome> ObservationOutcomes { get; set; }
        public virtual DbSet<ParentEvening> ParentEvenings { get; set; }
        public virtual DbSet<ParentEveningAppointment> ParentEveningAppointments { get; set; }
        public virtual DbSet<ParentEveningBreak> ParentEveningBreaks { get; set; }
        public virtual DbSet<ParentEveningStaffMember> ParentEveningStaffMembers { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PersonCondition> PersonConditions { get; set; }
        public virtual DbSet<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<StoreDiscount> ProductDiscounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<ReportCard> ReportCards { get; set; }
        public virtual DbSet<ReportCardEntry> ReportCardEntries { get; set; }
        public virtual DbSet<ReportCardTarget> ReportCardTargets { get; set; }
        public virtual DbSet<ReportCardTargetEntry> ReportCardTargetEntries { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomClosure> RoomClosures { get; set; }
        public virtual DbSet<RoomClosureReason> RoomClosureReasons { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolPhase> SchoolPhases { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SenEvent> SenEvents { get; set; }
        public virtual DbSet<SenProvision> SenProvisions { get; set; }
        public virtual DbSet<SenProvisionType> SenProvisionTypes { get; set; }
        public virtual DbSet<SenReview> SenReviews { get; set; }
        public virtual DbSet<SenReviewType> SenReviewTypes { get; set; }
        public virtual DbSet<SenStatus> SenStatuses { get; set; }
        public virtual DbSet<SenType> SenTypes { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<StaffAbsence> StaffAbsences { get; set; }
        public virtual DbSet<StaffAbsenceType> StaffAbsenceTypes { get; set; }
        public virtual DbSet<StaffIllnessType> StaffIllnessTypes { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<StoreDiscount> StoreDiscounts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentAchievement> StudentAchievements { get; set; }
        public virtual DbSet<StudentAgentRelationship> StudentAgentRelationships { get; set; }
        public virtual DbSet<StudentCharge> StudentCharges { get; set; }
        public virtual DbSet<StudentChargeDiscount> StudentChargeDiscounts { get; set; }
        public virtual DbSet<StudentContactRelationship> StudentContactRelationships { get; set; }
        public virtual DbSet<StudentContactRelationship> StudentContacts { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<StudentGroupMembership> StudentGroupMemberships { get; set; }
        public virtual DbSet<StudentGroupSupervisor> StudentGroupSupervisors { get; set; }
        public virtual DbSet<StudentIncident> StudentIncidents { get; set; }
        public virtual DbSet<StudyTopic> StudyTopics { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectCode> SubjectCodes { get; set; }
        public virtual DbSet<SubjectCodeSet> SubjectCodeSets { get; set; }
        public virtual DbSet<SubjectStaffMember> SubjectStaffMembers { get; set; }
        public virtual DbSet<SubjectStaffMemberRole> SubjectStaffMemberRoles { get; set; }
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
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.AcademicYear)
                        .WithMany(x => x.AcademicTerms)
                        .HasForeignKey(x => x.AcademicYearId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.AttendanceWeeks)
                        .WithOne(x => x.AcademicTerm)
                        .HasForeignKey(x => x.AcademicTermId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AcademicYear>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Achievements)
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
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.AccountTransactions)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Achievement>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<AchievementOutcome>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(ao => ao.StudentAchievements)
                        .WithOne(a => a.Outcome)
                        .HasForeignKey(a => a.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AchievementType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.AchievementTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Activity>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.Activities)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Address>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<AddressLink>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<AddressType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.AddressPersons)
                        .WithOne(x => x.AddressType)
                        .HasForeignKey(x => x.AddressTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Agency>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.AddressLinks)
                        .WithOne(x => x.Agency)
                        .HasForeignKey(x => x.AgencyId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Agents)
                        .WithOne(x => x.Agency)
                        .HasForeignKey(x => x.AgencyId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.AgencyType)
                        .WithMany(x => x.Agencies)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Directory)
                        .WithMany(x => x.Agencies)
                        .HasForeignKey(x => x.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.PhoneNumbers)
                        .WithOne(x => x.Agency)
                        .HasForeignKey(x => x.AgencyId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AgencyType>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<Agent>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Person)
                        .WithMany(x => x.Agents)
                        .HasForeignKey(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LinkedStudents)
                        .WithOne(x => x.Agent)
                        .HasForeignKey(x => x.AgentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AgentType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Agents)
                        .WithOne(x => x.AgentType)
                        .HasForeignKey(x => x.AgentTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Aspect>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(a => a.Results)
                        .WithOne(r => r.Aspect)
                        .HasForeignKey(r => r.AspectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AspectType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(at => at.Aspects)
                        .WithOne(a => a.Type)
                        .HasForeignKey(a => a.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceCode>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.AttendanceMarks)
                        .WithOne(x => x.AttendanceCode)
                        .HasForeignKey(x => x.CodeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceCodeType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(acm => acm.Codes)
                        .WithOne(ac => ac.CodeType)
                        .HasForeignKey(ac => ac.AttendanceCodeTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceMark>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<AttendancePeriod>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(aw => aw.AttendanceMarks)
                        .WithOne(am => am.Week)
                        .HasForeignKey(am => am.WeekId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<AttendanceWeekPattern>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<BehaviourOutcome>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StudentIncidents)
                        .WithOne(x => x.Outcome)
                        .HasForeignKey(x => x.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BehaviourStatus>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StudentIncidents)
                        .WithOne(x => x.Status)
                        .HasForeignKey(x => x.StatusId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BehaviourTarget>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.ReportCardLinks)
                        .WithOne(x => x.Target)
                        .HasForeignKey(x => x.TargetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Bill>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.BillItems)
                        .WithOne(x => x.Bill)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillAccountTransaction>(e =>
                {
                    SetIdDefaultValue(e);

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

                modelBuilder.Entity<BillStudentCharge>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Bill)
                        .WithMany(x => x.BillStudentCharges)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.StudentCharge)
                        .WithMany(x => x.BillStudentCharges)
                        .HasForeignKey(x => x.StudentChargeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillDiscount>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Discount)
                        .WithMany(x => x.BillDiscounts)
                        .HasForeignKey(x => x.DiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Bill)
                        .WithMany(x => x.BillChargeDiscounts)
                        .HasForeignKey(x => x.BillId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BillItem>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<BoarderStatus>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.BoarderStatus)
                        .HasForeignKey(x => x.BoarderStatusId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Building>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Floors)
                        .WithOne(x => x.Building)
                        .HasForeignKey(x => x.BuildingId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<BuildingFloor>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Rooms)
                        .WithOne(x => x.BuildingFloor)
                        .HasForeignKey(x => x.BuildingFloorId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Bulletin>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<Charge>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ChargeBillingPeriod>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StudentCharges)
                        .WithOne(x => x.ChargeBillingPeriod)
                        .HasForeignKey(x => x.ChargeBillingPeriodId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Bills)
                        .WithOne(x => x.ChargeBillingPeriod)
                        .HasForeignKey(x => x.ChargeBillingPeriodId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ChargeDiscount>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Sessions)
                        .WithOne(x => x.Class)
                        .HasForeignKey(x => x.ClassId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Comment>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<CommentBank>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Areas)
                        .WithOne(x => x.CommentBank)
                        .HasForeignKey(x => x.CommentBankId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CommentBankArea>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Sections)
                        .WithOne(x => x.Area)
                        .HasForeignKey(x => x.CommentBankAreaId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CommentBankSection>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Comments)
                        .WithOne(x => x.Section)
                        .HasForeignKey(x => x.CommentBankSectionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CommunicationLog>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<CommunicationType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.CommunicationLogs)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.CommunicationTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Contact>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.LinkedStudents)
                        .WithOne(x => x.Contact)
                        .HasForeignKey(x => x.ContactId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Person)
                        .WithMany(x => x.Contacts)
                        .HasForeignKey(x => x.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Course>(e =>
                {
                    SetIdDefaultValue(e);

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
                    
                    e.HasMany(x => x.CommentBankAreas)
                        .WithOne(x => x.Course)
                        .HasForeignKey(x => x.CourseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CoverArrangement>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<CurriculumBand>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.CurriculumBands)
                        .HasForeignKey(x => x.StudentGroupId)
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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<CurriculumBlock>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.CurriculumGroups)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Classes)
                        .WithOne(x => x.Group)
                        .HasForeignKey(x => x.CurriculumGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<CurriculumYearGroup>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Detention)
                        .HasForeignKey(x => x.DetentionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DetentionType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Detentions)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.DetentionTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEvent>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Detentions)
                        .WithOne(x => x.Event)
                        .HasForeignKey(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Attendees)
                        .WithOne(x => x.Event)
                        .HasForeignKey(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ParentEvenings)
                        .WithOne(x => x.Event)
                        .HasForeignKey(x => x.EventId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEventAttendee>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<DiaryEventAttendeeResponse>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.DiaryEventAttendees)
                        .WithOne(x => x.Response)
                        .HasForeignKey(x => x.ResponseId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<DiaryEventTemplate>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<DiaryEventType>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.PersonDietaryRequirements)
                        .WithOne(x => x.DietaryRequirement)
                        .HasForeignKey(x => x.DietaryRequirementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Directory>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(d => d.Bulletins)
                        .WithOne(b => b.Directory)
                        .HasForeignKey(b => b.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(d => d.People)
                        .WithOne(p => p.Directory)
                        .HasForeignKey(p => p.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(d => d.HomeworkItems)
                        .WithOne(hi => hi.Directory)
                        .HasForeignKey(hi => hi.DirectoryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(d => d.LessonPlans)
                        .WithOne(lp => lp.Directory)
                        .HasForeignKey(lp => lp.DirectoryId)
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

                modelBuilder.Entity<Discount>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<Document>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<DocumentType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Documents)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<EmailAddress>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<EmailAddressType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.EmailAddresses)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<EnrolmentStatus>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.EnrolmentStatus)
                        .HasForeignKey(x => x.EnrolmentStatusId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Ethnicity>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.People)
                        .WithOne(x => x.Ethnicity)
                        .HasForeignKey(x => x.EthnicityId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessment>(e =>
                {
                    SetIdDefaultValue(e);

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

                    e.HasMany(x => x.ExamAwards)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey(x => x.AssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamBaseElements)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey(x => x.AssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamBaseComponents)
                        .WithOne(x => x.Assessment)
                        .HasForeignKey(x => x.ExamAssessmentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessmentAspect>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Aspect)
                        .WithMany(x => x.AssessmentAspects)
                        .HasForeignKey(x => x.AspectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAssessmentMode>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

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
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAwardElement>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Element)
                        .WithMany(x => x.ExamAwardElements)
                        .HasForeignKey(x => x.ElementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamAwardSeries>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamAwardSeries)
                        .HasForeignKey(x => x.AwardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamBaseComponent>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.ExamComponents)
                        .WithOne(x => x.BaseComponent)
                        .HasForeignKey(x => x.BaseComponentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamBaseElement>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.ExamSeries)
                        .WithOne(x => x.ExamBoard)
                        .HasForeignKey(x => x.ExamBoardId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamCandidate>(e =>
                {
                    SetIdDefaultValue(e);

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
                        .WithMany(x => x.ExamCandidates)
                        .HasForeignKey(x => x.StudentId)
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
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Series)
                        .WithMany(x => x.ExamCandidateSeries)
                        .HasForeignKey(x => x.SeriesId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamCandidateSpecialArrangement>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.SpecialArrangement)
                        .WithMany(x => x.Candidates)
                        .HasForeignKey(x => x.SpecialArrangementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamComponent>(e =>
                {
                    SetIdDefaultValue(e);

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
                });

                modelBuilder.Entity<ExamComponentSitting>(e =>
                {
                    SetIdDefaultValue(e);

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

                modelBuilder.Entity<ExamDate>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Session)
                        .WithMany(x => x.ExamDates)
                        .HasForeignKey(x => x.SessionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ExamComponents)
                        .WithOne(x => x.ExamDate)
                        .HasForeignKey(x => x.ExamDateId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamElement>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExamEnrolment>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExamQualification>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Levels)
                        .WithOne(x => x.Qualification)
                        .HasForeignKey(x => x.QualificationId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamQualificationLevel>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.DefaultGradeSet)
                        .WithMany(x => x.ExamQualificationLevels)
                        .HasForeignKey(x => x.DefaultGradeSetId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamResultEmbargo>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.ResultSet)
                        .WithMany(x => x.ExamResultEmbargoes)
                        .HasForeignKey(x => x.ResultSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamRoom>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Room)
                        .WithMany(x => x.ExamRooms)
                        .HasForeignKey(x => x.RoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.SeatBlocks)
                        .WithOne(x => x.ExamRoom)
                        .HasForeignKey(x => x.ExamRoomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExamSeason>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExamSeries>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExamSession>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExamSpecialArrangement>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<Exclusion>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.Exclusions)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExclusionType)
                        .WithMany(x => x.Exclusions)
                        .HasForeignKey(x => x.ExclusionTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ExclusionReason)
                        .WithMany(x => x.Exclusions)
                        .HasForeignKey(x => x.ExclusionReasonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.AppealResult)
                        .WithMany(x => x.Exclusions)
                        .HasForeignKey(x => x.AppealResultId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ExclusionAppealResult>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExclusionReason>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ExclusionType>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<File>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Documents)
                        .WithOne(x => x.Attachment)
                        .HasForeignKey(x => x.FileId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<GiftedTalented>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<GovernanceType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.GovernanceType)
                        .HasForeignKey(x => x.GovernanceTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Grade>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Results)
                        .WithOne(x => x.Grade)
                        .HasForeignKey(x => x.GradeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<GradeSet>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Submissions)
                        .WithOne(x => x.HomeworkItem)
                        .HasForeignKey(x => x.HomeworkId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<HomeworkSubmission>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Task)
                        .WithMany(x => x.HomeworkSubmissions)
                        .HasForeignKey(x => x.TaskId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.HomeworkSubmissions)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.SubmittedWork)
                        .WithMany(x => x.HomeworkSubmissions)
                        .HasForeignKey(x => x.DocumentId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<House>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.Houses)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Incident>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.InvolvedStudents)
                        .WithOne(x => x.Incident)
                        .HasForeignKey(x => x.IncidentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentIncidentDetention>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<IncidentType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.BehaviourTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<IntakeType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.IntakeType)
                        .HasForeignKey(x => x.IntakeTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Language>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<LessonPlan>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<LessonPlanHomeworkItem>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.HomeworkItem)
                        .WithMany(x => x.LessonPlanHomeworkItems)
                        .HasForeignKey(x => x.HomeworkItemId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.LessonPlan)
                        .WithMany(x => x.HomeworkItems)
                        .HasForeignKey(x => x.LessonPlanId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<LessonPlanTemplate>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<LocalAuthority>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.LocalAuthority)
                        .HasForeignKey(x => x.LocalAuthorityId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Location>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.BehaviourAchievements)
                        .WithOne(x => x.Location)
                        .HasForeignKey(x => x.LocationId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.BehaviourIncidents)
                        .WithOne(x => x.Location)
                        .HasForeignKey(x => x.LocationId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<LogNote>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<LogNoteType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.LogNotes)
                        .WithOne(x => x.LogNoteType)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MarksheetColumn>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<MarksheetTemplate>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Columns)
                        .WithOne(x => x.Template)
                        .HasForeignKey(x => x.TemplateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Marksheets)
                        .WithOne(x => x.Template)
                        .HasForeignKey(x => x.MarksheetTemplateId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Marksheet>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.Marksheets)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MedicalCondition>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.PersonConditions)
                        .WithOne(x => x.MedicalCondition)
                        .HasForeignKey(x => x.ConditionId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<MedicalEvent>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<NextOfKin>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StaffMember)
                        .WithMany(x => x.NextOfKin)
                        .HasForeignKey(x => x.StaffMemberId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.NextOfKinPerson)
                        .WithMany(x => x.RelatedStaff)
                        .HasForeignKey(x => x.NextOfKinPersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<NextOfKinRelationshipType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.NextOfKin)
                        .WithOne(x => x.RelationshipType)
                        .HasForeignKey(x => x.RelationshipTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Observation>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ObservationOutcome>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Observations)
                        .WithOne(x => x.Outcome)
                        .HasForeignKey(x => x.OutcomeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ParentEvening>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StaffMembers)
                        .WithOne(x => x.ParentEvening)
                        .HasForeignKey(x => x.ParentEveningId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ParentEveningAppointment>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ParentEveningBreak>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ParentEveningGroup>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<ParentEveningStaffMember>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Appointments)
                        .WithOne(x => x.ParentEveningStaffMember)
                        .HasForeignKey(x => x.ParentEveningStaffId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Breaks)
                        .WithOne(x => x.ParentEveningStaffMember)
                        .HasForeignKey(x => x.ParentEveningStaffMemberId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Person>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(p => p.AddressLinks)
                        .WithOne(a => a.Person)
                        .HasForeignKey(a => a.PersonId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.EmailAddresses)
                        .WithOne(ea => ea.Person)
                        .HasForeignKey(ea => ea.PersonId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.MedicalConditions)
                        .WithOne(pc => pc.Person)
                        .HasForeignKey(pc => pc.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(p => p.PhoneNumbers)
                        .WithOne(pn => pn.Person)
                        .HasForeignKey(pn => pn.PersonId)
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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<PersonDietaryRequirement>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<PhoneNumber>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<PhoneNumberType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.PhoneNumbers)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Photo>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.People)
                        .WithOne(x => x.Photo)
                        .HasForeignKey(x => x.PhotoId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Product>(e =>
                {
                    SetIdDefaultValue(e);

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

                modelBuilder.Entity<StoreDiscount>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Product)
                        .WithMany(x => x.ProductDiscounts)
                        .HasForeignKey(x => x.ProductId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ProductType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Products)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.ProductTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ProductTypeDiscounts)
                        .WithOne(x => x.ProductType)
                        .HasForeignKey(x => x.ProductTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<RegGroup>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.RegGroups)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<RelationshipType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StudentContacts)
                        .WithOne(x => x.RelationshipType)
                        .HasForeignKey(x => x.RelationshipTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentAgents)
                        .WithOne(x => x.RelationshipType)
                        .HasForeignKey(x => x.RelationshipTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCard>(e =>
                {
                    SetIdDefaultValue(e);

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

                modelBuilder.Entity<ReportCardEntry>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.TargetSubmissions)
                        .WithOne(x => x.Entry)
                        .HasForeignKey(x => x.EntryId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.CreatedBy)
                        .WithMany(x => x.ReportCardSubmissions)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCardTarget>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Submissions)
                        .WithOne(x => x.Target)
                        .HasForeignKey(x => x.TargetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<ReportCardTargetEntry>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<Result>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<ResultSet>(e =>
                {
                    SetIdDefaultValue(e);

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

                    e.HasMany(x => x.RoleClaims)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired();
                });

                modelBuilder.Entity<RoleClaim>(e =>
                {
                    e.ToTable("RoleClaims");
                });

                modelBuilder.Entity<Room>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<RoomClosureReason>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Closures)
                        .WithOne(x => x.Reason)
                        .HasForeignKey(x => x.ReasonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<School>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Agency)
                        .WithMany(x => x.Schools)
                        .HasForeignKey(x => x.AgencyId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SchoolPhase>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.SchoolPhase)
                        .HasForeignKey(x => x.PhaseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SchoolType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Schools)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenEvent>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<SenEventType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Events)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.EventTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenProvision>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<SenProvisionType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.SenProvisions)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.ProvisionTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenReview>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<SenReviewType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Reviews)
                        .WithOne(x => x.ReviewType)
                        .HasForeignKey(x => x.ReviewTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SenStatus>(e =>
                {
                    SetIdDefaultValue(e);
                    
                    e.HasMany(x => x.Students)
                        .WithOne(x => x.SenStatus)
                        .HasForeignKey(x => x.SenStatusId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.Property(x => x.Code)
                        .IsFixedLength()
                        .IsUnicode(false);
                });

                modelBuilder.Entity<SenType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Students)
                        .WithOne(x => x.SenType)
                        .HasForeignKey(x => x.SenTypeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Session>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<StaffAbsence>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<StaffAbsenceType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Absences)
                        .WithOne(x => x.AbsenceType)
                        .HasForeignKey(x => x.AbsenceTypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StaffIllnessType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Absences)
                        .WithOne(x => x.IllnessType)
                        .HasForeignKey(x => x.IllnessTypeId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StaffMember>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(sm => sm.Sessions)
                        .WithOne(s => s.Teacher)
                        .HasForeignKey(s => s.TeacherId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(sm => sm.Person)
                        .WithMany(p => p.StaffMembers)
                        .HasForeignKey(sm => sm.PersonId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentGroupSupervisors)
                        .WithOne(x => x.Supervisor)
                        .HasForeignKey(x => x.SupervisorId)
                        .IsRequired()
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

                    e.HasMany(x => x.ParentEvenings)
                        .WithOne(x => x.StaffMember)
                        .HasForeignKey(x => x.StaffMemberId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StoreDiscount>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<Student>(e =>
                {
                    SetIdDefaultValue(e);

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

                    e.HasMany(x => x.StudentAchievements)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentIncidents)
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
                        .WithMany(x => x.Students)
                        .HasForeignKey(x => x.PersonId)
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

                    e.HasMany(x => x.StudentGroupMemberships)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.ParentEveningAppointments)
                        .WithOne(x => x.Student)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.Property(x => x.Upn)
                        .IsUnicode(false);
                });

                modelBuilder.Entity<StudentAchievement>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Achievement)
                        .WithMany(x => x.InvolvedStudents)
                        .HasForeignKey(x => x.AchievementId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentAgentRelationship>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.AgentRelationships)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentCharge>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

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

                modelBuilder.Entity<StudentChargeDiscount>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.Student)
                        .WithMany(x => x.ChargeDiscounts)
                        .HasForeignKey(x => x.StudentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.ChargeDiscount)
                        .WithMany(x => x.StudentChargeDiscounts)
                        .HasForeignKey(x => x.ChargeDiscountId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentGroup>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasOne(x => x.PromoteToGroup)
                        .WithMany(x => x.PromotionSourceGroups)
                        .HasForeignKey(x => x.PromoteToGroupId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.MainSupervisor)
                        .WithMany(x => x.MainGroups)
                        .HasForeignKey(x => x.MainSupervisorId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentMemberships)
                        .WithOne(x => x.StudentGroup)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.StudentGroupSupervisors)
                        .WithOne(x => x.StudentGroup)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudentGroupMembership>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<StudentGroupSupervisor>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<StudentIncident>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.LinkedDetentions)
                        .WithOne(x => x.StudentIncident)
                        .HasForeignKey(x => x.StudentIncidentId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<StudyTopic>(e =>
                {
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);

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
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<SubjectCodeSet>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.SubjectCodes)
                        .WithOne(x => x.SubjectCodeSet)
                        .HasForeignKey(x => x.SubjectCodeSetId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<SubjectStaffMember>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<SubjectStaffMemberRole>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.StaffMembers)
                        .WithOne(x => x.Role)
                        .HasForeignKey(x => x.RoleId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<Task>(e => { SetIdDefaultValue(e); });

                modelBuilder.Entity<TaskType>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Tasks)
                        .WithOne(x => x.Type)
                        .HasForeignKey(x => x.TypeId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<TrainingCertificate>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<TrainingCertificateStatus>(e =>
                {
                    SetIdDefaultValue(e);

                    e.HasMany(x => x.Certificates)
                        .WithOne(x => x.Status)
                        .HasForeignKey(x => x.StatusId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });

                modelBuilder.Entity<TrainingCourse>(e =>
                {
                    SetIdDefaultValue(e);

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
                        .WithMany(x => x.Users)
                        .HasForeignKey(x => x.PersonId)
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Results)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
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

                    e.HasMany(x => x.Documents)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.MedicalEvents)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Incidents)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Achievements)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.LessonPlans)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasMany(x => x.Bulletins)
                        .WithOne(x => x.CreatedBy)
                        .HasForeignKey(x => x.CreatedById)
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

                modelBuilder.Entity<VatRate>(e =>
                {
                    SetIdDefaultValue(e);
                });

                modelBuilder.Entity<YearGroup>(e =>
                {
                    SetIdDefaultValue(e);

                    SetIdDefaultValue(e);

                    e.HasMany(x => x.RegGroups)
                        .WithOne(x => x.YearGroup)
                        .HasForeignKey(x => x.YearGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.StudentGroup)
                        .WithMany(x => x.YearGroups)
                        .HasForeignKey(x => x.StudentGroupId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                });
            }
        }

        private void SetIdDefaultValue(EntityTypeBuilder builder)
        {
            builder.Property("Id").HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}