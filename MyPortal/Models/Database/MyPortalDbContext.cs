using System.Data.Common;

namespace MyPortal.Models.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyPortalDbContext : DbContext
    {
        public bool IsDebug { get; set; }
        public MyPortalDbContext()
            : base("name=MyPortalDbContext")
        {
            IsDebug = false;
        }
        
        public MyPortalDbContext(DbConnection connection) : base(connection, true)
        {
            IsDebug = false;
        }

        public virtual DbSet<AssessmentGrade> AssessmentGrades { get; set; }
        public virtual DbSet<AssessmentGradeSet> AssessmentGradeSets { get; set; }
        public virtual DbSet<AssessmentResult> AssessmentResults { get; set; }
        public virtual DbSet<AssessmentResultSet> AssessmentResultSets { get; set; }
        public virtual DbSet<AttendanceRegisterCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceRegisterMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendanceRegisterCodeMeaning> AttendanceMeanings { get; set; }
        public virtual DbSet<AttendancePeriod> AttendancePeriods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<StaffDocument> StaffDocuments { get; set; }
        public virtual DbSet<StudentDocument> StudentDocuments { get; set; }
        public virtual DbSet<StaffMember> StaffMembers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<CurriculumAcademicYear> CurriculumAcademicYears { get; set; }
        public virtual DbSet<CurriculumClassEnrolment> CurriculumClassEnrolments { get; set; }
        public virtual DbSet<CurriculumClass> CurriculumClasses { get; set; }
        public virtual DbSet<CurriculumClassPeriod> CurriculumClassPeriods { get; set; }
        public virtual DbSet<CurriculumLessonPlan> CurriculumLessonPlans { get; set; }
        public virtual DbSet<CurriculumLessonPlanTemplate> CurriculumLessonPlanTemplates { get; set; }
        public virtual DbSet<CurriculumStudyTopic> CurriculumStudyTopics { get; set; }
        public virtual DbSet<CurriculumSubject> CurriculumSubjects { get; set; }
        public virtual DbSet<FinanceBasketItem> FinanceBasketItems { get; set; }
        public virtual DbSet<FinanceProduct> FinanceProducts { get; set; }
        public virtual DbSet<FinanceSale> FinanceSales { get; set; }
        public virtual DbSet<PastoralRegGroup> PastoralRegGroups { get; set; }
        public virtual DbSet<PastoralYearGroup> PastoralYearGroups { get; set; }
        public virtual DbSet<PersonnelObservation> PersonnelObservations { get; set; }
        public virtual DbSet<PersonnelTrainingCertificate> PersonnelTrainingCertificates { get; set; }
        public virtual DbSet<PersonnelTrainingCourse> PersonnelTrainingCourses { get; set; }
        public virtual DbSet<PersonnelTrainingStatus> PersonnelTrainingStatuses { get; set; }
        public virtual DbSet<ProfileCommentBank> ProfileCommentBanks { get; set; }
        public virtual DbSet<ProfileComment> ProfileComments { get; set; }
        public virtual DbSet<ProfileLog> ProfileLogs { get; set; }
        public virtual DbSet<ProfileLogType> ProfileLogTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssessmentGrade>()
                .Property(e => e.GradeValue)
                .IsUnicode(false);

            modelBuilder.Entity<AssessmentGradeSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AssessmentGradeSet>()
                .HasMany(e => e.AssessmentGrades)
                .WithRequired(e => e.AssessmentGradeSet)
                .HasForeignKey(e => e.GradeSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AssessmentResult>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<AssessmentResultSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AssessmentResultSet>()
                .HasMany(e => e.AssessmentResults)
                .WithRequired(e => e.AssessmentResultSet)
                .HasForeignKey(e => e.ResultSetId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendanceRegisterCode>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceRegisterCode>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceRegisterMark>()
                .Property(e => e.Mark)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceRegisterCodeMeaning>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceRegisterCodeMeaning>()
                .HasMany(e => e.AttendanceRegisterCodes)
                .WithRequired(e => e.AttendanceRegisterCodeMeaning)
                .HasForeignKey(e => e.MeaningId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendancePeriod>()
                .Property(e => e.Weekday)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendancePeriod>()
                .Property(e => e.StartTime)
                .HasPrecision(2);

            modelBuilder.Entity<AttendancePeriod>()
                .Property(e => e.EndTime)
                .HasPrecision(2);

            modelBuilder.Entity<AttendancePeriod>()
                .HasMany(e => e.AttendanceRegisterMarks)
                .WithRequired(e => e.AttendancePeriod)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendancePeriod>()
                .HasMany(e => e.CurriculumClassPeriods)
                .WithRequired(e => e.AttendancePeriod)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<AttendanceWeek>()
                .HasMany(e => e.AttendanceRegisterMarks)
                .WithRequired(e => e.AttendanceWeek)
                .HasForeignKey(e => e.WeekId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.StaffDocuments)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.StudentDocuments)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.JobTitle)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Uploader)
                .HasForeignKey(e => e.UploaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.StaffDocuments)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.Teacher)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumLessonPlansAuthored)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.ProfileLogsWritten)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralRegGroups)
                .WithRequired(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservationsOwn)
                .WithRequired(e => e.Observee)
                .HasForeignKey(e => e.ObserveeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelObservationsObserved)
                .WithRequired(e => e.Observer)
                .HasForeignKey(e => e.ObserverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.CurriculumSubjectsLeading)
                .WithRequired(e => e.Leader)
                .HasForeignKey(e => e.LeaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithRequired(e => e.CoreStaffMember)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffMember>()
                .HasMany(e => e.PastoralYearGroups)
                .WithRequired(e => e.HeadOfYear)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.CandidateNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.AccountBalance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Student>()
                .Property(e => e.MisId)
                .IsUnicode(false);

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
                .HasMany(e => e.Enrolments)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentDocuments)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinanceBasketItems)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.FinanceSales)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.AttendanceWeeks)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.FinanceSales)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumAcademicYear>()
                .HasMany(e => e.AssessmentResultSets)
                .WithRequired(e => e.CurriculumAcademicYear)
                .HasForeignKey(e => e.AcademicYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumClass>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumClass>()
                .HasMany(e => e.Enrolments)
                .WithRequired(e => e.CurriculumClass)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumClass>()
                .HasMany(e => e.CurriculumClassPeriods)
                .WithRequired(e => e.CurriculumClass)
                .HasForeignKey(e => e.ClassId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumLessonPlan>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumLessonPlanTemplate>()
                .Property(e => e.Name)
                .IsUnicode(false);
                        
            modelBuilder.Entity<CurriculumStudyTopic>()
                .HasMany(e => e.LessonPlans)
                .WithRequired(e => e.StudyTopic)
                .HasForeignKey(e => e.StudyTopicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumSubject>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.AssessmentResults)
                .WithRequired(e => e.CurriculumSubject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.CurriculumSubject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CurriculumSubject>()
                .HasMany(e => e.CurriculumStudyTopics)
                .WithRequired(e => e.CurriculumSubject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceProduct>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<FinanceProduct>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<FinanceProduct>()
                .HasMany(e => e.FinanceBasketItems)
                .WithRequired(e => e.FinanceProduct)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceProduct>()
                .HasMany(e => e.FinanceSales)
                .WithRequired(e => e.FinanceProduct)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinanceSale>()
                .Property(e => e.AmountPaid)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PastoralRegGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PastoralRegGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.PastoralRegGroup)
                .HasForeignKey(e => e.RegGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.CoreStudents)
                .WithRequired(e => e.PastoralYearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.CurriculumClasses)
                .WithOptional(e => e.PastoralYearGroup)
                .HasForeignKey(e => e.YearGroupId);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.CurriculumStudyTopics)
                .WithRequired(e => e.PastoralYearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PastoralYearGroup>()
                .HasMany(e => e.PastoralRegGroups)
                .WithRequired(e => e.PastoralYearGroup)
                .HasForeignKey(e => e.YearGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonnelObservation>()
                .Property(e => e.Outcome)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelTrainingCourse>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelTrainingCourse>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelTrainingCourse>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithRequired(e => e.PersonnelTrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonnelTrainingStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PersonnelTrainingStatus>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithRequired(e => e.PersonnelTrainingStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfileCommentBank>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProfileCommentBank>()
                .HasMany(e => e.ProfileComments)
                .WithRequired(e => e.ProfileCommentBank)
                .HasForeignKey(e => e.CommentBankId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfileLogType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ProfileLogType>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.ProfileLogType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
