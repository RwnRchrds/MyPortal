using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortal.Database.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    FirstDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastDate = table.Column<DateTime>(type: "date", nullable: false),
                    Locked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DefaultPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    HouseNumber = table.Column<string>(maxLength: 128, nullable: true),
                    HouseName = table.Column<string>(maxLength: 128, nullable: true),
                    Apartment = table.Column<string>(maxLength: 128, nullable: true),
                    Street = table.Column<string>(maxLength: 256, nullable: false),
                    District = table.Column<string>(maxLength: 256, nullable: true),
                    Town = table.Column<string>(maxLength: 256, nullable: false),
                    County = table.Column<string>(maxLength: 256, nullable: false),
                    Postcode = table.Column<string>(maxLength: 128, nullable: false),
                    Country = table.Column<string>(maxLength: 128, nullable: false),
                    Validated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspectTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCodeMeanings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodeMeanings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Resolved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentBanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumYearGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    KeyStage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumYearGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetentionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetentionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventAttendeeResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventAttendeeResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true),
                    System = table.Column<bool>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaryRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    StaffOnly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directories_Directories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Staff = table.Column<bool>(nullable: false),
                    Student = table.Column<bool>(nullable: false),
                    Contact = table.Column<bool>(nullable: false),
                    General = table.Column<bool>(nullable: false),
                    Sen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddressTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamAssessmentModes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAssessmentModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamBoards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Abbreviation = table.Column<string>(maxLength: 20, nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    Code = table.Column<string>(maxLength: 5, nullable: true),
                    Domestic = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamBoards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamQualifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernanceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    DefaultPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntakeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlanTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    PlanTemplate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalAuthorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LeaCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogNoteTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: false),
                    IconClass = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogNoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObservationOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    IsMeal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomClosureReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false),
                    Exam = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomClosureReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPhases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPhases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenProvisionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenProvisionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenReviewTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenReviewTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffAbsenceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false),
                    Illness = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAbsenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffIllnessTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffIllnessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GroupType = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStaffMemberRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMemberRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemAreas_SystemAreas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SystemAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Setting = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Personal = table.Column<bool>(nullable: false),
                    ColourCode = table.Column<string>(nullable: false),
                    System = table.Column<bool>(nullable: false),
                    Reserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificateStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserType = table.Column<string>(maxLength: 5, nullable: true),
                    SelectedAcademicYearId = table.Column<Guid>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicYears_SelectedAcademicYearId",
                        column: x => x.SelectedAcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceWeekPatterns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceWeekPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceWeekPatterns_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    MeaningId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Statutory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceCodes_AttendanceCodeMeanings_MeaningId",
                        column: x => x.MeaningId,
                        principalTable: "AttendanceCodeMeanings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CommentBankId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_CommentBanks_CommentBankId",
                        column: x => x.CommentBankId,
                        principalTable: "CommentBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    CommunicationTypeId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Outgoing = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationLogs_CommunicationTypes_CommunicationTypeId",
                        column: x => x.CommunicationTypeId,
                        principalTable: "CommunicationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BlockId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumGroups_CurriculumBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "CurriculumBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBands",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    CurriculumYearGroupId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumBands_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumBands_CurriculumYearGroups_CurriculumYearGroupId",
                        column: x => x.CurriculumYearGroupId,
                        principalTable: "CurriculumYearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    EventTypeId = table.Column<Guid>(nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    Hours = table.Column<int>(nullable: false),
                    Days = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEventTemplates_DiaryEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "DiaryEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agencies_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agencies_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agencies_AgencyTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AgencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SubmitOnline = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkItems_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamQualificationLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    QualificationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQualificationLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamQualificationLevels_ExamQualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "ExamQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aspects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false),
                    GradeSetId = table.Column<Guid>(nullable: true),
                    MaxMark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    StudentVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aspects_GradeSets_GradeSetId",
                        column: x => x.GradeSetId,
                        principalTable: "GradeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aspects_AspectTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AspectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GradeSetId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<int>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_GradeSets_GradeSetId",
                        column: x => x.GradeSetId,
                        principalTable: "GradeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LocationId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    MaxGroupSize = table.Column<int>(nullable: false),
                    TelephoneNo = table.Column<string>(nullable: true),
                    ExcludeFromCover = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductTypeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    OnceOnly = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(nullable: false),
                    CalendarYear = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSeasons_ResultSets_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetTemplateGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    MarksheetTemplateId = table.Column<Guid>(nullable: false),
                    StudentGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetTemplateGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarksheetTemplateGroups_MarksheetTemplates_MarksheetTemplateId",
                        column: x => x.MarksheetTemplateId,
                        principalTable: "MarksheetTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarksheetTemplateGroups_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectCodeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Code = table.Column<string>(maxLength: 5, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectCodes_SubjectCodeId",
                        column: x => x.SubjectCodeId,
                        principalTable: "SubjectCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AreaId = table.Column<Guid>(nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 128, nullable: false),
                    FullDescription = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetPermissions_SystemAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "SystemAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AreaId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    Restricted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_SystemAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "SystemAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bulletins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    StaffOnly = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulletins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bulletins_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bulletins_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    FileId = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Public = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 256, nullable: true),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    ChosenFirstName = table.Column<string>(maxLength: 256, nullable: true),
                    PhotoId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<string>(maxLength: 10, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Deceased = table.Column<DateTime>(type: "date", nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendancePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekPatternId = table.Column<Guid>(nullable: false),
                    Weekday = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    AmReg = table.Column<bool>(nullable: false),
                    PmReg = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendancePeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendancePeriods_AttendanceWeekPatterns_WeekPatternId",
                        column: x => x.WeekPatternId,
                        principalTable: "AttendanceWeekPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekPatternId = table.Column<Guid>(nullable: false),
                    Beginning = table.Column<DateTime>(type: "date", nullable: false),
                    IsNonTimetable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceWeeks_AttendanceWeekPatterns_WeekPatternId",
                        column: x => x.WeekPatternId,
                        principalTable: "AttendanceWeekPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBandBlockAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BlockId = table.Column<Guid>(nullable: false),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBandBlockAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumBandBlockAssignments_CurriculumBands_BandId",
                        column: x => x.BandId,
                        principalTable: "CurriculumBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculumBandBlockAssignments_CurriculumBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "CurriculumBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TemplateId = table.Column<Guid>(nullable: false),
                    AspectId = table.Column<Guid>(nullable: false),
                    ResultSetId = table.Column<Guid>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    ReadOnly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarksheetColumns_Aspects_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarksheetColumns_ResultSets_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarksheetColumns_MarksheetTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "MarksheetTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventTypeId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: true),
                    Subject = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Location = table.Column<string>(maxLength: 256, nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    IsAllDay = table.Column<bool>(nullable: false),
                    IsBlock = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    IsStudentVisible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEvents_DiaryEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "DiaryEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryEvents_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoomId = table.Column<Guid>(nullable: false),
                    Columns = table.Column<int>(nullable: false),
                    Rows = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomClosures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoomId = table.Column<Guid>(nullable: false),
                    ReasonId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomClosures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomClosures_RoomClosureReasons_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "RoomClosureReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomClosures_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamResultsEmbargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamSeasonId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResultsEmbargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResultsEmbargoes_ExamSeasons_ExamSeasonId",
                        column: x => x.ExamSeasonId,
                        principalTable: "ExamSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamBoardId = table.Column<Guid>(nullable: false),
                    ExamSeasonId = table.Column<Guid>(nullable: false),
                    SeriesCode = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSeries_ExamBoards_ExamBoardId",
                        column: x => x.ExamBoardId,
                        principalTable: "ExamBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamSeries_ExamSeasons_ExamSeasonId",
                        column: x => x.ExamSeasonId,
                        principalTable: "ExamSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRolePermissions_AspNetPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "AspNetPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetRolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressPeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AddressId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressPeople_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressPeople_People_AddressId",
                        column: x => x.AddressId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgencyId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    JobTitle = table.Column<string>(maxLength: 128, nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agents_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    ParentalBallot = table.Column<bool>(nullable: false),
                    PlaceOfWork = table.Column<string>(maxLength: 256, nullable: true),
                    JobTitle = table.Column<string>(maxLength: 256, nullable: true),
                    NiNumber = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    AgencyId = table.Column<Guid>(nullable: true),
                    Address = table.Column<string>(maxLength: 128, nullable: false),
                    Main = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_EmailAddressTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmailAddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    ConditionId = table.Column<Guid>(nullable: false),
                    MedicationTaken = table.Column<bool>(nullable: false),
                    Medication = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonConditions_MedicalConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "MedicalConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonConditions_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonDietaryRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    DietaryRequirementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDietaryRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDietaryRequirements_DietaryRequirements_DietaryRequirementId",
                        column: x => x.DietaryRequirementId,
                        principalTable: "DietaryRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDietaryRequirements_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    AgencyId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(maxLength: 128, nullable: true),
                    Main = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_PhoneNumberTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    LocalAuthorityId = table.Column<Guid>(nullable: false),
                    EstablishmentNumber = table.Column<int>(nullable: false),
                    Urn = table.Column<string>(maxLength: 128, nullable: false),
                    Uprn = table.Column<string>(maxLength: 128, nullable: false),
                    PhaseId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false),
                    GovernanceTypeId = table.Column<Guid>(nullable: false),
                    IntakeTypeId = table.Column<Guid>(nullable: false),
                    HeadTeacherId = table.Column<Guid>(nullable: true),
                    TelephoneNo = table.Column<string>(maxLength: 128, nullable: true),
                    FaxNo = table.Column<string>(maxLength: 128, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 128, nullable: true),
                    Website = table.Column<string>(maxLength: 128, nullable: true),
                    Local = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_GovernanceTypes_GovernanceTypeId",
                        column: x => x.GovernanceTypeId,
                        principalTable: "GovernanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_People_HeadTeacherId",
                        column: x => x.HeadTeacherId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_IntakeTypes_IntakeTypeId",
                        column: x => x.IntakeTypeId,
                        principalTable: "IntakeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_LocalAuthorities_LocalAuthorityId",
                        column: x => x.LocalAuthorityId,
                        principalTable: "LocalAuthorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_SchoolPhases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "SchoolPhases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_SchoolTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SchoolTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LineManagerId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    NiNumber = table.Column<string>(maxLength: 128, nullable: true),
                    PostNominal = table.Column<string>(maxLength: 128, nullable: true),
                    TeachingStaff = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMembers_StaffMembers_LineManagerId",
                        column: x => x.LineManagerId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffMembers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    AssignedToId = table.Column<Guid>(nullable: false),
                    AssignedById = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_AssignedById",
                        column: x => x.AssignedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_People_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventAttendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    ResponseId = table.Column<Guid>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Attended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventAttendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendees_DiaryEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "DiaryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendees_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendees_DiaryEventAttendeeResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "DiaryEventAttendeeResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamAwards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamSeriesId = table.Column<Guid>(nullable: false),
                    QualificationId = table.Column<Guid>(nullable: false),
                    ExternalTitle = table.Column<string>(nullable: true),
                    InternalTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AwardCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAwards_ExamSeries_ExamSeriesId",
                        column: x => x.ExamSeriesId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAwards_ExamQualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "ExamQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DetentionTypeId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    SupervisorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detentions_DetentionTypes_DetentionTypeId",
                        column: x => x.DetentionTypeId,
                        principalTable: "DetentionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detentions_DiaryEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "DiaryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detentions_StaffMembers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    HeadId = table.Column<Guid>(nullable: true),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_StaffMembers_HeadId",
                        column: x => x.HeadId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    ObserveeId = table.Column<Guid>(nullable: false),
                    ObserverId = table.Column<Guid>(nullable: false),
                    OutcomeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_StaffMembers_ObserveeId",
                        column: x => x.ObserveeId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observations_StaffMembers_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observations_ObservationOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "ObservationOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffAbsences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StaffMemberId = table.Column<Guid>(nullable: false),
                    AbsenceTypeId = table.Column<Guid>(nullable: false),
                    IllnessTypeId = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AnnualLeave = table.Column<bool>(nullable: false),
                    Confidential = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAbsences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAbsences_StaffAbsenceTypes_AbsenceTypeId",
                        column: x => x.AbsenceTypeId,
                        principalTable: "StaffAbsenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffAbsences_StaffIllnessTypes_IllnessTypeId",
                        column: x => x.IllnessTypeId,
                        principalTable: "StaffIllnessTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffAbsences_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStaffMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(nullable: false),
                    StaffMemberId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMembers_SubjectStaffMemberRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SubjectStaffMemberRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMembers_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMembers_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CourseId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCertificates_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingCertificates_StaffMembers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingCertificates_TrainingCertificateStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TrainingCertificateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    HeadId = table.Column<Guid>(nullable: true),
                    CurriculumYearGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearGroups_CurriculumYearGroups_CurriculumYearGroupId",
                        column: x => x.CurriculumYearGroupId,
                        principalTable: "CurriculumYearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearGroups_StaffMembers_HeadId",
                        column: x => x.HeadId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(nullable: false),
                    LevelId = table.Column<Guid>(nullable: false),
                    AwardId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_ExamAwards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "ExamAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Courses_ExamQualificationLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ExamQualificationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamAwardId = table.Column<Guid>(nullable: false),
                    QcaCodeId = table.Column<Guid>(nullable: false),
                    Qan = table.Column<string>(nullable: true),
                    LevelId = table.Column<Guid>(nullable: false),
                    EntryAspectId = table.Column<Guid>(nullable: false),
                    ResultAspectId = table.Column<Guid>(nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(10, 2)", nullable: false),
                    InternalTitle = table.Column<string>(nullable: true),
                    ExternalTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EntryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamElements_Aspects_EntryAspectId",
                        column: x => x.EntryAspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamElements_ExamAwards_ExamAwardId",
                        column: x => x.ExamAwardId,
                        principalTable: "ExamAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamElements_SubjectCodes_QcaCodeId",
                        column: x => x.QcaCodeId,
                        principalTable: "SubjectCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamElements_Aspects_ResultAspectId",
                        column: x => x.ResultAspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TutorId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegGroups_StaffMembers_TutorId",
                        column: x => x.TutorId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegGroups_YearGroups_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyTopics_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyTopics_YearGroups_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CourseId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: true),
                    YearGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_CurriculumGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CurriculumGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_YearGroups_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamElementId = table.Column<Guid>(nullable: false),
                    AssessmentModeId = table.Column<Guid>(nullable: false),
                    AspectId = table.Column<Guid>(nullable: false),
                    InternalTitle = table.Column<string>(nullable: true),
                    ExternalTitle = table.Column<string>(nullable: true),
                    ComponentCode = table.Column<string>(nullable: true),
                    DateDue = table.Column<DateTime>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: true),
                    IsTimetabled = table.Column<bool>(nullable: false),
                    MaximumMark = table.Column<int>(nullable: false),
                    ExaminationDate = table.Column<DateTime>(nullable: true),
                    ExamSessionId = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamComponents_Aspects_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamAssessmentModes_AssessmentModeId",
                        column: x => x.AssessmentModeId,
                        principalTable: "ExamAssessmentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamElements_ExamElementId",
                        column: x => x.ExamElementId,
                        principalTable: "ExamElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamSessions_ExamSessionId",
                        column: x => x.ExamSessionId,
                        principalTable: "ExamSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    RegGroupId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: false),
                    HouseId = table.Column<Guid>(nullable: true),
                    CandidateNumber = table.Column<string>(maxLength: 128, nullable: true),
                    AdmissionNumber = table.Column<int>(nullable: false),
                    DateStarting = table.Column<DateTime>(type: "date", nullable: true),
                    DateLeaving = table.Column<DateTime>(type: "date", nullable: true),
                    AccountBalance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FreeSchoolMeals = table.Column<bool>(nullable: false),
                    SenStatusId = table.Column<Guid>(nullable: true),
                    PupilPremium = table.Column<bool>(nullable: false),
                    Upn = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    Uci = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_RegGroups_RegGroupId",
                        column: x => x.RegGroupId,
                        principalTable: "RegGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_SenStatus_SenStatusId",
                        column: x => x.SenStatusId,
                        principalTable: "SenStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_YearGroups_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudyTopicId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    DirectoryId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    LearningObjectives = table.Column<string>(nullable: false),
                    PlanContent = table.Column<string>(nullable: false),
                    Homework = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonPlans_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonPlans_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonPlans_StudyTopics_StudyTopicId",
                        column: x => x.StudyTopicId,
                        principalTable: "StudyTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ClassId = table.Column<Guid>(nullable: false),
                    PeriodId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    RoomId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_AttendancePeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_StaffMembers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    AchievementTypeId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    RecordedById = table.Column<Guid>(nullable: false),
                    OutcomeId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_AchievementTypes_AchievementTypeId",
                        column: x => x.AchievementTypeId,
                        principalTable: "AchievementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_AchievementOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "AchievementOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievements_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    WeekId = table.Column<Guid>(nullable: false),
                    PeriodId = table.Column<Guid>(nullable: false),
                    Mark = table.Column<string>(maxLength: 1, nullable: false),
                    Comments = table.Column<string>(maxLength: 256, nullable: true),
                    MinutesLate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_AttendancePeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_AttendanceWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "AttendanceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketItems_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBandMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    BandId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBandMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumBandMemberships_CurriculumBands_BandId",
                        column: x => x.BandId,
                        principalTable: "CurriculumBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculumBandMemberships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumGroupMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumGroupMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumGroupMemberships_CurriculumGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "CurriculumGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurriculumGroupMemberships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftedTalentedStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    Notes = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftedTalentedStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftedTalentedStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftedTalentedStudents_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    HomeworkId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    PointsAchieved = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_HomeworkItems_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "HomeworkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmissions_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    BehaviourTypeId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    RecordedById = table.Column<Guid>(nullable: false),
                    OutcomeId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidents_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_IncidentTypes_BehaviourTypeId",
                        column: x => x.BehaviourTypeId,
                        principalTable: "IncidentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_BehaviourOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "BehaviourOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_BehaviourStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BehaviourStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogNotes_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_LogNoteTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LogNoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    RecordedById = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalEvents_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalEvents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    AspectId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    GradeId = table.Column<Guid>(nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Aspects_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Grades_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_ResultSets_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Processed = table.Column<bool>(nullable: false),
                    Refunded = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    EventTypeId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenEvents_SenEventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "SenEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenEvents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenProvisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    ProvisionTypeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenProvisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenProvisions_SenProvisionTypes_ProvisionTypeId",
                        column: x => x.ProvisionTypeId,
                        principalTable: "SenProvisionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenProvisions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    ReviewTypeId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Outcome = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenReviews_SenReviewTypes_ReviewTypeId",
                        column: x => x.ReviewTypeId,
                        principalTable: "SenReviewTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenReviews_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentAgentRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    AgentId = table.Column<Guid>(nullable: false),
                    RelationshipTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAgentRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAgentRelationships_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAgentRelationships_AgentRelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "AgentRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAgentRelationships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentContactRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RelationshipTypeId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false),
                    Correspondence = table.Column<bool>(nullable: false),
                    ParentalResponsibility = table.Column<bool>(nullable: false),
                    PupilReport = table.Column<bool>(nullable: false),
                    CourtOrder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentContactRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentContactRelationships_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentContactRelationships_ContactRelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "ContactRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentContactRelationships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoverArrangements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekId = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    RoomId = table.Column<Guid>(nullable: true),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverArrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoverArrangements_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoverArrangements_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoverArrangements_StaffMembers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoverArrangements_AttendanceWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "AttendanceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentDetentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    IncidentId = table.Column<Guid>(nullable: false),
                    DetentionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentDetentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentDetentions_Detentions_DetentionId",
                        column: x => x.DetentionId,
                        principalTable: "Detentions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentDetentions_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AcademicYearId",
                table: "Achievements",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AchievementTypeId",
                table: "Achievements",
                column: "AchievementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_LocationId",
                table: "Achievements",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_OutcomeId",
                table: "Achievements",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_RecordedById",
                table: "Achievements",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_StudentId",
                table: "Achievements",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressPeople_AddressId",
                table: "AddressPeople",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_AddressId",
                table: "Agencies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_DirectoryId",
                table: "Agencies",
                column: "DirectoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_TypeId",
                table: "Agencies",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgencyId",
                table: "Agents",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PersonId",
                table: "Agents",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_GradeSetId",
                table: "Aspects",
                column: "GradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_TypeId",
                table: "Aspects",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetPermissions_AreaId",
                table: "AspNetPermissions",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetPermissions_ClaimValue",
                table: "AspNetPermissions",
                column: "ClaimValue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolePermissions_PermissionId",
                table: "AspNetRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolePermissions_RoleId",
                table: "AspNetRolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SelectedAcademicYearId",
                table: "AspNetUsers",
                column: "SelectedAcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceCodes_MeaningId",
                table: "AttendanceCodes",
                column: "MeaningId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_PeriodId",
                table: "AttendanceMarks",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_StudentId",
                table: "AttendanceMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_WeekId",
                table: "AttendanceMarks",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendancePeriods_WeekPatternId",
                table: "AttendancePeriods",
                column: "WeekPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceWeekPatterns_AcademicYearId",
                table: "AttendanceWeekPatterns",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceWeeks_WeekPatternId",
                table: "AttendanceWeeks",
                column: "WeekPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_StudentId",
                table: "BasketItems",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_AuthorId",
                table: "Bulletins",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_DirectoryId",
                table: "Bulletins",
                column: "DirectoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AcademicYearId",
                table: "Classes",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GroupId",
                table: "Classes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_YearGroupId",
                table: "Classes",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentBankId",
                table: "Comments",
                column: "CommentBankId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationLogs_CommunicationTypeId",
                table: "CommunicationLogs",
                column: "CommunicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AwardId",
                table: "Courses",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelId",
                table: "Courses",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CoverArrangements_RoomId",
                table: "CoverArrangements",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CoverArrangements_SessionId",
                table: "CoverArrangements",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CoverArrangements_TeacherId",
                table: "CoverArrangements",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CoverArrangements_WeekId",
                table: "CoverArrangements",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBandBlockAssignments_BandId",
                table: "CurriculumBandBlockAssignments",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBandBlockAssignments_BlockId",
                table: "CurriculumBandBlockAssignments",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBandMemberships_BandId",
                table: "CurriculumBandMemberships",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBandMemberships_StudentId",
                table: "CurriculumBandMemberships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBands_CurriculumYearGroupId",
                table: "CurriculumBands",
                column: "CurriculumYearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBands_AcademicYearId_Code",
                table: "CurriculumBands",
                columns: new[] { "AcademicYearId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumGroupMemberships_GroupId",
                table: "CurriculumGroupMemberships",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumGroupMemberships_StudentId",
                table: "CurriculumGroupMemberships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumGroups_BlockId",
                table: "CurriculumGroups",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Detentions_DetentionTypeId",
                table: "Detentions",
                column: "DetentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Detentions_EventId",
                table: "Detentions",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detentions_SupervisorId",
                table: "Detentions",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendees_EventId",
                table: "DiaryEventAttendees",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendees_PersonId",
                table: "DiaryEventAttendees",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendees_ResponseId",
                table: "DiaryEventAttendees",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEvents_EventTypeId",
                table: "DiaryEvents",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEvents_RoomId",
                table: "DiaryEvents",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventTemplates_EventTypeId",
                table: "DiaryEventTemplates",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Directories_ParentId",
                table: "Directories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_CreatedById",
                table: "Documents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DirectoryId",
                table: "Documents",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeId",
                table: "Documents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_PersonId",
                table: "EmailAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_TypeId",
                table: "EmailAddresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwards_ExamSeriesId",
                table: "ExamAwards",
                column: "ExamSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwards_QualificationId",
                table: "ExamAwards",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_AspectId",
                table: "ExamComponents",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_AssessmentModeId",
                table: "ExamComponents",
                column: "AssessmentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_ExamElementId",
                table: "ExamComponents",
                column: "ExamElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_ExamSessionId",
                table: "ExamComponents",
                column: "ExamSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_EntryAspectId",
                table: "ExamElements",
                column: "EntryAspectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_ExamAwardId",
                table: "ExamElements",
                column: "ExamAwardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_QcaCodeId",
                table: "ExamElements",
                column: "QcaCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_ResultAspectId",
                table: "ExamElements",
                column: "ResultAspectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQualificationLevels_QualificationId",
                table: "ExamQualificationLevels",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResultsEmbargoes_ExamSeasonId",
                table: "ExamResultsEmbargoes",
                column: "ExamSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRooms_RoomId",
                table: "ExamRooms",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeasons_ResultSetId",
                table: "ExamSeasons",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeries_ExamBoardId",
                table: "ExamSeries",
                column: "ExamBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeries_ExamSeasonId",
                table: "ExamSeries",
                column: "ExamSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftedTalentedStudents_StudentId",
                table: "GiftedTalentedStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftedTalentedStudents_SubjectId",
                table: "GiftedTalentedStudents",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradeSetId",
                table: "Grades",
                column: "GradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkItems_DirectoryId",
                table: "HomeworkItems",
                column: "DirectoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_DocumentId",
                table: "HomeworkSubmissions",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_HomeworkId",
                table: "HomeworkSubmissions",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_StudentId",
                table: "HomeworkSubmissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_TaskId",
                table: "HomeworkSubmissions",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_HeadId",
                table: "Houses",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetentions_DetentionId",
                table: "IncidentDetentions",
                column: "DetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetentions_IncidentId",
                table: "IncidentDetentions",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AcademicYearId",
                table: "Incidents",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_BehaviourTypeId",
                table: "Incidents",
                column: "BehaviourTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_LocationId",
                table: "Incidents",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_OutcomeId",
                table: "Incidents",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_RecordedById",
                table: "Incidents",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_StatusId",
                table: "Incidents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_StudentId",
                table: "Incidents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlans_AuthorId",
                table: "LessonPlans",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlans_DirectoryId",
                table: "LessonPlans",
                column: "DirectoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlans_StudyTopicId",
                table: "LessonPlans",
                column: "StudyTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotes_AcademicYearId",
                table: "LogNotes",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotes_CreatedById",
                table: "LogNotes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotes_StudentId",
                table: "LogNotes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotes_TypeId",
                table: "LogNotes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LogNotes_UpdatedById",
                table: "LogNotes",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetColumns_AspectId",
                table: "MarksheetColumns",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetColumns_ResultSetId",
                table: "MarksheetColumns",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetColumns_TemplateId",
                table: "MarksheetColumns",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetTemplateGroups_MarksheetTemplateId",
                table: "MarksheetTemplateGroups",
                column: "MarksheetTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetTemplateGroups_StudentGroupId",
                table: "MarksheetTemplateGroups",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvents_RecordedById",
                table: "MedicalEvents",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvents_StudentId",
                table: "MedicalEvents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_ObserveeId",
                table: "Observations",
                column: "ObserveeId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_ObserverId",
                table: "Observations",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_OutcomeId",
                table: "Observations",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DirectoryId",
                table: "People",
                column: "DirectoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_UserId",
                table: "People",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConditions_ConditionId",
                table: "PersonConditions",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConditions_PersonId",
                table: "PersonConditions",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDietaryRequirements_DietaryRequirementId",
                table: "PersonDietaryRequirements",
                column: "DietaryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDietaryRequirements_PersonId",
                table: "PersonDietaryRequirements",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_PersonId",
                table: "PhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_TypeId",
                table: "PhoneNumbers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroups_TutorId",
                table: "RegGroups",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroups_YearGroupId",
                table: "RegGroups",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AreaId",
                table: "Reports",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AspectId",
                table: "Results",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_GradeId",
                table: "Results",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ResultSetId",
                table: "Results",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StudentId",
                table: "Results",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClosures_ReasonId",
                table: "RoomClosures",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClosures_RoomId",
                table: "RoomClosures",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_AcademicYearId",
                table: "Sales",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_StudentId",
                table: "Sales",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_GovernanceTypeId",
                table: "Schools",
                column: "GovernanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_HeadTeacherId",
                table: "Schools",
                column: "HeadTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_IntakeTypeId",
                table: "Schools",
                column: "IntakeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_LocalAuthorityId",
                table: "Schools",
                column: "LocalAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_PhaseId",
                table: "Schools",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_TypeId",
                table: "Schools",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenEvents_EventTypeId",
                table: "SenEvents",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenEvents_StudentId",
                table: "SenEvents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SenProvisions_ProvisionTypeId",
                table: "SenProvisions",
                column: "ProvisionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenProvisions_StudentId",
                table: "SenProvisions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReviews_ReviewTypeId",
                table: "SenReviews",
                column: "ReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReviews_StudentId",
                table: "SenReviews",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClassId",
                table: "Sessions",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_PeriodId",
                table: "Sessions",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RoomId",
                table: "Sessions",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TeacherId",
                table: "Sessions",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAbsences_AbsenceTypeId",
                table: "StaffAbsences",
                column: "AbsenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAbsences_IllnessTypeId",
                table: "StaffAbsences",
                column: "IllnessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAbsences_StaffMemberId",
                table: "StaffAbsences",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_LineManagerId",
                table: "StaffMembers",
                column: "LineManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_PersonId",
                table: "StaffMembers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgentRelationships_AgentId",
                table: "StudentAgentRelationships",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgentRelationships_RelationshipTypeId",
                table: "StudentAgentRelationships",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAgentRelationships_StudentId",
                table: "StudentAgentRelationships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContactRelationships_ContactId",
                table: "StudentContactRelationships",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContactRelationships_RelationshipTypeId",
                table: "StudentContactRelationships",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContactRelationships_StudentId",
                table: "StudentContactRelationships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HouseId",
                table: "Students",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonId",
                table: "Students",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RegGroupId",
                table: "Students",
                column: "RegGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SenStatusId",
                table: "Students",
                column: "SenStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_YearGroupId",
                table: "Students",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTopics_SubjectId",
                table: "StudyTopics",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTopics_YearGroupId",
                table: "StudyTopics",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectCodeId",
                table: "Subjects",
                column: "SubjectCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMembers_RoleId",
                table: "SubjectStaffMembers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMembers_StaffMemberId",
                table: "SubjectStaffMembers",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMembers_SubjectId",
                table: "SubjectStaffMembers",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemAreas_ParentId",
                table: "SystemAreas",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedById",
                table: "Tasks",
                column: "AssignedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedToId",
                table: "Tasks",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TypeId",
                table: "Tasks",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificates_CourseId",
                table: "TrainingCertificates",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificates_StaffId",
                table: "TrainingCertificates",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificates_StatusId",
                table: "TrainingCertificates",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroups_CurriculumYearGroupId",
                table: "YearGroups",
                column: "CurriculumYearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroups_HeadId",
                table: "YearGroups",
                column: "HeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "AddressPeople");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRolePermissions");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttendanceCodes");

            migrationBuilder.DropTable(
                name: "AttendanceMarks");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Bulletins");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CommunicationLogs");

            migrationBuilder.DropTable(
                name: "CoverArrangements");

            migrationBuilder.DropTable(
                name: "CurriculumBandBlockAssignments");

            migrationBuilder.DropTable(
                name: "CurriculumBandMemberships");

            migrationBuilder.DropTable(
                name: "CurriculumGroupMemberships");

            migrationBuilder.DropTable(
                name: "DiaryEventAttendees");

            migrationBuilder.DropTable(
                name: "DiaryEventTemplates");

            migrationBuilder.DropTable(
                name: "EmailAddresses");

            migrationBuilder.DropTable(
                name: "ExamComponents");

            migrationBuilder.DropTable(
                name: "ExamResultsEmbargoes");

            migrationBuilder.DropTable(
                name: "ExamRooms");

            migrationBuilder.DropTable(
                name: "ExclusionReasons");

            migrationBuilder.DropTable(
                name: "ExclusionTypes");

            migrationBuilder.DropTable(
                name: "GiftedTalentedStudents");

            migrationBuilder.DropTable(
                name: "HomeworkSubmissions");

            migrationBuilder.DropTable(
                name: "IncidentDetentions");

            migrationBuilder.DropTable(
                name: "LessonPlans");

            migrationBuilder.DropTable(
                name: "LessonPlanTemplates");

            migrationBuilder.DropTable(
                name: "LogNotes");

            migrationBuilder.DropTable(
                name: "MarksheetColumns");

            migrationBuilder.DropTable(
                name: "MarksheetTemplateGroups");

            migrationBuilder.DropTable(
                name: "MedicalEvents");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "PersonConditions");

            migrationBuilder.DropTable(
                name: "PersonDietaryRequirements");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "RoomClosures");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "SenEvents");

            migrationBuilder.DropTable(
                name: "SenProvisions");

            migrationBuilder.DropTable(
                name: "SenReviews");

            migrationBuilder.DropTable(
                name: "StaffAbsences");

            migrationBuilder.DropTable(
                name: "StudentAgentRelationships");

            migrationBuilder.DropTable(
                name: "StudentContactRelationships");

            migrationBuilder.DropTable(
                name: "SubjectStaffMembers");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "TrainingCertificates");

            migrationBuilder.DropTable(
                name: "AchievementTypes");

            migrationBuilder.DropTable(
                name: "AchievementOutcomes");

            migrationBuilder.DropTable(
                name: "AspNetPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AttendanceCodeMeanings");

            migrationBuilder.DropTable(
                name: "CommentBanks");

            migrationBuilder.DropTable(
                name: "CommunicationTypes");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "AttendanceWeeks");

            migrationBuilder.DropTable(
                name: "CurriculumBands");

            migrationBuilder.DropTable(
                name: "DiaryEventAttendeeResponses");

            migrationBuilder.DropTable(
                name: "EmailAddressTypes");

            migrationBuilder.DropTable(
                name: "ExamAssessmentModes");

            migrationBuilder.DropTable(
                name: "ExamElements");

            migrationBuilder.DropTable(
                name: "ExamSessions");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "HomeworkItems");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Detentions");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "StudyTopics");

            migrationBuilder.DropTable(
                name: "LogNoteTypes");

            migrationBuilder.DropTable(
                name: "MarksheetTemplates");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "ObservationOutcomes");

            migrationBuilder.DropTable(
                name: "MedicalConditions");

            migrationBuilder.DropTable(
                name: "DietaryRequirements");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "RoomClosureReasons");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "GovernanceTypes");

            migrationBuilder.DropTable(
                name: "IntakeTypes");

            migrationBuilder.DropTable(
                name: "LocalAuthorities");

            migrationBuilder.DropTable(
                name: "SchoolPhases");

            migrationBuilder.DropTable(
                name: "SchoolTypes");

            migrationBuilder.DropTable(
                name: "SenEventTypes");

            migrationBuilder.DropTable(
                name: "SenProvisionTypes");

            migrationBuilder.DropTable(
                name: "SenReviewTypes");

            migrationBuilder.DropTable(
                name: "StaffAbsenceTypes");

            migrationBuilder.DropTable(
                name: "StaffIllnessTypes");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "AgentRelationshipTypes");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactRelationshipTypes");

            migrationBuilder.DropTable(
                name: "SubjectStaffMemberRoles");

            migrationBuilder.DropTable(
                name: "TrainingCourses");

            migrationBuilder.DropTable(
                name: "TrainingCertificateStatus");

            migrationBuilder.DropTable(
                name: "SystemAreas");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "AttendancePeriods");

            migrationBuilder.DropTable(
                name: "Aspects");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "DetentionTypes");

            migrationBuilder.DropTable(
                name: "DiaryEvents");

            migrationBuilder.DropTable(
                name: "IncidentTypes");

            migrationBuilder.DropTable(
                name: "BehaviourOutcomes");

            migrationBuilder.DropTable(
                name: "BehaviourStatus");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CurriculumGroups");

            migrationBuilder.DropTable(
                name: "AttendanceWeekPatterns");

            migrationBuilder.DropTable(
                name: "GradeSets");

            migrationBuilder.DropTable(
                name: "AspectTypes");

            migrationBuilder.DropTable(
                name: "DiaryEventTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "RegGroups");

            migrationBuilder.DropTable(
                name: "SenStatus");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AgencyTypes");

            migrationBuilder.DropTable(
                name: "ExamAwards");

            migrationBuilder.DropTable(
                name: "ExamQualificationLevels");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "CurriculumBlocks");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "YearGroups");

            migrationBuilder.DropTable(
                name: "ExamSeries");

            migrationBuilder.DropTable(
                name: "ExamQualifications");

            migrationBuilder.DropTable(
                name: "SubjectCodes");

            migrationBuilder.DropTable(
                name: "CurriculumYearGroups");

            migrationBuilder.DropTable(
                name: "StaffMembers");

            migrationBuilder.DropTable(
                name: "ExamBoards");

            migrationBuilder.DropTable(
                name: "ExamSeasons");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ResultSets");

            migrationBuilder.DropTable(
                name: "Directories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AcademicYears");
        }
    }
}
