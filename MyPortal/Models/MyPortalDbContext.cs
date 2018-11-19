using System.Data.Entity;

namespace MyPortal.Models
{
    public class MyPortalDbContext : DbContext
    {
        public MyPortalDbContext()
            : base("name=MyPortalDbContext")
        {
        }

        public virtual DbSet<BasketItem> BasketItems { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<GradeSet> GradeSets { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<LogType> LogTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RegGroup> RegGroups { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<ResultSet> ResultSets { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffDocument> StaffDocuments { get; set; }
        public virtual DbSet<StaffObservation> StaffObservations { get; set; }
        public virtual DbSet<StudentDocument> StudentDocuments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TrainingCertificate> TrainingCertificates { get; set; }
        public virtual DbSet<TrainingCourse> TrainingCourses { get; set; }
        public virtual DbSet<TrainingStatus> TrainingStatuses { get; set; }
        public virtual DbSet<YearGroup> YearGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.StaffDocuments)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Grade>()
                .Property(e => e.GradeValue)
                .IsUnicode(false);

            modelBuilder.Entity<GradeSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<GradeSet>()
                .HasMany(e => e.Grades)
                .WithRequired(e => e.GradeSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LogType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LogType>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.LogType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.BasketItems)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RegGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RegGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.RegGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<ResultSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ResultSet>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.ResultSet)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.JobTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.UploaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.RegGroups)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.TutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.StaffDocuments)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.StaffObservations)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.ObserveeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.StaffObservations1)
                .WithRequired(e => e.Staff1)
                .HasForeignKey(e => e.ObserverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.LeaderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.TrainingCertificates)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .HasMany(e => e.YearGroups)
                .WithRequired(e => e.Staff)
                .HasForeignKey(e => e.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffObservation>()
                .Property(e => e.Outcome)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.LastName)
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
                .HasMany(e => e.BasketItems)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Logs)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Sales)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentDocuments)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Results)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrainingCourse>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingCourse>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingCourse>()
                .HasMany(e => e.TrainingCertificates)
                .WithRequired(e => e.TrainingCourse)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrainingStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TrainingStatus>()
                .HasMany(e => e.TrainingCertificates)
                .WithRequired(e => e.TrainingStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YearGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.RegGroups)
                .WithRequired(e => e.YearGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YearGroup>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.YearGroup)
                .WillCascadeOnDelete(false);
        }
    }
}