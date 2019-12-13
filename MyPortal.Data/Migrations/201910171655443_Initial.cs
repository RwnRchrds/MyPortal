namespace MyPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessment_Aspects",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    GradeSetId = c.Int(nullable: false),
                    Description = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessment_GradeSets", t => t.GradeSetId)
                .ForeignKey("dbo.Assessment_AspectTypes", t => t.TypeId)
                .Index(t => t.TypeId)
                .Index(t => t.GradeSetId);

            CreateTable(
                "dbo.Assessment_GradeSets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    Active = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Assessment_Grades",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    GradeSetId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    Value = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessment_GradeSets", t => t.GradeSetId)
                .Index(t => t.GradeSetId);

            CreateTable(
                "dbo.Assessment_Results",
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
                .ForeignKey("dbo.Assessment_ResultSets", t => t.ResultSetId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .ForeignKey("dbo.Assessment_Aspects", t => t.AspectId)
                .Index(t => t.ResultSetId)
                .Index(t => t.StudentId)
                .Index(t => t.AspectId);

            CreateTable(
                "dbo.Assessment_ResultSets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    IsCurrent = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.People_Students",
                c => new
                {
                    Id = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                    RegGroupId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                    HouseId = c.Int(),
                    CandidateNumber = c.String(maxLength: 128),
                    AdmissionNumber = c.Int(nullable: false, identity: true),
                    AccountBalance = c.Decimal(nullable: false, precision: 10, scale: 2),
                    FreeSchoolMeals = c.Boolean(nullable: false),
                    GiftedAndTalented = c.Boolean(nullable: false),
                    SenStatusId = c.Int(nullable: false),
                    PupilPremium = c.Boolean(nullable: false),
                    MisId = c.String(maxLength: 256),
                    Upn = c.String(maxLength: 13, unicode: false),
                    Uci = c.String(),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pastoral_RegGroups", t => t.RegGroupId)
                .ForeignKey("dbo.Pastoral_YearGroups", t => t.YearGroupId)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .ForeignKey("dbo.Pastoral_Houses", t => t.HouseId)
                .ForeignKey("dbo.Sen_Statuses", t => t.SenStatusId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.RegGroupId)
                .Index(t => t.YearGroupId)
                .Index(t => t.HouseId)
                .Index(t => t.SenStatusId);

            CreateTable(
                "dbo.Attendance_Marks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    WeekId = c.Int(nullable: false),
                    PeriodId = c.Int(nullable: false),
                    Mark = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attendance_Periods", t => t.PeriodId)
                .ForeignKey("dbo.Attendance_Weeks", t => t.WeekId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.WeekId)
                .Index(t => t.PeriodId);

            CreateTable(
                "dbo.Attendance_Periods",
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
                "dbo.Curriculum_Sessions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ClassId = c.Int(nullable: false),
                    PeriodId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_Classes", t => t.ClassId)
                .ForeignKey("dbo.Attendance_Periods", t => t.PeriodId)
                .Index(t => t.ClassId)
                .Index(t => t.PeriodId);

            CreateTable(
                "dbo.Curriculum_Classes",
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
                .ForeignKey("dbo.People_Staff", t => t.TeacherId)
                .ForeignKey("dbo.Curriculum_Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Pastoral_YearGroups", t => t.YearGroupId)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.SubjectId)
                .Index(t => t.TeacherId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "dbo.Curriculum_AcademicYears",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    FirstDate = c.DateTime(nullable: false, storeType: "date"),
                    LastDate = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Behaviour_Achievements",
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
                .ForeignKey("dbo.School_Locations", t => t.LocationId)
                .ForeignKey("dbo.People_Staff", t => t.RecordedById)
                .ForeignKey("dbo.Behaviour_AchievementTypes", t => t.AchievementTypeId)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.AchievementTypeId)
                .Index(t => t.StudentId)
                .Index(t => t.LocationId)
                .Index(t => t.RecordedById);

            CreateTable(
                "dbo.School_Locations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Behaviour_Incidents",
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
                .ForeignKey("dbo.People_Staff", t => t.RecordedById)
                .ForeignKey("dbo.Behaviour_IncidentTypes", t => t.BehaviourTypeId)
                .ForeignKey("dbo.School_Locations", t => t.LocationId)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.AcademicYearId)
                .Index(t => t.BehaviourTypeId)
                .Index(t => t.StudentId)
                .Index(t => t.LocationId)
                .Index(t => t.RecordedById);

            CreateTable(
                "dbo.People_Staff",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.System_Bulletins",
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
                .ForeignKey("dbo.People_Staff", t => t.AuthorId)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.Curriculum_LessonPlans",
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
                .ForeignKey("dbo.Curriculum_StudyTopics", t => t.StudyTopicId)
                .ForeignKey("dbo.People_Staff", t => t.AuthorId)
                .Index(t => t.StudyTopicId)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.Curriculum_StudyTopics",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SubjectId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_Subjects", t => t.SubjectId)
                .ForeignKey("dbo.Pastoral_YearGroups", t => t.YearGroupId)
                .Index(t => t.SubjectId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "dbo.Curriculum_Subjects",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    LeaderId = c.Int(nullable: false),
                    Code = c.String(nullable: false, maxLength: 128),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Staff", t => t.LeaderId)
                .Index(t => t.LeaderId);

            CreateTable(
                "dbo.SenGiftedTalenteds",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    SubjectId = c.Int(nullable: false),
                    Notes = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_Subjects", t => t.SubjectId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);

            CreateTable(
                "dbo.Pastoral_YearGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    HeadId = c.Int(nullable: false),
                    KeyStage = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Staff", t => t.HeadId)
                .Index(t => t.HeadId);

            CreateTable(
                "dbo.Pastoral_RegGroups",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    TutorId = c.Int(nullable: false),
                    YearGroupId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pastoral_YearGroups", t => t.YearGroupId)
                .ForeignKey("dbo.People_Staff", t => t.TutorId)
                .Index(t => t.TutorId)
                .Index(t => t.YearGroupId);

            CreateTable(
                "dbo.Docs_Documents",
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
                .ForeignKey("dbo.Docs_DocumentTypes", t => t.TypeId)
                .ForeignKey("dbo.People_Staff", t => t.UploaderId)
                .Index(t => t.TypeId)
                .Index(t => t.UploaderId);

            CreateTable(
                "dbo.Docs_PersonDocuments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    DocumentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .ForeignKey("dbo.Docs_Documents", t => t.DocumentId)
                .Index(t => t.PersonId)
                .Index(t => t.DocumentId);

            CreateTable(
                "dbo.People_Persons",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(maxLength: 128),
                    FirstName = c.String(nullable: false, maxLength: 256),
                    LastName = c.String(nullable: false, maxLength: 256),
                    Gender = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Dob = c.DateTime(storeType: "date"),
                    UserId = c.String(),
                    Deleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.People_Contacts",
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
                .ForeignKey("dbo.People_Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.Medical_PersonDietaryRequirements",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    DietaryRequirementId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medical_DietaryRequirements", t => t.DietaryRequirementId)
                .ForeignKey("dbo.People_Persons", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.DietaryRequirementId);

            CreateTable(
                "dbo.Medical_DietaryRequirements",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Medical_PersonConditions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    ConditionId = c.Int(nullable: false),
                    MedicationTaken = c.Boolean(nullable: false),
                    Medication = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medical_Conditions", t => t.ConditionId)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.ConditionId);

            CreateTable(
                "dbo.Medical_Conditions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Communication_PhoneNumbers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TypeId = c.Int(nullable: false),
                    PersonId = c.Int(nullable: false),
                    Number = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Communication_PhoneNumberTypes", t => t.TypeId)
                .ForeignKey("dbo.People_Persons", t => t.PersonId)
                .Index(t => t.TypeId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.Communication_PhoneNumberTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.System_Schools",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    LocalAuthorityId = c.Int(),
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
                .ForeignKey("dbo.School_GovernanceTypes", t => t.GovernanceTypeId)
                .ForeignKey("dbo.People_Persons", t => t.HeadTeacherId)
                .ForeignKey("dbo.School_IntakeTypes", t => t.IntakeTypeId)
                .ForeignKey("dbo.School_Phases", t => t.PhaseId)
                .ForeignKey("dbo.School_Types", t => t.TypeId)
                .Index(t => t.PhaseId)
                .Index(t => t.TypeId)
                .Index(t => t.GovernanceTypeId)
                .Index(t => t.IntakeTypeId)
                .Index(t => t.HeadTeacherId);

            CreateTable(
                "dbo.School_GovernanceTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.School_IntakeTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.School_Phases",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.School_Types",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Docs_DocumentTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Medical_Events",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    RecordedById = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Note = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Staff", t => t.RecordedById, cascadeDelete: true)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.RecordedById);

            CreateTable(
                "dbo.Pastoral_Houses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    HeadId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Staff", t => t.HeadId)
                .Index(t => t.HeadId);

            CreateTable(
                "dbo.Personnel_Observations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    ObserveeId = c.Int(nullable: false),
                    ObserverId = c.Int(nullable: false),
                    Outcome = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People_Staff", t => t.ObserverId)
                .ForeignKey("dbo.People_Staff", t => t.ObserveeId)
                .Index(t => t.ObserveeId)
                .Index(t => t.ObserverId);

            CreateTable(
                "dbo.Personnel_TrainingCertificates",
                c => new
                {
                    CourseId = c.Int(nullable: false),
                    StaffId = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.CourseId, t.StaffId })
                .ForeignKey("dbo.Personnel_TrainingCourses", t => t.CourseId)
                .ForeignKey("dbo.People_Staff", t => t.StaffId)
                .Index(t => t.CourseId)
                .Index(t => t.StaffId);

            CreateTable(
                "dbo.Personnel_TrainingCourses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 128),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Profile_Logs",
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
                .ForeignKey("dbo.Profile_LogTypes", t => t.TypeId)
                .ForeignKey("dbo.People_Staff", t => t.AuthorId)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.TypeId)
                .Index(t => t.AuthorId)
                .Index(t => t.StudentId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "dbo.Profile_LogTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Behaviour_IncidentTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                    System = c.Boolean(nullable: false),
                    DefaultPoints = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Behaviour_AchievementTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                    DefaultPoints = c.Int(nullable: false),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Attendance_Weeks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AcademicYearId = c.Int(nullable: false),
                    Beginning = c.DateTime(nullable: false, storeType: "date"),
                    IsHoliday = c.Boolean(nullable: false),
                    IsNonTimetable = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "dbo.Finance_Sales",
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
                .ForeignKey("dbo.Finance_Products", t => t.ProductId)
                .ForeignKey("dbo.Curriculum_AcademicYears", t => t.AcademicYearId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ProductId)
                .Index(t => t.AcademicYearId);

            CreateTable(
                "dbo.Finance_Products",
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
                .ForeignKey("dbo.Finance_ProductTypes", t => t.ProductTypeId)
                .Index(t => t.ProductTypeId);

            CreateTable(
                "dbo.Finance_BasketItems",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ProductId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Finance_Products", t => t.ProductId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ProductId);

            CreateTable(
                "dbo.Finance_ProductTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                    IsMeal = c.Boolean(nullable: false),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Curriculum_Enrolments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    ClassId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum_Classes", t => t.ClassId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId);

            CreateTable(
                "dbo.Sen_Events",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentId = c.Int(nullable: false),
                    EventTypeId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Note = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sen_EventTypes", t => t.EventTypeId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.EventTypeId);

            CreateTable(
                "dbo.Sen_EventTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Sen_Provisions",
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
                .ForeignKey("dbo.Sen_ProvisionTypes", t => t.ProvisionTypeId)
                .ForeignKey("dbo.People_Students", t => t.StudentId)
                .Index(t => t.StudentId)
                .Index(t => t.ProvisionTypeId);

            CreateTable(
                "dbo.Sen_ProvisionTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Sen_Statuses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Assessment_AspectTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Attendance_Codes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    Description = c.String(nullable: false, maxLength: 128),
                    MeaningId = c.Int(nullable: false),
                    System = c.Boolean(nullable: false),
                    DoNotUse = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attendance_Meanings", t => t.MeaningId)
                .Index(t => t.MeaningId);

            CreateTable(
                "dbo.Attendance_Meanings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Code = c.String(nullable: false, maxLength: 128),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Communication_CommunicationLogs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PersonId = c.Int(nullable: false),
                    CommunicationTypeId = c.Int(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Note = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Communication_CommunicationTypes", t => t.CommunicationTypeId)
                .Index(t => t.CommunicationTypeId);

            CreateTable(
                "dbo.Communication_CommunicationTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.People_ContactTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Curriculum_LessonPlanTemplates",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                    PlanTemplate = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Profile_CommentBanks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 128),
                    System = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Profile_Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CommentBankId = c.Int(nullable: false),
                    Value = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profile_CommentBanks", t => t.CommentBankId)
                .Index(t => t.CommentBankId);

            CreateTable(
                "dbo.Sen_ReviewTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.System_Areas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.System_Reports",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AreaId = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128),
                    Description = c.String(nullable: false, maxLength: 128),
                    Restricted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System_Areas", t => t.AreaId)
                .Index(t => t.AreaId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.System_Reports", "AreaId", "dbo.System_Areas");
            DropForeignKey("dbo.Profile_Comments", "CommentBankId", "dbo.Profile_CommentBanks");
            DropForeignKey("dbo.Communication_CommunicationLogs", "CommunicationTypeId", "dbo.Communication_CommunicationTypes");
            DropForeignKey("dbo.Attendance_Codes", "MeaningId", "dbo.Attendance_Meanings");
            DropForeignKey("dbo.Assessment_Aspects", "TypeId", "dbo.Assessment_AspectTypes");
            DropForeignKey("dbo.Assessment_Results", "AspectId", "dbo.Assessment_Aspects");
            DropForeignKey("dbo.People_Students", "SenStatusId", "dbo.Sen_Statuses");
            DropForeignKey("dbo.Sen_Provisions", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Sen_Provisions", "ProvisionTypeId", "dbo.Sen_ProvisionTypes");
            DropForeignKey("dbo.Sen_Events", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Sen_Events", "EventTypeId", "dbo.Sen_EventTypes");
            DropForeignKey("dbo.Profile_Logs", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Medical_Events", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.SenGiftedTalenteds", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Finance_Sales", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Finance_BasketItems", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Curriculum_Enrolments", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Behaviour_Incidents", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Behaviour_Achievements", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Attendance_Marks", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Curriculum_Sessions", "PeriodId", "dbo.Attendance_Periods");
            DropForeignKey("dbo.Curriculum_Sessions", "ClassId", "dbo.Curriculum_Classes");
            DropForeignKey("dbo.Curriculum_Enrolments", "ClassId", "dbo.Curriculum_Classes");
            DropForeignKey("dbo.Finance_Sales", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Finance_Products", "ProductTypeId", "dbo.Finance_ProductTypes");
            DropForeignKey("dbo.Finance_Sales", "ProductId", "dbo.Finance_Products");
            DropForeignKey("dbo.Finance_BasketItems", "ProductId", "dbo.Finance_Products");
            DropForeignKey("dbo.Profile_Logs", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Behaviour_Incidents", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Curriculum_Classes", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Attendance_Weeks", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Attendance_Marks", "WeekId", "dbo.Attendance_Weeks");
            DropForeignKey("dbo.Behaviour_Achievements", "AcademicYearId", "dbo.Curriculum_AcademicYears");
            DropForeignKey("dbo.Behaviour_Achievements", "AchievementTypeId", "dbo.Behaviour_AchievementTypes");
            DropForeignKey("dbo.Behaviour_Incidents", "LocationId", "dbo.School_Locations");
            DropForeignKey("dbo.Behaviour_Incidents", "BehaviourTypeId", "dbo.Behaviour_IncidentTypes");
            DropForeignKey("dbo.Profile_Logs", "AuthorId", "dbo.People_Staff");
            DropForeignKey("dbo.Profile_Logs", "TypeId", "dbo.Profile_LogTypes");
            DropForeignKey("dbo.Personnel_TrainingCertificates", "StaffId", "dbo.People_Staff");
            DropForeignKey("dbo.Personnel_TrainingCertificates", "CourseId", "dbo.Personnel_TrainingCourses");
            DropForeignKey("dbo.Personnel_Observations", "ObserveeId", "dbo.People_Staff");
            DropForeignKey("dbo.Personnel_Observations", "ObserverId", "dbo.People_Staff");
            DropForeignKey("dbo.Pastoral_YearGroups", "HeadId", "dbo.People_Staff");
            DropForeignKey("dbo.Pastoral_RegGroups", "TutorId", "dbo.People_Staff");
            DropForeignKey("dbo.Pastoral_Houses", "HeadId", "dbo.People_Staff");
            DropForeignKey("dbo.People_Students", "HouseId", "dbo.Pastoral_Houses");
            DropForeignKey("dbo.Medical_Events", "RecordedById", "dbo.People_Staff");
            DropForeignKey("dbo.Docs_Documents", "UploaderId", "dbo.People_Staff");
            DropForeignKey("dbo.Docs_Documents", "TypeId", "dbo.Docs_DocumentTypes");
            DropForeignKey("dbo.Docs_PersonDocuments", "DocumentId", "dbo.Docs_Documents");
            DropForeignKey("dbo.People_Students", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.People_Staff", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.System_Schools", "TypeId", "dbo.School_Types");
            DropForeignKey("dbo.System_Schools", "PhaseId", "dbo.School_Phases");
            DropForeignKey("dbo.System_Schools", "IntakeTypeId", "dbo.School_IntakeTypes");
            DropForeignKey("dbo.System_Schools", "HeadTeacherId", "dbo.People_Persons");
            DropForeignKey("dbo.System_Schools", "GovernanceTypeId", "dbo.School_GovernanceTypes");
            DropForeignKey("dbo.Communication_PhoneNumbers", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Communication_PhoneNumbers", "TypeId", "dbo.Communication_PhoneNumberTypes");
            DropForeignKey("dbo.Docs_PersonDocuments", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Medical_PersonConditions", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Medical_PersonConditions", "ConditionId", "dbo.Medical_Conditions");
            DropForeignKey("dbo.Medical_PersonDietaryRequirements", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Medical_PersonDietaryRequirements", "DietaryRequirementId", "dbo.Medical_DietaryRequirements");
            DropForeignKey("dbo.People_Contacts", "PersonId", "dbo.People_Persons");
            DropForeignKey("dbo.Curriculum_Subjects", "LeaderId", "dbo.People_Staff");
            DropForeignKey("dbo.Curriculum_LessonPlans", "AuthorId", "dbo.People_Staff");
            DropForeignKey("dbo.Curriculum_StudyTopics", "YearGroupId", "dbo.Pastoral_YearGroups");
            DropForeignKey("dbo.People_Students", "YearGroupId", "dbo.Pastoral_YearGroups");
            DropForeignKey("dbo.Pastoral_RegGroups", "YearGroupId", "dbo.Pastoral_YearGroups");
            DropForeignKey("dbo.People_Students", "RegGroupId", "dbo.Pastoral_RegGroups");
            DropForeignKey("dbo.Curriculum_Classes", "YearGroupId", "dbo.Pastoral_YearGroups");
            DropForeignKey("dbo.Curriculum_LessonPlans", "StudyTopicId", "dbo.Curriculum_StudyTopics");
            DropForeignKey("dbo.Curriculum_StudyTopics", "SubjectId", "dbo.Curriculum_Subjects");
            DropForeignKey("dbo.SenGiftedTalenteds", "SubjectId", "dbo.Curriculum_Subjects");
            DropForeignKey("dbo.Curriculum_Classes", "SubjectId", "dbo.Curriculum_Subjects");
            DropForeignKey("dbo.Curriculum_Classes", "TeacherId", "dbo.People_Staff");
            DropForeignKey("dbo.System_Bulletins", "AuthorId", "dbo.People_Staff");
            DropForeignKey("dbo.Behaviour_Incidents", "RecordedById", "dbo.People_Staff");
            DropForeignKey("dbo.Behaviour_Achievements", "RecordedById", "dbo.People_Staff");
            DropForeignKey("dbo.Behaviour_Achievements", "LocationId", "dbo.School_Locations");
            DropForeignKey("dbo.Attendance_Marks", "PeriodId", "dbo.Attendance_Periods");
            DropForeignKey("dbo.Assessment_Results", "StudentId", "dbo.People_Students");
            DropForeignKey("dbo.Assessment_Results", "ResultSetId", "dbo.Assessment_ResultSets");
            DropForeignKey("dbo.Assessment_Grades", "GradeSetId", "dbo.Assessment_GradeSets");
            DropForeignKey("dbo.Assessment_Aspects", "GradeSetId", "dbo.Assessment_GradeSets");
            DropIndex("dbo.System_Reports", new[] { "AreaId" });
            DropIndex("dbo.Profile_Comments", new[] { "CommentBankId" });
            DropIndex("dbo.Communication_CommunicationLogs", new[] { "CommunicationTypeId" });
            DropIndex("dbo.Attendance_Codes", new[] { "MeaningId" });
            DropIndex("dbo.Sen_Provisions", new[] { "ProvisionTypeId" });
            DropIndex("dbo.Sen_Provisions", new[] { "StudentId" });
            DropIndex("dbo.Sen_Events", new[] { "EventTypeId" });
            DropIndex("dbo.Sen_Events", new[] { "StudentId" });
            DropIndex("dbo.Curriculum_Enrolments", new[] { "ClassId" });
            DropIndex("dbo.Curriculum_Enrolments", new[] { "StudentId" });
            DropIndex("dbo.Finance_BasketItems", new[] { "ProductId" });
            DropIndex("dbo.Finance_BasketItems", new[] { "StudentId" });
            DropIndex("dbo.Finance_Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Finance_Sales", new[] { "AcademicYearId" });
            DropIndex("dbo.Finance_Sales", new[] { "ProductId" });
            DropIndex("dbo.Finance_Sales", new[] { "StudentId" });
            DropIndex("dbo.Attendance_Weeks", new[] { "AcademicYearId" });
            DropIndex("dbo.Profile_Logs", new[] { "AcademicYearId" });
            DropIndex("dbo.Profile_Logs", new[] { "StudentId" });
            DropIndex("dbo.Profile_Logs", new[] { "AuthorId" });
            DropIndex("dbo.Profile_Logs", new[] { "TypeId" });
            DropIndex("dbo.Personnel_TrainingCertificates", new[] { "StaffId" });
            DropIndex("dbo.Personnel_TrainingCertificates", new[] { "CourseId" });
            DropIndex("dbo.Personnel_Observations", new[] { "ObserverId" });
            DropIndex("dbo.Personnel_Observations", new[] { "ObserveeId" });
            DropIndex("dbo.Pastoral_Houses", new[] { "HeadId" });
            DropIndex("dbo.Medical_Events", new[] { "RecordedById" });
            DropIndex("dbo.Medical_Events", new[] { "StudentId" });
            DropIndex("dbo.System_Schools", new[] { "HeadTeacherId" });
            DropIndex("dbo.System_Schools", new[] { "IntakeTypeId" });
            DropIndex("dbo.System_Schools", new[] { "GovernanceTypeId" });
            DropIndex("dbo.System_Schools", new[] { "TypeId" });
            DropIndex("dbo.System_Schools", new[] { "PhaseId" });
            DropIndex("dbo.Communication_PhoneNumbers", new[] { "PersonId" });
            DropIndex("dbo.Communication_PhoneNumbers", new[] { "TypeId" });
            DropIndex("dbo.Medical_PersonConditions", new[] { "ConditionId" });
            DropIndex("dbo.Medical_PersonConditions", new[] { "PersonId" });
            DropIndex("dbo.Medical_PersonDietaryRequirements", new[] { "DietaryRequirementId" });
            DropIndex("dbo.Medical_PersonDietaryRequirements", new[] { "PersonId" });
            DropIndex("dbo.People_Contacts", new[] { "PersonId" });
            DropIndex("dbo.Docs_PersonDocuments", new[] { "DocumentId" });
            DropIndex("dbo.Docs_PersonDocuments", new[] { "PersonId" });
            DropIndex("dbo.Docs_Documents", new[] { "UploaderId" });
            DropIndex("dbo.Docs_Documents", new[] { "TypeId" });
            DropIndex("dbo.Pastoral_RegGroups", new[] { "YearGroupId" });
            DropIndex("dbo.Pastoral_RegGroups", new[] { "TutorId" });
            DropIndex("dbo.Pastoral_YearGroups", new[] { "HeadId" });
            DropIndex("dbo.SenGiftedTalenteds", new[] { "SubjectId" });
            DropIndex("dbo.SenGiftedTalenteds", new[] { "StudentId" });
            DropIndex("dbo.Curriculum_Subjects", new[] { "LeaderId" });
            DropIndex("dbo.Curriculum_StudyTopics", new[] { "YearGroupId" });
            DropIndex("dbo.Curriculum_StudyTopics", new[] { "SubjectId" });
            DropIndex("dbo.Curriculum_LessonPlans", new[] { "AuthorId" });
            DropIndex("dbo.Curriculum_LessonPlans", new[] { "StudyTopicId" });
            DropIndex("dbo.System_Bulletins", new[] { "AuthorId" });
            DropIndex("dbo.People_Staff", new[] { "PersonId" });
            DropIndex("dbo.Behaviour_Incidents", new[] { "RecordedById" });
            DropIndex("dbo.Behaviour_Incidents", new[] { "LocationId" });
            DropIndex("dbo.Behaviour_Incidents", new[] { "StudentId" });
            DropIndex("dbo.Behaviour_Incidents", new[] { "BehaviourTypeId" });
            DropIndex("dbo.Behaviour_Incidents", new[] { "AcademicYearId" });
            DropIndex("dbo.Behaviour_Achievements", new[] { "RecordedById" });
            DropIndex("dbo.Behaviour_Achievements", new[] { "LocationId" });
            DropIndex("dbo.Behaviour_Achievements", new[] { "StudentId" });
            DropIndex("dbo.Behaviour_Achievements", new[] { "AchievementTypeId" });
            DropIndex("dbo.Behaviour_Achievements", new[] { "AcademicYearId" });
            DropIndex("dbo.Curriculum_Classes", new[] { "YearGroupId" });
            DropIndex("dbo.Curriculum_Classes", new[] { "TeacherId" });
            DropIndex("dbo.Curriculum_Classes", new[] { "SubjectId" });
            DropIndex("dbo.Curriculum_Classes", new[] { "AcademicYearId" });
            DropIndex("dbo.Curriculum_Sessions", new[] { "PeriodId" });
            DropIndex("dbo.Curriculum_Sessions", new[] { "ClassId" });
            DropIndex("dbo.Attendance_Marks", new[] { "PeriodId" });
            DropIndex("dbo.Attendance_Marks", new[] { "WeekId" });
            DropIndex("dbo.Attendance_Marks", new[] { "StudentId" });
            DropIndex("dbo.People_Students", new[] { "SenStatusId" });
            DropIndex("dbo.People_Students", new[] { "HouseId" });
            DropIndex("dbo.People_Students", new[] { "YearGroupId" });
            DropIndex("dbo.People_Students", new[] { "RegGroupId" });
            DropIndex("dbo.People_Students", new[] { "PersonId" });
            DropIndex("dbo.Assessment_Results", new[] { "AspectId" });
            DropIndex("dbo.Assessment_Results", new[] { "StudentId" });
            DropIndex("dbo.Assessment_Results", new[] { "ResultSetId" });
            DropIndex("dbo.Assessment_Grades", new[] { "GradeSetId" });
            DropIndex("dbo.Assessment_Aspects", new[] { "GradeSetId" });
            DropIndex("dbo.Assessment_Aspects", new[] { "TypeId" });
            DropTable("dbo.System_Reports");
            DropTable("dbo.System_Areas");
            DropTable("dbo.Sen_ReviewTypes");
            DropTable("dbo.Profile_Comments");
            DropTable("dbo.Profile_CommentBanks");
            DropTable("dbo.Curriculum_LessonPlanTemplates");
            DropTable("dbo.People_ContactTypes");
            DropTable("dbo.Communication_CommunicationTypes");
            DropTable("dbo.Communication_CommunicationLogs");
            DropTable("dbo.Attendance_Meanings");
            DropTable("dbo.Attendance_Codes");
            DropTable("dbo.Assessment_AspectTypes");
            DropTable("dbo.Sen_Statuses");
            DropTable("dbo.Sen_ProvisionTypes");
            DropTable("dbo.Sen_Provisions");
            DropTable("dbo.Sen_EventTypes");
            DropTable("dbo.Sen_Events");
            DropTable("dbo.Curriculum_Enrolments");
            DropTable("dbo.Finance_ProductTypes");
            DropTable("dbo.Finance_BasketItems");
            DropTable("dbo.Finance_Products");
            DropTable("dbo.Finance_Sales");
            DropTable("dbo.Attendance_Weeks");
            DropTable("dbo.Behaviour_AchievementTypes");
            DropTable("dbo.Behaviour_IncidentTypes");
            DropTable("dbo.Profile_LogTypes");
            DropTable("dbo.Profile_Logs");
            DropTable("dbo.Personnel_TrainingCourses");
            DropTable("dbo.Personnel_TrainingCertificates");
            DropTable("dbo.Personnel_Observations");
            DropTable("dbo.Pastoral_Houses");
            DropTable("dbo.Medical_Events");
            DropTable("dbo.Docs_DocumentTypes");
            DropTable("dbo.School_Types");
            DropTable("dbo.School_Phases");
            DropTable("dbo.School_IntakeTypes");
            DropTable("dbo.School_GovernanceTypes");
            DropTable("dbo.System_Schools");
            DropTable("dbo.Communication_PhoneNumberTypes");
            DropTable("dbo.Communication_PhoneNumbers");
            DropTable("dbo.Medical_Conditions");
            DropTable("dbo.Medical_PersonConditions");
            DropTable("dbo.Medical_DietaryRequirements");
            DropTable("dbo.Medical_PersonDietaryRequirements");
            DropTable("dbo.People_Contacts");
            DropTable("dbo.People_Persons");
            DropTable("dbo.Docs_PersonDocuments");
            DropTable("dbo.Docs_Documents");
            DropTable("dbo.Pastoral_RegGroups");
            DropTable("dbo.Pastoral_YearGroups");
            DropTable("dbo.SenGiftedTalenteds");
            DropTable("dbo.Curriculum_Subjects");
            DropTable("dbo.Curriculum_StudyTopics");
            DropTable("dbo.Curriculum_LessonPlans");
            DropTable("dbo.System_Bulletins");
            DropTable("dbo.People_Staff");
            DropTable("dbo.Behaviour_Incidents");
            DropTable("dbo.School_Locations");
            DropTable("dbo.Behaviour_Achievements");
            DropTable("dbo.Curriculum_AcademicYears");
            DropTable("dbo.Curriculum_Classes");
            DropTable("dbo.Curriculum_Sessions");
            DropTable("dbo.Attendance_Periods");
            DropTable("dbo.Attendance_Marks");
            DropTable("dbo.People_Students");
            DropTable("dbo.Assessment_ResultSets");
            DropTable("dbo.Assessment_Results");
            DropTable("dbo.Assessment_Grades");
            DropTable("dbo.Assessment_GradeSets");
            DropTable("dbo.Assessment_Aspects");
        }
    }
}
