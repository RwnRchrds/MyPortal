namespace MyPortal.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "curriculum.AcademicYear",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    FirstDate = c.DateTime(nullable: false, storeType: "date"),
                    LastDate = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "behaviour.Achievement",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AcademicYearId = c.Int(nullable: false),
                    AchievementTypeId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    LocationId = c.Int(nullable: false),
                    RecordedById = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Comments = c.String(),
                    Points = c.Int(nullable: false),
                    Resolved = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("school.Location", t => t.LocationId)
                .ForeignKey("person.StaffMember", t => t.RecordedById)
                .ForeignKey("person.Student", t => t.StudentId)
                .ForeignKey("behaviour.AchievementType", t => t.AchievementTypeId)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.AchievementTypeId)
                .Index(t => t.StudentId)
                .Index(t => t.LocationId)
                .Index(t => t.RecordedById);

            CreateTable(
                "school.Location",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "behaviour.Incident",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AcademicYearId = c.Int(nullable: false),
                    BehaviourTypeId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    LocationId = c.Int(nullable: false),
                    RecordedById = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Comments = c.String(),
                    Points = c.Int(nullable: false),
                    Resolved = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.StaffMember", t => t.RecordedById)
                .ForeignKey("person.Student", t => t.StudentId)
                .ForeignKey("behaviour.IncidentType", t => t.BehaviourTypeId)
                .ForeignKey("school.Location", t => t.LocationId)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.BehaviourTypeId)
                .Index(t => t.StudentId)
                .Index(t => t.LocationId)
                .Index(t => t.RecordedById);

            CreateTable(
                "person.StaffMember",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    NiNumber = c.String(maxLength: 128),
                    PostNominal = c.String(maxLength: 128),
                    TeachingStaff = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.Code, unique: true);

            CreateTable(
                "system.Bulletin",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AuthorId = c.Int(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    ExpireDate = c.DateTime(),
                    Title = c.String(nullable: false, maxLength: 128),
                    Detail = c.String(nullable: false),
                    ShowStudents = c.Boolean(nullable: false),
                    Approved = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.StaffMember", t => t.AuthorId)
                .Index(t => t.AuthorId);

            CreateTable(
                "curriculum.Class",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AcademicYearId = c.Int(nullable: false),
                    SubjectId = c.Int(),
                    Name = c.String(nullable: false, maxLength: 128),
                    TeacherId = c.Int(nullable: false),
                    YearGroupId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("curriculum.Subject", t => t.SubjectId)
                .ForeignKey("pastoral.YearGroup", t => t.YearGroupId)
                .ForeignKey("person.StaffMember", t => t.TeacherId)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "curriculum.Enrolment",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ClassId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.Student", t => t.StudentId)
                .ForeignKey("curriculum.Class", t => t.ClassId)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId);

            CreateTable(
                "person.Student",
                c => new
                {
                    Id = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                    RegGroupId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                    HouseId = c.Int(),
                    CandidateNumber = c.String(maxLength: 128),
                    AdmissionNumber = c.Int(nullable: false, identity: true),
                    DateStarting = c.DateTime(storeType: "date"),
                    DateLeaving = c.DateTime(storeType: "date"),
                    AccountBalance = c.Decimal(nullable: false, precision: 10, scale: 2),
                    FreeSchoolMeals = c.Boolean(nullable: false),
                    GiftedAndTalented = c.Boolean(nullable: false),
                    SenStatusId = c.Int(),
                    PupilPremium = c.Boolean(nullable: false),
                    MisId = c.String(maxLength: 256),
                    Upn = c.String(maxLength: 13, unicode: false),
                    Uci = c.String(),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("pastoral.RegGroup", t => t.RegGroupId)
                .ForeignKey("pastoral.YearGroup", t => t.YearGroupId)
                .ForeignKey("pastoral.House", t => t.HouseId)
                .ForeignKey("person.People", t => t.PersonId)
                .ForeignKey("sen.Status", t => t.SenStatusId)
                .Index(t => t.PersonId)
                .Index(t => t.RegGroupId)
                .Index(t => t.YearGroupId)
                .Index(t => t.HouseId)
                .Index(t => t.SenStatusId);

            CreateTable(
                "attendance.Mark",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    WeekId = c.Int(nullable: false),
                    PeriodId = c.Int(nullable: false),
                    Mark = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Comments = c.String(maxLength: 256),
                    MinutesLate = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("attendance.Period", t => t.PeriodId)
                .ForeignKey("attendance.Week", t => t.WeekId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.WeekId)
                .Index(t => t.PeriodId);

            CreateTable(
                "attendance.Period",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Weekday = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128),
                    StartTime = c.Time(nullable: false, precision: 2),
                    EndTime = c.Time(nullable: false, precision: 2),
                    IsAm = c.Boolean(nullable: false),
                    IsPm = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "curriculum.Session",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ClassId = c.Int(nullable: false),
                    PeriodId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("attendance.Period", t => t.PeriodId)
                .ForeignKey("curriculum.Class", t => t.ClassId)
                .Index(t => t.ClassId)
                .Index(t => t.PeriodId);

            CreateTable(
                "attendance.Week",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AcademicYearId = c.Int(nullable: false),
                    Beginning = c.DateTime(nullable: false, storeType: "date"),
                    IsHoliday = c.Boolean(nullable: false),
                    IsNonTimetable = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "finance.BasketItem",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("finance.Product", t => t.ProductId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ProductId);

            CreateTable(
                "finance.Product",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProductTypeId = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 256),
                    Price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    Visible = c.Boolean(nullable: false),
                    OnceOnly = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("finance.ProductType", t => t.ProductTypeId)
                .Index(t => t.ProductTypeId);

            CreateTable(
                "finance.Sale",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                    AcademicYearId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    AmountPaid = c.Decimal(nullable: false, precision: 10, scale: 2),
                    Processed = c.Boolean(nullable: false),
                    Refunded = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("finance.Product", t => t.ProductId)
                .ForeignKey("person.Student", t => t.StudentId)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.StudentId)
                .Index(t => t.ProductId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "finance.ProductType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                    IsMeal = c.Boolean(nullable: false),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "sen.GiftedTalented",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    SubjectId = c.Int(nullable: false),
                    Notes = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("curriculum.Subject", t => t.SubjectId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);

            CreateTable(
                "curriculum.Subject",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Code = c.String(nullable: false, maxLength: 128),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "curriculum.SubjectStaffMember",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubjectId = c.Int(nullable: false),
                    StaffMemberId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("curriculum.SubjectStaffMemberRole", t => t.RoleId)
                .ForeignKey("curriculum.Subject", t => t.SubjectId)
                .ForeignKey("person.StaffMember", t => t.StaffMemberId)
                .Index(t => t.SubjectId)
                .Index(t => t.StaffMemberId)
                .Index(t => t.RoleId);

            CreateTable(
                "curriculum.SubjectStaffMemberRole",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "curriculum.StudyTopic",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubjectId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("pastoral.YearGroup", t => t.YearGroupId)
                .ForeignKey("curriculum.Subject", t => t.SubjectId)
                .Index(t => t.SubjectId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "curriculum.LessonPlan",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudyTopicId = c.Int(nullable: false),
                    AuthorId = c.Int(nullable: false),
                    Title = c.String(nullable: false, maxLength: 256),
                    LearningObjectives = c.String(nullable: false),
                    PlanContent = c.String(nullable: false),
                    Homework = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("curriculum.StudyTopic", t => t.StudyTopicId)
                .ForeignKey("person.StaffMember", t => t.AuthorId)
                .Index(t => t.StudyTopicId)
                .Index(t => t.AuthorId);

            CreateTable(
                "pastoral.YearGroup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    HeadId = c.Int(nullable: false),
                    KeyStage = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.StaffMember", t => t.HeadId)
                .Index(t => t.HeadId);

            CreateTable(
                "pastoral.RegGroup",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    TutorId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("pastoral.YearGroup", t => t.YearGroupId)
                .ForeignKey("person.StaffMember", t => t.TutorId)
                .Index(t => t.TutorId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "pastoral.House",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    HeadId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.StaffMember", t => t.HeadId)
                .Index(t => t.HeadId);

            CreateTable(
                "medical.Event",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    RecordedById = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Note = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.StaffMember", t => t.RecordedById, cascadeDelete: true)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.RecordedById);

            CreateTable(
                "person.People",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 128),
                    FirstName = c.String(nullable: false, maxLength: 256),
                    MiddleName = c.String(maxLength: 256),
                    PhotoId = c.Int(),
                    NhsNumber = c.String(maxLength: 256),
                    LastName = c.String(nullable: false, maxLength: 256),
                    Gender = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Dob = c.DateTime(storeType: "date"),
                    Deceased = c.DateTime(storeType: "date"),
                    UserId = c.String(),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "communication.AddressPerson",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AddressId = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("communication.Address", t => t.AddressId)
                .ForeignKey("person.People", t => t.AddressId)
                .Index(t => t.AddressId);

            CreateTable(
                "communication.Address",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HouseNumber = c.String(),
                    HouseName = c.String(),
                    Apartment = c.String(),
                    Street = c.String(nullable: false, maxLength: 256),
                    District = c.String(maxLength: 256),
                    Town = c.String(nullable: false, maxLength: 256),
                    County = c.String(nullable: false, maxLength: 256),
                    Postcode = c.String(nullable: false, maxLength: 128),
                    Country = c.String(nullable: false, maxLength: 128),
                    Validated = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "person.Contact",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    ParentalBallot = c.Boolean(nullable: false),
                    PlaceOfWork = c.String(),
                    JobTitle = c.String(),
                    NiNumber = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.PersonId);

            CreateTable(
                "person.StudentContact",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ContactTypeId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    ContactId = c.Int(nullable: false),
                    Correspondence = c.Boolean(nullable: false),
                    ParentalResponsibility = c.Boolean(nullable: false),
                    PupilReport = c.Boolean(nullable: false),
                    CourtOrder = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("person.Contact", t => t.ContactId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ContactId);

            CreateTable(
                "document.PersonDietaryRequirement",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    DietaryRequirementId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("medical.DietaryRequirement", t => t.DietaryRequirementId)
                .ForeignKey("person.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.DietaryRequirementId);

            CreateTable(
                "medical.DietaryRequirement",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "communication.EmailAddress",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                    Address = c.String(nullable: false, maxLength: 128),
                    Main = c.Boolean(nullable: false),
                    Primary = c.Boolean(nullable: false),
                    Notes = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("communication.EmailAddressType", t => t.TypeId)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.TypeId)
                .Index(t => t.PersonId);

            CreateTable(
                "communication.EmailAddressType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "medical.PersonCondition",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    ConditionId = c.Int(nullable: false),
                    MedicationTaken = c.Boolean(nullable: false),
                    Medication = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("medical.Condition", t => t.ConditionId)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.ConditionId);

            CreateTable(
                "medical.Condition",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "document.PersonAttachment",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    DocumentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("document.Document", t => t.DocumentId)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.DocumentId);

            CreateTable(
                "document.Document",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 256),
                    Url = c.String(nullable: false),
                    UploaderId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    IsGeneral = c.Boolean(nullable: false),
                    Approved = c.Boolean(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("document.Type", t => t.TypeId)
                .ForeignKey("person.StaffMember", t => t.UploaderId)
                .Index(t => t.TypeId)
                .Index(t => t.UploaderId);

            CreateTable(
                "document.Type",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "communication.PhoneNumber",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                    Number = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("communication.PhoneNumberType", t => t.TypeId)
                .ForeignKey("person.People", t => t.PersonId)
                .Index(t => t.TypeId)
                .Index(t => t.PersonId);

            CreateTable(
                "communication.PhoneNumberType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "school.School",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    LocalAuthorityId = c.Int(nullable: false),
                    EstablishmentNumber = c.Int(nullable: false),
                    Urn = c.String(nullable: false, maxLength: 128),
                    Uprn = c.String(nullable: false, maxLength: 128),
                    PhaseId = c.Int(nullable: false),
                    TypeId = c.Int(nullable: false),
                    GovernanceTypeId = c.Int(nullable: false),
                    IntakeTypeId = c.Int(nullable: false),
                    HeadTeacherId = c.Int(),
                    TelephoneNo = c.String(maxLength: 128),
                    FaxNo = c.String(maxLength: 128),
                    EmailAddress = c.String(maxLength: 128),
                    Website = c.String(maxLength: 128),
                    Local = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("school.GovernanceType", t => t.GovernanceTypeId)
                .ForeignKey("person.People", t => t.HeadTeacherId)
                .ForeignKey("school.IntakeType", t => t.IntakeTypeId)
                .ForeignKey("school.LocalAuthority", t => t.LocalAuthorityId)
                .ForeignKey("school.Phase", t => t.PhaseId)
                .ForeignKey("school.Type", t => t.TypeId)
                .Index(t => t.LocalAuthorityId)
                .Index(t => t.PhaseId)
                .Index(t => t.TypeId)
                .Index(t => t.GovernanceTypeId)
                .Index(t => t.IntakeTypeId)
                .Index(t => t.HeadTeacherId);

            CreateTable(
                "school.GovernanceType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "school.IntakeType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "school.LocalAuthority",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LeaCode = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128),
                    Website = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "school.Phase",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "school.Type",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "profile.LogNote",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    AuthorId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    AcademicYearId = c.Int(nullable: false),
                    Message = c.String(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("profile.LogNoteType", t => t.TypeId)
                .ForeignKey("person.Student", t => t.StudentId)
                .ForeignKey("person.StaffMember", t => t.AuthorId)
                .ForeignKey("curriculum.AcademicYear", t => t.AcademicYearId)
                .Index(t => t.TypeId)
                .Index(t => t.AuthorId)
                .Index(t => t.StudentId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "profile.LogNoteType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "assessment.Result",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ResultSetId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    AspectId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Grade = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("assessment.Aspect", t => t.AspectId)
                .ForeignKey("assessment.ResultSet", t => t.ResultSetId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.ResultSetId)
                .Index(t => t.StudentId)
                .Index(t => t.AspectId);

            CreateTable(
                "assessment.Aspect",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    GradeSetId = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("assessment.GradeSet", t => t.GradeSetId)
                .ForeignKey("assessment.AspectType", t => t.TypeId)
                .Index(t => t.TypeId)
                .Index(t => t.GradeSetId);

            CreateTable(
                "assessment.GradeSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Active = c.Boolean(nullable: false),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "assessment.Grade",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GradeSetId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    Value = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("assessment.GradeSet", t => t.GradeSetId)
                .Index(t => t.GradeSetId);

            CreateTable(
                "assessment.AspectType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "assessment.ResultSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "sen.Event",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    EventTypeId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Note = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sen.EventType", t => t.EventTypeId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.EventTypeId);

            CreateTable(
                "sen.EventType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "sen.Provision",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ProvisionTypeId = c.Int(nullable: false),
                    StartDate = c.DateTime(nullable: false, storeType: "date"),
                    EndDate = c.DateTime(nullable: false, storeType: "date"),
                    Note = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("sen.ProvisionType", t => t.ProvisionTypeId)
                .ForeignKey("person.Student", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ProvisionTypeId);

            CreateTable(
                "sen.ProvisionType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "sen.Status",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "personnel.Observation",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    ObserveeId = c.Int(nullable: false),
                    ObserverId = c.Int(nullable: false),
                    OutcomeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("personnel.ObservationOutcome", t => t.OutcomeId)
                .ForeignKey("person.StaffMember", t => t.ObserveeId)
                .ForeignKey("person.StaffMember", t => t.ObserverId)
                .Index(t => t.ObserveeId)
                .Index(t => t.ObserverId)
                .Index(t => t.OutcomeId);

            CreateTable(
                "personnel.ObservationOutcome",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "personnel.TrainingCertificate",
                c => new
                {
                    CourseId = c.Int(nullable: false),
                    StaffId = c.Int(nullable: false),
                    StatusId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CourseId, t.StaffId })
                .ForeignKey("personnel.TrainingCertificateStatus", t => t.StatusId)
                .ForeignKey("personnel.TrainingCourse", t => t.CourseId)
                .ForeignKey("person.StaffMember", t => t.StaffId)
                .Index(t => t.CourseId)
                .Index(t => t.StaffId)
                .Index(t => t.StatusId);

            CreateTable(
                "personnel.TrainingCertificateStatus",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "personnel.TrainingCourse",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 128),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "behaviour.IncidentType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                    System = c.Boolean(nullable: false),
                    DefaultPoints = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "behaviour.AchievementType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                    DefaultPoints = c.Int(nullable: false),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "attendance.CodeMeaning",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "attendance.Code",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Description = c.String(nullable: false, maxLength: 128),
                    MeaningId = c.Int(nullable: false),
                    DoNotUse = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("attendance.CodeMeaning", t => t.MeaningId)
                .Index(t => t.MeaningId);

            CreateTable(
                "profile.CommentBank",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    System = c.Boolean(nullable: false),
                    InUse = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "profile.Comment",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CommentBankId = c.Int(nullable: false),
                    Value = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("profile.CommentBank", t => t.CommentBankId)
                .Index(t => t.CommentBankId);

            CreateTable(
                "communication.Log",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    CommunicationTypeId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Note = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("communication.Type", t => t.CommunicationTypeId)
                .Index(t => t.CommunicationTypeId);

            CreateTable(
                "communication.Type",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "curriculum.LessonPlanTemplate",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    PlanTemplate = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "person.RelationshipType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "system.Report",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AreaId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128),
                    Description = c.String(nullable: false, maxLength: 128),
                    Restricted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Area", t => t.AreaId)
                .Index(t => t.AreaId);

            CreateTable(
                "system.Area",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "sen.ReviewType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("system.Report", "AreaId", "system.Area");
            DropForeignKey("communication.Log", "CommunicationTypeId", "communication.Type");
            DropForeignKey("profile.Comment", "CommentBankId", "profile.CommentBank");
            DropForeignKey("attendance.Code", "MeaningId", "attendance.CodeMeaning");
            DropForeignKey("finance.Sale", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("profile.LogNote", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("behaviour.Incident", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("curriculum.Class", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("attendance.Week", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("behaviour.Achievement", "AcademicYearId", "curriculum.AcademicYear");
            DropForeignKey("behaviour.Achievement", "AchievementTypeId", "behaviour.AchievementType");
            DropForeignKey("behaviour.Incident", "LocationId", "school.Location");
            DropForeignKey("behaviour.Incident", "BehaviourTypeId", "behaviour.IncidentType");
            DropForeignKey("curriculum.SubjectStaffMember", "StaffMemberId", "person.StaffMember");
            DropForeignKey("profile.LogNote", "AuthorId", "person.StaffMember");
            DropForeignKey("personnel.TrainingCertificate", "StaffId", "person.StaffMember");
            DropForeignKey("personnel.TrainingCertificate", "CourseId", "personnel.TrainingCourse");
            DropForeignKey("personnel.TrainingCertificate", "StatusId", "personnel.TrainingCertificateStatus");
            DropForeignKey("personnel.Observation", "ObserverId", "person.StaffMember");
            DropForeignKey("personnel.Observation", "ObserveeId", "person.StaffMember");
            DropForeignKey("personnel.Observation", "OutcomeId", "personnel.ObservationOutcome");
            DropForeignKey("pastoral.YearGroup", "HeadId", "person.StaffMember");
            DropForeignKey("pastoral.RegGroup", "TutorId", "person.StaffMember");
            DropForeignKey("pastoral.House", "HeadId", "person.StaffMember");
            DropForeignKey("document.Document", "UploaderId", "person.StaffMember");
            DropForeignKey("curriculum.LessonPlan", "AuthorId", "person.StaffMember");
            DropForeignKey("curriculum.Class", "TeacherId", "person.StaffMember");
            DropForeignKey("curriculum.Session", "ClassId", "curriculum.Class");
            DropForeignKey("curriculum.Enrolment", "ClassId", "curriculum.Class");
            DropForeignKey("person.StudentContact", "StudentId", "person.Student");
            DropForeignKey("person.Student", "SenStatusId", "sen.Status");
            DropForeignKey("sen.Provision", "StudentId", "person.Student");
            DropForeignKey("sen.Provision", "ProvisionTypeId", "sen.ProvisionType");
            DropForeignKey("sen.Event", "StudentId", "person.Student");
            DropForeignKey("sen.Event", "EventTypeId", "sen.EventType");
            DropForeignKey("finance.Sale", "StudentId", "person.Student");
            DropForeignKey("assessment.Result", "StudentId", "person.Student");
            DropForeignKey("assessment.Result", "ResultSetId", "assessment.ResultSet");
            DropForeignKey("assessment.Aspect", "TypeId", "assessment.AspectType");
            DropForeignKey("assessment.Result", "AspectId", "assessment.Aspect");
            DropForeignKey("assessment.Grade", "GradeSetId", "assessment.GradeSet");
            DropForeignKey("assessment.Aspect", "GradeSetId", "assessment.GradeSet");
            DropForeignKey("profile.LogNote", "StudentId", "person.Student");
            DropForeignKey("profile.LogNote", "TypeId", "profile.LogNoteType");
            DropForeignKey("person.Student", "PersonId", "person.People");
            DropForeignKey("person.StaffMember", "PersonId", "person.People");
            DropForeignKey("school.School", "TypeId", "school.Type");
            DropForeignKey("school.School", "PhaseId", "school.Phase");
            DropForeignKey("school.School", "LocalAuthorityId", "school.LocalAuthority");
            DropForeignKey("school.School", "IntakeTypeId", "school.IntakeType");
            DropForeignKey("school.School", "HeadTeacherId", "person.People");
            DropForeignKey("school.School", "GovernanceTypeId", "school.GovernanceType");
            DropForeignKey("communication.PhoneNumber", "PersonId", "person.People");
            DropForeignKey("communication.PhoneNumber", "TypeId", "communication.PhoneNumberType");
            DropForeignKey("document.PersonAttachment", "PersonId", "person.People");
            DropForeignKey("document.Document", "TypeId", "document.Type");
            DropForeignKey("document.PersonAttachment", "DocumentId", "document.Document");
            DropForeignKey("medical.PersonCondition", "PersonId", "person.People");
            DropForeignKey("medical.PersonCondition", "ConditionId", "medical.Condition");
            DropForeignKey("communication.EmailAddress", "PersonId", "person.People");
            DropForeignKey("communication.EmailAddress", "TypeId", "communication.EmailAddressType");
            DropForeignKey("document.PersonDietaryRequirement", "PersonId", "person.People");
            DropForeignKey("document.PersonDietaryRequirement", "DietaryRequirementId", "medical.DietaryRequirement");
            DropForeignKey("person.Contact", "PersonId", "person.People");
            DropForeignKey("person.StudentContact", "ContactId", "person.Contact");
            DropForeignKey("communication.AddressPerson", "AddressId", "person.People");
            DropForeignKey("communication.AddressPerson", "AddressId", "communication.Address");
            DropForeignKey("medical.Event", "StudentId", "person.Student");
            DropForeignKey("medical.Event", "RecordedById", "person.StaffMember");
            DropForeignKey("behaviour.Incident", "StudentId", "person.Student");
            DropForeignKey("person.Student", "HouseId", "pastoral.House");
            DropForeignKey("sen.GiftedTalented", "StudentId", "person.Student");
            DropForeignKey("curriculum.StudyTopic", "SubjectId", "curriculum.Subject");
            DropForeignKey("curriculum.StudyTopic", "YearGroupId", "pastoral.YearGroup");
            DropForeignKey("person.Student", "YearGroupId", "pastoral.YearGroup");
            DropForeignKey("pastoral.RegGroup", "YearGroupId", "pastoral.YearGroup");
            DropForeignKey("person.Student", "RegGroupId", "pastoral.RegGroup");
            DropForeignKey("curriculum.Class", "YearGroupId", "pastoral.YearGroup");
            DropForeignKey("curriculum.LessonPlan", "StudyTopicId", "curriculum.StudyTopic");
            DropForeignKey("curriculum.SubjectStaffMember", "SubjectId", "curriculum.Subject");
            DropForeignKey("curriculum.SubjectStaffMember", "RoleId", "curriculum.SubjectStaffMemberRole");
            DropForeignKey("sen.GiftedTalented", "SubjectId", "curriculum.Subject");
            DropForeignKey("curriculum.Class", "SubjectId", "curriculum.Subject");
            DropForeignKey("finance.BasketItem", "StudentId", "person.Student");
            DropForeignKey("finance.Product", "ProductTypeId", "finance.ProductType");
            DropForeignKey("finance.Sale", "ProductId", "finance.Product");
            DropForeignKey("finance.BasketItem", "ProductId", "finance.Product");
            DropForeignKey("curriculum.Enrolment", "StudentId", "person.Student");
            DropForeignKey("attendance.Mark", "StudentId", "person.Student");
            DropForeignKey("attendance.Mark", "WeekId", "attendance.Week");
            DropForeignKey("curriculum.Session", "PeriodId", "attendance.Period");
            DropForeignKey("attendance.Mark", "PeriodId", "attendance.Period");
            DropForeignKey("behaviour.Achievement", "StudentId", "person.Student");
            DropForeignKey("system.Bulletin", "AuthorId", "person.StaffMember");
            DropForeignKey("behaviour.Incident", "RecordedById", "person.StaffMember");
            DropForeignKey("behaviour.Achievement", "RecordedById", "person.StaffMember");
            DropForeignKey("behaviour.Achievement", "LocationId", "school.Location");
            DropIndex("system.Report", new[] { "AreaId" });
            DropIndex("communication.Log", new[] { "CommunicationTypeId" });
            DropIndex("profile.Comment", new[] { "CommentBankId" });
            DropIndex("attendance.Code", new[] { "MeaningId" });
            DropIndex("personnel.TrainingCertificate", new[] { "StatusId" });
            DropIndex("personnel.TrainingCertificate", new[] { "StaffId" });
            DropIndex("personnel.TrainingCertificate", new[] { "CourseId" });
            DropIndex("personnel.Observation", new[] { "OutcomeId" });
            DropIndex("personnel.Observation", new[] { "ObserverId" });
            DropIndex("personnel.Observation", new[] { "ObserveeId" });
            DropIndex("sen.Provision", new[] { "ProvisionTypeId" });
            DropIndex("sen.Provision", new[] { "StudentId" });
            DropIndex("sen.Event", new[] { "EventTypeId" });
            DropIndex("sen.Event", new[] { "StudentId" });
            DropIndex("assessment.Grade", new[] { "GradeSetId" });
            DropIndex("assessment.Aspect", new[] { "GradeSetId" });
            DropIndex("assessment.Aspect", new[] { "TypeId" });
            DropIndex("assessment.Result", new[] { "AspectId" });
            DropIndex("assessment.Result", new[] { "StudentId" });
            DropIndex("assessment.Result", new[] { "ResultSetId" });
            DropIndex("profile.LogNote", new[] { "AcademicYearId" });
            DropIndex("profile.LogNote", new[] { "StudentId" });
            DropIndex("profile.LogNote", new[] { "AuthorId" });
            DropIndex("profile.LogNote", new[] { "TypeId" });
            DropIndex("school.School", new[] { "HeadTeacherId" });
            DropIndex("school.School", new[] { "IntakeTypeId" });
            DropIndex("school.School", new[] { "GovernanceTypeId" });
            DropIndex("school.School", new[] { "TypeId" });
            DropIndex("school.School", new[] { "PhaseId" });
            DropIndex("school.School", new[] { "LocalAuthorityId" });
            DropIndex("communication.PhoneNumber", new[] { "PersonId" });
            DropIndex("communication.PhoneNumber", new[] { "TypeId" });
            DropIndex("document.Document", new[] { "UploaderId" });
            DropIndex("document.Document", new[] { "TypeId" });
            DropIndex("document.PersonAttachment", new[] { "DocumentId" });
            DropIndex("document.PersonAttachment", new[] { "PersonId" });
            DropIndex("medical.PersonCondition", new[] { "ConditionId" });
            DropIndex("medical.PersonCondition", new[] { "PersonId" });
            DropIndex("communication.EmailAddress", new[] { "PersonId" });
            DropIndex("communication.EmailAddress", new[] { "TypeId" });
            DropIndex("document.PersonDietaryRequirement", new[] { "DietaryRequirementId" });
            DropIndex("document.PersonDietaryRequirement", new[] { "PersonId" });
            DropIndex("person.StudentContact", new[] { "ContactId" });
            DropIndex("person.StudentContact", new[] { "StudentId" });
            DropIndex("person.Contact", new[] { "PersonId" });
            DropIndex("communication.AddressPerson", new[] { "AddressId" });
            DropIndex("medical.Event", new[] { "RecordedById" });
            DropIndex("medical.Event", new[] { "StudentId" });
            DropIndex("pastoral.House", new[] { "HeadId" });
            DropIndex("pastoral.RegGroup", new[] { "YearGroupId" });
            DropIndex("pastoral.RegGroup", new[] { "TutorId" });
            DropIndex("pastoral.YearGroup", new[] { "HeadId" });
            DropIndex("curriculum.LessonPlan", new[] { "AuthorId" });
            DropIndex("curriculum.LessonPlan", new[] { "StudyTopicId" });
            DropIndex("curriculum.StudyTopic", new[] { "YearGroupId" });
            DropIndex("curriculum.StudyTopic", new[] { "SubjectId" });
            DropIndex("curriculum.SubjectStaffMember", new[] { "RoleId" });
            DropIndex("curriculum.SubjectStaffMember", new[] { "StaffMemberId" });
            DropIndex("curriculum.SubjectStaffMember", new[] { "SubjectId" });
            DropIndex("sen.GiftedTalented", new[] { "SubjectId" });
            DropIndex("sen.GiftedTalented", new[] { "StudentId" });
            DropIndex("finance.Sale", new[] { "AcademicYearId" });
            DropIndex("finance.Sale", new[] { "ProductId" });
            DropIndex("finance.Sale", new[] { "StudentId" });
            DropIndex("finance.Product", new[] { "ProductTypeId" });
            DropIndex("finance.BasketItem", new[] { "ProductId" });
            DropIndex("finance.BasketItem", new[] { "StudentId" });
            DropIndex("attendance.Week", new[] { "AcademicYearId" });
            DropIndex("curriculum.Session", new[] { "PeriodId" });
            DropIndex("curriculum.Session", new[] { "ClassId" });
            DropIndex("attendance.Mark", new[] { "PeriodId" });
            DropIndex("attendance.Mark", new[] { "WeekId" });
            DropIndex("attendance.Mark", new[] { "StudentId" });
            DropIndex("person.Student", new[] { "SenStatusId" });
            DropIndex("person.Student", new[] { "HouseId" });
            DropIndex("person.Student", new[] { "YearGroupId" });
            DropIndex("person.Student", new[] { "RegGroupId" });
            DropIndex("person.Student", new[] { "PersonId" });
            DropIndex("curriculum.Enrolment", new[] { "ClassId" });
            DropIndex("curriculum.Enrolment", new[] { "StudentId" });
            DropIndex("curriculum.Class", new[] { "YearGroupId" });
            DropIndex("curriculum.Class", new[] { "TeacherId" });
            DropIndex("curriculum.Class", new[] { "SubjectId" });
            DropIndex("curriculum.Class", new[] { "AcademicYearId" });
            DropIndex("system.Bulletin", new[] { "AuthorId" });
            DropIndex("person.StaffMember", new[] { "Code" });
            DropIndex("person.StaffMember", new[] { "PersonId" });
            DropIndex("behaviour.Incident", new[] { "RecordedById" });
            DropIndex("behaviour.Incident", new[] { "LocationId" });
            DropIndex("behaviour.Incident", new[] { "StudentId" });
            DropIndex("behaviour.Incident", new[] { "BehaviourTypeId" });
            DropIndex("behaviour.Incident", new[] { "AcademicYearId" });
            DropIndex("behaviour.Achievement", new[] { "RecordedById" });
            DropIndex("behaviour.Achievement", new[] { "LocationId" });
            DropIndex("behaviour.Achievement", new[] { "StudentId" });
            DropIndex("behaviour.Achievement", new[] { "AchievementTypeId" });
            DropIndex("behaviour.Achievement", new[] { "AcademicYearId" });
            DropTable("sen.ReviewType");
            DropTable("system.Area");
            DropTable("system.Report");
            DropTable("person.RelationshipType");
            DropTable("curriculum.LessonPlanTemplate");
            DropTable("communication.Type");
            DropTable("communication.Log");
            DropTable("profile.Comment");
            DropTable("profile.CommentBank");
            DropTable("attendance.Code");
            DropTable("attendance.CodeMeaning");
            DropTable("behaviour.AchievementType");
            DropTable("behaviour.IncidentType");
            DropTable("personnel.TrainingCourse");
            DropTable("personnel.TrainingCertificateStatus");
            DropTable("personnel.TrainingCertificate");
            DropTable("personnel.ObservationOutcome");
            DropTable("personnel.Observation");
            DropTable("sen.Status");
            DropTable("sen.ProvisionType");
            DropTable("sen.Provision");
            DropTable("sen.EventType");
            DropTable("sen.Event");
            DropTable("assessment.ResultSet");
            DropTable("assessment.AspectType");
            DropTable("assessment.Grade");
            DropTable("assessment.GradeSet");
            DropTable("assessment.Aspect");
            DropTable("assessment.Result");
            DropTable("profile.LogNoteType");
            DropTable("profile.LogNote");
            DropTable("school.Type");
            DropTable("school.Phase");
            DropTable("school.LocalAuthority");
            DropTable("school.IntakeType");
            DropTable("school.GovernanceType");
            DropTable("school.School");
            DropTable("communication.PhoneNumberType");
            DropTable("communication.PhoneNumber");
            DropTable("document.Type");
            DropTable("document.Document");
            DropTable("document.PersonAttachment");
            DropTable("medical.Condition");
            DropTable("medical.PersonCondition");
            DropTable("communication.EmailAddressType");
            DropTable("communication.EmailAddress");
            DropTable("medical.DietaryRequirement");
            DropTable("document.PersonDietaryRequirement");
            DropTable("person.StudentContact");
            DropTable("person.Contact");
            DropTable("communication.Address");
            DropTable("communication.AddressPerson");
            DropTable("person.People");
            DropTable("medical.Event");
            DropTable("pastoral.House");
            DropTable("pastoral.RegGroup");
            DropTable("pastoral.YearGroup");
            DropTable("curriculum.LessonPlan");
            DropTable("curriculum.StudyTopic");
            DropTable("curriculum.SubjectStaffMemberRole");
            DropTable("curriculum.SubjectStaffMember");
            DropTable("curriculum.Subject");
            DropTable("sen.GiftedTalented");
            DropTable("finance.ProductType");
            DropTable("finance.Sale");
            DropTable("finance.Product");
            DropTable("finance.BasketItem");
            DropTable("attendance.Week");
            DropTable("curriculum.Session");
            DropTable("attendance.Period");
            DropTable("attendance.Mark");
            DropTable("person.Student");
            DropTable("curriculum.Enrolment");
            DropTable("curriculum.Class");
            DropTable("system.Bulletin");
            DropTable("person.StaffMember");
            DropTable("behaviour.Incident");
            DropTable("school.Location");
            DropTable("behaviour.Achievement");
            DropTable("curriculum.AcademicYear");
        }
    }
}
