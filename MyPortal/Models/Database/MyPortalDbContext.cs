using System.Data.Common;

namespace MyPortal.Models.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyPortalDbContext : DbContext
    {
        public MyPortalDbContext()
            : base("name=MyPortalDbContext")
        {
        }
        
        public MyPortalDbContext(DbConnection connection) : base(connection, true)
        {
        }

        public virtual DbSet<AssessmentGrade> AssessmentGrades { get; set; }
        public virtual DbSet<AssessmentGradeSet> AssessmentGradeSets { get; set; }
        public virtual DbSet<AssessmentResult> AssessmentResults { get; set; }
        public virtual DbSet<AssessmentResultSet> AssessmentResultSets { get; set; }
        public virtual DbSet<AttendanceCode> AttendanceCodes { get; set; }
        public virtual DbSet<AttendanceMark> AttendanceMarks { get; set; }
        public virtual DbSet<AttendanceMeaning> AttendanceMeanings { get; set; }
        public virtual DbSet<AttendancePeriod> AttendancePeriods { get; set; }
        public virtual DbSet<AttendanceWeek> AttendanceWeeks { get; set; }
        public virtual DbSet<CoreDocument> CoreDocuments { get; set; }
        public virtual DbSet<CoreStaffDocument> CoreStaffDocuments { get; set; }
        public virtual DbSet<CoreStudentDocument> CoreStudentDocuments { get; set; }
        public virtual DbSet<CoreStaffMember> CoreStaff { get; set; }
        public virtual DbSet<CoreStudent> CoreStudents { get; set; }
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
                .Property(e => e.Grade)
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

            modelBuilder.Entity<AttendanceCode>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceCode>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceMark>()
                .Property(e => e.Mark)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceMeaning>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AttendanceMeaning>()
                .HasMany(e => e.AttendanceCodes)
                .WithRequired(e => e.AttendanceMeaning)
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
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.AttendancePeriod)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttendancePeriod>()
                .HasMany(e => e.CurriculumClassPeriods)
                .WithRequired(e => e.AttendancePeriod)
                .HasForeignKey(e => e.PeriodId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreDocument>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CoreDocument>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<CoreDocument>()
                .HasMany(e => e.CoreStaffDocuments)
                .WithRequired(e => e.CoreDocument)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreDocument>()
                .HasMany(e => e.CoreStudentDocuments)
                .WithRequired(e => e.CoreDocument)
                .HasForeignKey(e => e.DocumentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.JobTitle)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.CoreDocuments)
                .WithRequired(e => e.Uploader)
                .HasForeignKey(e => e.UploaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.CoreStaffDocuments)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.CurriculumClasses)
                .WithRequired(e => e.CoreStaffMember)
                .HasForeignKey(e => e.TeacherId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.CurriculumLessonPlansAuthored)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.ProfileLogsWritten)
                .WithRequired(e => e.CoreStaffMember)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.PastoralRegGroups)
                .WithRequired(e => e.Tutor)
                .HasForeignKey(e => e.TutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.PersonnelObservationsOwn)
                .WithRequired(e => e.Observee)
                .HasForeignKey(e => e.ObserveeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.PersonnelObservationsObserved)
                .WithRequired(e => e.Observer)
                .HasForeignKey(e => e.ObserverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.CurriculumSubjectsLeading)
                .WithRequired(e => e.Leader)
                .HasForeignKey(e => e.LeaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.PersonnelTrainingCertificates)
                .WithRequired(e => e.CoreStaffMember)
                .HasForeignKey(e => e.StaffId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStaffMember>()
                .HasMany(e => e.PastoralYearGroups)
                .WithRequired(e => e.CoreStaffMember)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.CandidateNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.AccountBalance)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CoreStudent>()
                .Property(e => e.MisId)
                .IsUnicode(false);

            modelBuilder.Entity<CoreStudent>()
                .HasMany(e => e.AssessmentResults)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
                .HasMany(e => e.AttendanceMarks)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
                .HasMany(e => e.CoreStudentDocuments)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
                .HasMany(e => e.FinanceBasketItems)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
                .HasMany(e => e.ProfileLogs)
                .WithRequired(e => e.CoreStudent)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CoreStudent>()
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

            modelBuilder.Entity<CurriculumClass>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CurriculumClass>()
                .HasMany(e => e.CurriculumClassEnrolments)
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
