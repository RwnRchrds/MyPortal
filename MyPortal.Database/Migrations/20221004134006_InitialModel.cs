using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BuildingNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Apartment = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    District = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Town = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    County = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Validated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspectTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCodeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceWeekPatterns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceWeekPatterns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourRoleTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Resolved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BehaviourTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehaviourTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoarderStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoarderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeBillingPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeBillingPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentBanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBlocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumYearGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    KeyStage = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumYearGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetentionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventAttendeeResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaryRequirements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Percentage = table.Column<bool>(type: "bit", nullable: false),
                    BlockOtherDiscounts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Staff = table.Column<bool>(type: "bit", nullable: false),
                    Student = table.Column<bool>(type: "bit", nullable: false),
                    Contact = table.Column<bool>(type: "bit", nullable: false),
                    General = table.Column<bool>(type: "bit", nullable: false),
                    Sen = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddressTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrolmentStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolmentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ethnicities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamAssessmentModes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ExternallyAssessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAssessmentModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamBoards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Domestic = table.Column<bool>(type: "bit", nullable: false),
                    UseEdi = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamBoards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamQualifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    JcQualificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamSpecialArrangements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSpecialArrangements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionAppealResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionAppealResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExclusionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExclusionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernanceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DefaultPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntakeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlanTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PlanTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalAuthorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LeaCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogNoteTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogNoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarksheetTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NextOfKinRelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKinRelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObservationOutcomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationOutcomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PhotoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsMeal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permissions = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomClosureReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomClosureReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolPhases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolPhases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenProvisionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenProvisionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenReviewTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenReviewTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffAbsenceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false),
                    Authorised = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAbsenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffIllnessTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffIllnessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PromoteToGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaxMembers = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_StudentGroups_PromoteToGroupId",
                        column: x => x.PromoteToGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectCodeSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCodeSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStaffMemberRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SubjectLeader = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMemberRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Personal = table.Column<bool>(type: "bit", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificateStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AcademicTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicTerms_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AttendanceCodeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Restricted = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceCodes_AttendanceCodeTypes_AttendanceCodeTypeId",
                        column: x => x.AttendanceCodeTypeId,
                        principalTable: "AttendanceCodeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendancePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekPatternId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weekday = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    AmReg = table.Column<bool>(type: "bit", nullable: false),
                    PmReg = table.Column<bool>(type: "bit", nullable: false)
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
                name: "BuildingFloors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingFloors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingFloors_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CommentBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "DiaryEventTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agencies_AgencyTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AgencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agencies_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SubmitOnline = table.Column<bool>(type: "bit", nullable: false),
                    MaxPoints = table.Column<int>(type: "int", nullable: false)
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
                name: "ExamAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentType = table.Column<int>(type: "int", nullable: false),
                    InternalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAssessments_ExamBoards_ExamBoardId",
                        column: x => x.ExamBoardId,
                        principalTable: "ExamBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SittingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamDates_ExamSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "ExamSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aspects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MinMark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MaxMark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ColumnHeading = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aspects_AspectTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AspectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aspects_GradeSets_GradeSetId",
                        column: x => x.GradeSetId,
                        principalTable: "GradeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamQualificationLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    QualificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultGradeSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JcLevelCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    System = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_ExamQualificationLevels_GradeSets_DefaultGradeSetId",
                        column: x => x.DefaultGradeSetId,
                        principalTable: "GradeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GradeSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
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
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PreferredFirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PreferredLastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NhsNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Deceased = table.Column<DateTime>(type: "date", nullable: true),
                    EthnicityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_People_Ethnicities_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "Ethnicities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamResultEmbargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResultEmbargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamResultEmbargoes_ResultSets_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarYear = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Default = table.Column<bool>(type: "bit", nullable: false)
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
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurriculumYearGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_CurriculumBands_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_CurriculumGroups_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetTemplateGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    MarksheetTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false)
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
                name: "SubjectCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectCodeSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectCodes_SubjectCodeSets_SubjectCodeSetId",
                        column: x => x.SubjectCodeSetId,
                        principalTable: "SubjectCodeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    VatRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Variable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_VatRates_VatRateId",
                        column: x => x.VatRateId,
                        principalTable: "VatRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ShowOnStore = table.Column<bool>(type: "bit", nullable: false),
                    OrderLimit = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Products_VatRates_VatRateId",
                        column: x => x.VatRateId,
                        principalTable: "VatRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekPatternId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Beginning = table.Column<DateTime>(type: "date", nullable: false),
                    IsNonTimetable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceWeeks_AcademicTerms_AcademicTermId",
                        column: x => x.AcademicTermId,
                        principalTable: "AcademicTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceWeeks_AttendanceWeekPatterns_WeekPatternId",
                        column: x => x.WeekPatternId,
                        principalTable: "AttendanceWeekPatterns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BuildingFloorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MaxGroupSize = table.Column<int>(type: "int", nullable: false),
                    TelephoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcludeFromCover = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_BuildingFloors_BuildingFloorId",
                        column: x => x.BuildingFloorId,
                        principalTable: "BuildingFloors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExamBaseComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AssessmentModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamBaseComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamBaseComponents_ExamAssessmentModes_AssessmentModeId",
                        column: x => x.AssessmentModeId,
                        principalTable: "ExamAssessmentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamBaseComponents_ExamAssessments_ExamAssessmentId",
                        column: x => x.ExamAssessmentId,
                        principalTable: "ExamAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarksheetColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AspectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_MarksheetColumns_MarksheetTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "MarksheetTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarksheetColumns_ResultSets_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Main = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressLinks_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressLinks_AddressTypes_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressLinks_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressLinks_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Agents_AgentTypes_AgentTypeId",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentTypes",
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentalBallot = table.Column<bool>(type: "bit", nullable: false),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NiNumber = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Main = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailAddresses_EmailAddressTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmailAddressTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonConditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicationTaken = table.Column<bool>(type: "bit", nullable: false),
                    Medication = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DietaryRequirementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Main = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumbers_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocalAuthorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstablishmentNumber = table.Column<int>(type: "int", nullable: false),
                    Urn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Uprn = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GovernanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntakeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeadTeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Local = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schools_GovernanceTypes_GovernanceTypeId",
                        column: x => x.GovernanceTypeId,
                        principalTable: "GovernanceTypes",
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
                        name: "FK_Schools_People_HeadTeacherId",
                        column: x => x.HeadTeacherId,
                        principalTable: "People",
                        principalColumn: "Id");
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LineManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BankAccount = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    BankSortCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NiNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    Qualifications = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    TeachingStaff = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMembers_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffMembers_StaffMembers_LineManagerId",
                        column: x => x.LineManagerId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdmissionNumber = table.Column<int>(type: "int", nullable: false),
                    DateStarting = table.Column<DateTime>(type: "date", nullable: true),
                    DateLeaving = table.Column<DateTime>(type: "date", nullable: true),
                    FreeSchoolMeals = table.Column<bool>(type: "bit", nullable: false),
                    SenStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EnrolmentStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BoarderStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PupilPremium = table.Column<bool>(type: "bit", nullable: false),
                    Upn = table.Column<string>(type: "varchar(13)", unicode: false, maxLength: 13, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_BoarderStatus_BoarderStatusId",
                        column: x => x.BoarderStatusId,
                        principalTable: "BoarderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_EnrolmentStatus_EnrolmentStatusId",
                        column: x => x.EnrolmentStatusId,
                        principalTable: "EnrolmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_SenStatus_SenStatusId",
                        column: x => x.SenStatusId,
                        principalTable: "SenStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_SenTypes_SenTypeId",
                        column: x => x.SenTypeId,
                        principalTable: "SenTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserType = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ExamBoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamSeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "CurriculumBandBlockAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BlockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "ExamBaseElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QcaCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualAccrNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamBaseElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamBaseElements_ExamAssessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "ExamAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamBaseElements_ExamQualificationLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ExamQualificationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamBaseElements_SubjectCodes_QcaCodeId",
                        column: x => x.QcaCodeId,
                        principalTable: "SubjectCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ChargeDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeDiscounts_Charges_ChargeId",
                        column: x => x.ChargeId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChargeDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplyTo = table.Column<int>(type: "int", nullable: true),
                    ApplyToCart = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreDiscounts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreDiscounts_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllDay = table.Column<bool>(type: "bit", nullable: false),
                    Public = table.Column<bool>(type: "bit", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Columns = table.Column<int>(type: "int", nullable: false),
                    Rows = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                name: "CommunicationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommunicationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outgoing = table.Column<bool>(type: "bit", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_CommunicationLogs_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColourCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Houses_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextOfKinPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextOfKin_NextOfKinRelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "NextOfKinRelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NextOfKin_People_NextOfKinPersonId",
                        column: x => x.NextOfKinPersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NextOfKin_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    ObserveeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObserverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observations_ObservationOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "ObservationOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "StaffAbsences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AbsenceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IllnessTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Confidential = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "StudentGroupSupervisors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupSupervisors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroupSupervisors_StaffMembers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroupSupervisors_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificates", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_TrainingCertificates_TrainingCourses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurriculumYearGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_YearGroups_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YearGroups_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
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
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeBillingPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dispatched = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_ChargeBillingPeriods_ChargeBillingPeriodId",
                        column: x => x.ChargeBillingPeriodId,
                        principalTable: "ChargeBillingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamCandidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Uci = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PreviousCandidateNumber = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    PreviousCentreNumber = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    SpecialConsideration = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCandidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamCandidate_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExclusionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExclusionReasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    AppealDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppealResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exclusions_ExclusionAppealResults_AppealResultId",
                        column: x => x.AppealResultId,
                        principalTable: "ExclusionAppealResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exclusions_ExclusionReasons_ExclusionReasonId",
                        column: x => x.ExclusionReasonId,
                        principalTable: "ExclusionReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exclusions_ExclusionTypes_ExclusionTypeId",
                        column: x => x.ExclusionTypeId,
                        principalTable: "ExclusionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exclusions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehaviourTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCards_IncidentTypes_BehaviourTypeId",
                        column: x => x.BehaviourTypeId,
                        principalTable: "IncidentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportCards_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AspectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    GradeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Mark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ColourCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "SenEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvisionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Outcome = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                        name: "FK_StudentAgentRelationships_RelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "RelationshipTypes",
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
                name: "StudentCharges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeBillingPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCharges_ChargeBillingPeriods_ChargeBillingPeriodId",
                        column: x => x.ChargeBillingPeriodId,
                        principalTable: "ChargeBillingPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCharges_Charges_ChargeId",
                        column: x => x.ChargeId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentCharges_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentContactRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    RelationshipTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Correspondence = table.Column<bool>(type: "bit", nullable: false),
                    ParentalResponsibility = table.Column<bool>(type: "bit", nullable: false),
                    PupilReport = table.Column<bool>(type: "bit", nullable: false),
                    CourtOrder = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_StudentContactRelationships_RelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "RelationshipTypes",
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
                name: "StudentGroupMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroupMemberships_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroupMemberships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievementTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Achievements_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MinutesLate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceMarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_AttendanceCodes_CodeId",
                        column: x => x.CodeId,
                        principalTable: "AttendanceCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_AttendancePeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_AttendanceWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "AttendanceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMarks_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bulletins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulletins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bulletins_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bulletins_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Documents_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BehaviourTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_Incidents_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_LogNotes_LogNoteTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LogNoteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogNotes_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalEvents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalEvents_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    AllowEdit = table.Column<bool>(type: "bit", nullable: false),
                    System = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssignedById",
                        column: x => x.AssignedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAssessmentAspects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AspectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AspectOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAssessmentAspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAssessmentAspects_Aspects_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAssessmentAspects_ExamAssessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "ExamAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAssessmentAspects_ExamSeries_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BaseComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamSeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamDateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaximumMark = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamAssessmentModes_AssessmentModeId",
                        column: x => x.AssessmentModeId,
                        principalTable: "ExamAssessmentModes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamBaseComponents_BaseComponentId",
                        column: x => x.BaseComponentId,
                        principalTable: "ExamBaseComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamDates_ExamDateId",
                        column: x => x.ExamDateId,
                        principalTable: "ExamDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponents_ExamSeries_ExamSeriesId",
                        column: x => x.ExamSeriesId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BaseElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExamFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Submitted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamElements_ExamBaseElements_BaseElementId",
                        column: x => x.BaseElementId,
                        principalTable: "ExamBaseElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamElements_ExamSeries_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftedTalentedStudents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "SubjectStaffMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMembers", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_SubjectStaffMembers_SubjectStaffMemberRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SubjectStaffMemberRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentChargeDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChargeDiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentChargeDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentChargeDiscounts_ChargeDiscounts_ChargeDiscountId",
                        column: x => x.ChargeDiscountId,
                        principalTable: "ChargeDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentChargeDiscounts_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DetentionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventAttendees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false),
                    CanEdit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventAttendees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendees_DiaryEventAttendeeResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "DiaryEventAttendeeResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "ParentEvenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BookingOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingClosed = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEvenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEvenings_DiaryEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "DiaryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamRoomSeatBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatRow = table.Column<int>(type: "int", nullable: false),
                    SeatColumn = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRoomSeatBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRoomSeatBlocks_ExamRooms_ExamRoomId",
                        column: x => x.ExamRoomId,
                        principalTable: "ExamRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegGroups_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegGroups_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
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
                name: "BillAccountTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountTransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillAccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillAccountTransactions_AccountTransactions_AccountTransactionId",
                        column: x => x.AccountTransactionId,
                        principalTable: "AccountTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillAccountTransactions_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDiscounts_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillDiscounts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CustomerReceived = table.Column<bool>(type: "bit", nullable: false),
                    Refunded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamCandidateSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCandidateSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamCandidateSeries_ExamCandidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "ExamCandidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamCandidateSeries_ExamSeries_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamCandidateSpecialArrangements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialArrangementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCandidateSpecialArrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamCandidateSpecialArrangements_ExamCandidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "ExamCandidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamCandidateSpecialArrangements_ExamSpecialArrangements_SpecialArrangementId",
                        column: x => x.SpecialArrangementId,
                        principalTable: "ExamSpecialArrangements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCardEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    AttendanceWeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCardEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCardEntries_AttendancePeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportCardEntries_AttendanceWeeks_AttendanceWeekId",
                        column: x => x.AttendanceWeekId,
                        principalTable: "AttendanceWeeks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportCardEntries_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportCardEntries_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCardTargets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ReportCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCardTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCardTargets_BehaviourTargets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "BehaviourTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportCardTargets_ReportCards_ReportCardId",
                        column: x => x.ReportCardId,
                        principalTable: "ReportCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillCharges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentChargeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Refunded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillCharges_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillCharges_StudentCharges_StudentChargeId",
                        column: x => x.StudentChargeId,
                        principalTable: "StudentCharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAchievements_AchievementOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "AchievementOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAchievements_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentIncidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentIncidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentIncidents_BehaviourOutcomes_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "BehaviourOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentIncidents_BehaviourRoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "BehaviourRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentIncidents_BehaviourStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BehaviourStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentIncidents_Incidents_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentIncidents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    HomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PointsAchieved = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "ExamComponentSittings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ExtraTimePercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamComponentSittings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamComponentSittings_ExamComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "ExamComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamComponentSittings_ExamRooms_ExamRoomId",
                        column: x => x.ExamRoomId,
                        principalTable: "ExamRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamElementComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamElementComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamElementComponents_ExamComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "ExamComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamElementComponents_ExamElements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "ExamElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurriculumGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_CurriculumGroups_CurriculumGroupId",
                        column: x => x.CurriculumGroupId,
                        principalTable: "CurriculumGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Directories_DirectoryId",
                        column: x => x.DirectoryId,
                        principalTable: "Directories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamAwards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    QualificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AwardCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAwards_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ExamAwards_ExamAssessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "ExamAssessments",
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
                name: "StudyTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyTopics_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentEveningGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentEveningId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEveningGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEveningGroup_ParentEvenings_ParentEveningId",
                        column: x => x.ParentEveningId,
                        principalTable: "ParentEvenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentEveningGroup_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentEveningStaffMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentEveningId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvailableTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AppointmentLength = table.Column<int>(type: "int", nullable: false),
                    BreakLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEveningStaffMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEveningStaffMembers_ParentEvenings_ParentEveningId",
                        column: x => x.ParentEveningId,
                        principalTable: "ParentEvenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentEveningStaffMembers_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCardTargetEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCardTargetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCardTargetEntries_ReportCardEntries_EntryId",
                        column: x => x.EntryId,
                        principalTable: "ReportCardEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportCardTargetEntries_ReportCardTargets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "ReportCardTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentIncidentDetentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentIncidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetentionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentIncidentDetentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentIncidentDetentions_Detentions_DetentionId",
                        column: x => x.DetentionId,
                        principalTable: "Detentions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentIncidentDetentions_StudentIncidents_StudentIncidentId",
                        column: x => x.StudentIncidentId,
                        principalTable: "StudentIncidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeatAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SittingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatRow = table.Column<int>(type: "int", nullable: false),
                    SeatColumn = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSeatAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSeatAllocations_ExamCandidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "ExamCandidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamSeatAllocations_ExamComponentSittings_SittingId",
                        column: x => x.SittingId,
                        principalTable: "ExamComponentSittings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_AttendancePeriods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
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
                name: "ExamAwardElements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AwardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ElementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAwardElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAwardElements_ExamAwards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "ExamAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAwardElements_ExamElements_ElementId",
                        column: x => x.ElementId,
                        principalTable: "ExamElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamAwardSeries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AwardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAwardSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamAwardSeries_ExamAwards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "ExamAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamAwardSeries_ExamSeries_AwardId",
                        column: x => x.AwardId,
                        principalTable: "ExamSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamEnrolments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AwardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamEnrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamEnrolments_ExamAwards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "ExamAwards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamEnrolments_ExamCandidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "ExamCandidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudyTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PlanContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlans", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_LessonPlans_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentEveningAppointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentEveningStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEveningAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEveningAppointments_ParentEveningStaffMembers_ParentEveningStaffId",
                        column: x => x.ParentEveningStaffId,
                        principalTable: "ParentEveningStaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentEveningAppointments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentEveningBreaks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ParentEveningStaffMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentEveningBreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentEveningBreaks_ParentEveningStaffMembers_ParentEveningStaffMemberId",
                        column: x => x.ParentEveningStaffMemberId,
                        principalTable: "ParentEveningStaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoverArrangements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverArrangements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoverArrangements_AttendanceWeeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "AttendanceWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                });

            migrationBuilder.CreateTable(
                name: "LessonPlanHomeworkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LessonPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeworkItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanHomeworkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonPlanHomeworkItems_HomeworkItems_HomeworkItemId",
                        column: x => x.HomeworkItemId,
                        principalTable: "HomeworkItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonPlanHomeworkItems_LessonPlans_LessonPlanId",
                        column: x => x.LessonPlanId,
                        principalTable: "LessonPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicTerms_AcademicYearId",
                table: "AcademicTerms",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_StudentId",
                table: "AccountTransactions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AcademicYearId",
                table: "Achievements",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_AchievementTypeId",
                table: "Achievements",
                column: "AchievementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CreatedById",
                table: "Achievements",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_LocationId",
                table: "Achievements",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StudentGroupId",
                table: "Activities",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinks_AddressId",
                table: "AddressLinks",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinks_AddressTypeId",
                table: "AddressLinks",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinks_AgencyId",
                table: "AddressLinks",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressLinks_PersonId",
                table: "AddressLinks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_DirectoryId",
                table: "Agencies",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Agencies_TypeId",
                table: "Agencies",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgencyId",
                table: "Agents",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgentTypeId",
                table: "Agents",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PersonId",
                table: "Agents",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_GradeSetId",
                table: "Aspects",
                column: "GradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspects_TypeId",
                table: "Aspects",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceCodes_AttendanceCodeTypeId",
                table: "AttendanceCodes",
                column: "AttendanceCodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_CodeId",
                table: "AttendanceMarks",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMarks_CreatedById",
                table: "AttendanceMarks",
                column: "CreatedById");

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
                name: "IX_AttendanceWeeks_AcademicTermId",
                table: "AttendanceWeeks",
                column: "AcademicTermId");

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
                name: "IX_BillAccountTransactions_AccountTransactionId",
                table: "BillAccountTransactions",
                column: "AccountTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_BillAccountTransactions_BillId",
                table: "BillAccountTransactions",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillCharges_BillId",
                table: "BillCharges",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillCharges_StudentChargeId",
                table: "BillCharges",
                column: "StudentChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDiscounts_BillId",
                table: "BillDiscounts",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDiscounts_DiscountId",
                table: "BillDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_ProductId",
                table: "BillItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ChargeBillingPeriodId",
                table: "Bills",
                column: "ChargeBillingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StudentId",
                table: "Bills",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingFloors_BuildingId",
                table: "BuildingFloors",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_CreatedById",
                table: "Bulletins",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletins_DirectoryId",
                table: "Bulletins",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeDiscounts_ChargeId",
                table: "ChargeDiscounts",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeDiscounts_DiscountId",
                table: "ChargeDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_VatRateId",
                table: "Charges",
                column: "VatRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CurriculumGroupId",
                table: "Classes",
                column: "CurriculumGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_DirectoryId",
                table: "Classes",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentBankId",
                table: "Comments",
                column: "CommentBankId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationLogs_CommunicationTypeId",
                table: "CommunicationLogs",
                column: "CommunicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationLogs_ContactId",
                table: "CommunicationLogs",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonId",
                table: "Contacts",
                column: "PersonId");

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
                name: "IX_CurriculumBands_AcademicYearId",
                table: "CurriculumBands",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBands_CurriculumYearGroupId",
                table: "CurriculumBands",
                column: "CurriculumYearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumBands_StudentGroupId",
                table: "CurriculumBands",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumGroups_BlockId",
                table: "CurriculumGroups",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumGroups_StudentGroupId",
                table: "CurriculumGroups",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Detentions_DetentionTypeId",
                table: "Detentions",
                column: "DetentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Detentions_EventId",
                table: "Detentions",
                column: "EventId");

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
                name: "IX_Documents_FileId",
                table: "Documents",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeId",
                table: "Documents",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_AgencyId",
                table: "EmailAddresses",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_PersonId",
                table: "EmailAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_TypeId",
                table: "EmailAddresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssessmentAspects_AspectId",
                table: "ExamAssessmentAspects",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssessmentAspects_AssessmentId",
                table: "ExamAssessmentAspects",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssessmentAspects_SeriesId",
                table: "ExamAssessmentAspects",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAssessments_ExamBoardId",
                table: "ExamAssessments",
                column: "ExamBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwardElements_AwardId",
                table: "ExamAwardElements",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwardElements_ElementId",
                table: "ExamAwardElements",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwards_AssessmentId",
                table: "ExamAwards",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwards_CourseId",
                table: "ExamAwards",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwards_QualificationId",
                table: "ExamAwards",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAwardSeries_AwardId",
                table: "ExamAwardSeries",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBaseComponents_AssessmentModeId",
                table: "ExamBaseComponents",
                column: "AssessmentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBaseComponents_ExamAssessmentId",
                table: "ExamBaseComponents",
                column: "ExamAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBaseElements_AssessmentId",
                table: "ExamBaseElements",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBaseElements_LevelId",
                table: "ExamBaseElements",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamBaseElements_QcaCodeId",
                table: "ExamBaseElements",
                column: "QcaCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidate_StudentId",
                table: "ExamCandidate",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidateSeries_CandidateId",
                table: "ExamCandidateSeries",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidateSeries_SeriesId",
                table: "ExamCandidateSeries",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidateSpecialArrangements_CandidateId",
                table: "ExamCandidateSpecialArrangements",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCandidateSpecialArrangements_SpecialArrangementId",
                table: "ExamCandidateSpecialArrangements",
                column: "SpecialArrangementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_AssessmentModeId",
                table: "ExamComponents",
                column: "AssessmentModeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_BaseComponentId",
                table: "ExamComponents",
                column: "BaseComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_ExamDateId",
                table: "ExamComponents",
                column: "ExamDateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponents_ExamSeriesId",
                table: "ExamComponents",
                column: "ExamSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponentSittings_ComponentId",
                table: "ExamComponentSittings",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamComponentSittings_ExamRoomId",
                table: "ExamComponentSittings",
                column: "ExamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDates_SessionId",
                table: "ExamDates",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElementComponents_ComponentId",
                table: "ExamElementComponents",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElementComponents_ElementId",
                table: "ExamElementComponents",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_BaseElementId",
                table: "ExamElements",
                column: "BaseElementId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamElements_SeriesId",
                table: "ExamElements",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamEnrolments_AwardId",
                table: "ExamEnrolments",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamEnrolments_CandidateId",
                table: "ExamEnrolments",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQualificationLevels_DefaultGradeSetId",
                table: "ExamQualificationLevels",
                column: "DefaultGradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamQualificationLevels_QualificationId",
                table: "ExamQualificationLevels",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResultEmbargoes_ResultSetId",
                table: "ExamResultEmbargoes",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRooms_RoomId",
                table: "ExamRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRoomSeatBlocks_ExamRoomId",
                table: "ExamRoomSeatBlocks",
                column: "ExamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeasons_ResultSetId",
                table: "ExamSeasons",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeatAllocations_CandidateId",
                table: "ExamSeatAllocations",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeatAllocations_SittingId",
                table: "ExamSeatAllocations",
                column: "SittingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeries_ExamBoardId",
                table: "ExamSeries",
                column: "ExamBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeries_ExamSeasonId",
                table: "ExamSeries",
                column: "ExamSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_AppealResultId",
                table: "Exclusions",
                column: "AppealResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_ExclusionReasonId",
                table: "Exclusions",
                column: "ExclusionReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_ExclusionTypeId",
                table: "Exclusions",
                column: "ExclusionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exclusions_StudentId",
                table: "Exclusions",
                column: "StudentId");

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
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmissions_DocumentId",
                table: "HomeworkSubmissions",
                column: "DocumentId");

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
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_StaffMemberId",
                table: "Houses",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_StudentGroupId",
                table: "Houses",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_AcademicYearId",
                table: "Incidents",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_BehaviourTypeId",
                table: "Incidents",
                column: "BehaviourTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CreatedById",
                table: "Incidents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_LocationId",
                table: "Incidents",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanHomeworkItems_HomeworkItemId",
                table: "LessonPlanHomeworkItems",
                column: "HomeworkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlanHomeworkItems_LessonPlanId",
                table: "LessonPlanHomeworkItems",
                column: "LessonPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlans_CreatedById",
                table: "LessonPlans",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlans_DirectoryId",
                table: "LessonPlans",
                column: "DirectoryId");

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
                name: "IX_MedicalEvents_CreatedById",
                table: "MedicalEvents",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvents_StudentId",
                table: "MedicalEvents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_NextOfKinPersonId",
                table: "NextOfKin",
                column: "NextOfKinPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_RelationshipTypeId",
                table: "NextOfKin",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_StaffMemberId",
                table: "NextOfKin",
                column: "StaffMemberId");

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
                name: "IX_ParentEveningAppointments_ParentEveningStaffId",
                table: "ParentEveningAppointments",
                column: "ParentEveningStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningAppointments_StudentId",
                table: "ParentEveningAppointments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningBreaks_ParentEveningStaffMemberId",
                table: "ParentEveningBreaks",
                column: "ParentEveningStaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningGroup_ParentEveningId",
                table: "ParentEveningGroup",
                column: "ParentEveningId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningGroup_StudentGroupId",
                table: "ParentEveningGroup",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEvenings_EventId",
                table: "ParentEvenings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningStaffMembers_ParentEveningId",
                table: "ParentEveningStaffMembers",
                column: "ParentEveningId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentEveningStaffMembers_StaffMemberId",
                table: "ParentEveningStaffMembers",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DirectoryId",
                table: "People",
                column: "DirectoryId");

            migrationBuilder.CreateIndex(
                name: "IX_People_EthnicityId",
                table: "People",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PhotoId",
                table: "People",
                column: "PhotoId");

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
                name: "IX_PhoneNumbers_AgencyId",
                table: "PhoneNumbers",
                column: "AgencyId");

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
                name: "IX_Products_VatRateId",
                table: "Products",
                column: "VatRateId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroups_StaffMemberId",
                table: "RegGroups",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroups_StudentGroupId",
                table: "RegGroups",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroups_YearGroupId",
                table: "RegGroups",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardEntries_AttendanceWeekId",
                table: "ReportCardEntries",
                column: "AttendanceWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardEntries_CreatedById",
                table: "ReportCardEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardEntries_PeriodId",
                table: "ReportCardEntries",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardEntries_ReportCardId",
                table: "ReportCardEntries",
                column: "ReportCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCards_BehaviourTypeId",
                table: "ReportCards",
                column: "BehaviourTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCards_StudentId",
                table: "ReportCards",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardTargetEntries_EntryId",
                table: "ReportCardTargetEntries",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardTargetEntries_TargetId",
                table: "ReportCardTargetEntries",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardTargets_ReportCardId",
                table: "ReportCardTargets",
                column: "ReportCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCardTargets_TargetId",
                table: "ReportCardTargets",
                column: "TargetId");

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
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClosures_ReasonId",
                table: "RoomClosures",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomClosures_RoomId",
                table: "RoomClosures",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingFloorId",
                table: "Rooms",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_LocationId",
                table: "Rooms",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AgencyId",
                table: "Schools",
                column: "AgencyId");

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
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDiscounts_DiscountId",
                table: "StoreDiscounts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDiscounts_ProductId",
                table: "StoreDiscounts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreDiscounts_ProductTypeId",
                table: "StoreDiscounts",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_AchievementId",
                table: "StudentAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_OutcomeId",
                table: "StudentAchievements",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAchievements_StudentId",
                table: "StudentAchievements",
                column: "StudentId");

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
                name: "IX_StudentChargeDiscounts_ChargeDiscountId",
                table: "StudentChargeDiscounts",
                column: "ChargeDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentChargeDiscounts_StudentId",
                table: "StudentChargeDiscounts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCharges_ChargeBillingPeriodId",
                table: "StudentCharges",
                column: "ChargeBillingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCharges_ChargeId",
                table: "StudentCharges",
                column: "ChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCharges_StudentId",
                table: "StudentCharges",
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
                name: "IX_StudentGroupMemberships_StudentGroupId",
                table: "StudentGroupMemberships",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupMemberships_StudentId",
                table: "StudentGroupMemberships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_PromoteToGroupId",
                table: "StudentGroups",
                column: "PromoteToGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupSupervisors_StudentGroupId",
                table: "StudentGroupSupervisors",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroupSupervisors_SupervisorId",
                table: "StudentGroupSupervisors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidentDetentions_DetentionId",
                table: "StudentIncidentDetentions",
                column: "DetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidentDetentions_StudentIncidentId",
                table: "StudentIncidentDetentions",
                column: "StudentIncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidents_IncidentId",
                table: "StudentIncidents",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidents_OutcomeId",
                table: "StudentIncidents",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidents_RoleTypeId",
                table: "StudentIncidents",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidents_StatusId",
                table: "StudentIncidents",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIncidents_StudentId",
                table: "StudentIncidents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BoarderStatusId",
                table: "Students",
                column: "BoarderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_EnrolmentStatusId",
                table: "Students",
                column: "EnrolmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_PersonId",
                table: "Students",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SenStatusId",
                table: "Students",
                column: "SenStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SenTypeId",
                table: "Students",
                column: "SenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTopics_CourseId",
                table: "StudyTopics",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCodes_SubjectCodeSetId",
                table: "SubjectCodes",
                column: "SubjectCodeSetId");

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
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroups_CurriculumYearGroupId",
                table: "YearGroups",
                column: "CurriculumYearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroups_StaffMemberId",
                table: "YearGroups",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroups_StudentGroupId",
                table: "YearGroups",
                column: "StudentGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "AddressLinks");

            migrationBuilder.DropTable(
                name: "AttendanceMarks");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "BillAccountTransactions");

            migrationBuilder.DropTable(
                name: "BillCharges");

            migrationBuilder.DropTable(
                name: "BillDiscounts");

            migrationBuilder.DropTable(
                name: "BillItems");

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
                name: "DiaryEventAttendees");

            migrationBuilder.DropTable(
                name: "DiaryEventTemplates");

            migrationBuilder.DropTable(
                name: "EmailAddresses");

            migrationBuilder.DropTable(
                name: "ExamAssessmentAspects");

            migrationBuilder.DropTable(
                name: "ExamAwardElements");

            migrationBuilder.DropTable(
                name: "ExamAwardSeries");

            migrationBuilder.DropTable(
                name: "ExamCandidateSeries");

            migrationBuilder.DropTable(
                name: "ExamCandidateSpecialArrangements");

            migrationBuilder.DropTable(
                name: "ExamElementComponents");

            migrationBuilder.DropTable(
                name: "ExamEnrolments");

            migrationBuilder.DropTable(
                name: "ExamResultEmbargoes");

            migrationBuilder.DropTable(
                name: "ExamRoomSeatBlocks");

            migrationBuilder.DropTable(
                name: "ExamSeatAllocations");

            migrationBuilder.DropTable(
                name: "Exclusions");

            migrationBuilder.DropTable(
                name: "GiftedTalentedStudents");

            migrationBuilder.DropTable(
                name: "HomeworkSubmissions");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LessonPlanHomeworkItems");

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
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "ParentEveningAppointments");

            migrationBuilder.DropTable(
                name: "ParentEveningBreaks");

            migrationBuilder.DropTable(
                name: "ParentEveningGroup");

            migrationBuilder.DropTable(
                name: "PersonConditions");

            migrationBuilder.DropTable(
                name: "PersonDietaryRequirements");

            migrationBuilder.DropTable(
                name: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "RegGroups");

            migrationBuilder.DropTable(
                name: "ReportCardTargetEntries");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RoomClosures");

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
                name: "StoreDiscounts");

            migrationBuilder.DropTable(
                name: "StudentAchievements");

            migrationBuilder.DropTable(
                name: "StudentAgentRelationships");

            migrationBuilder.DropTable(
                name: "StudentChargeDiscounts");

            migrationBuilder.DropTable(
                name: "StudentContactRelationships");

            migrationBuilder.DropTable(
                name: "StudentGroupMemberships");

            migrationBuilder.DropTable(
                name: "StudentGroupSupervisors");

            migrationBuilder.DropTable(
                name: "StudentIncidentDetentions");

            migrationBuilder.DropTable(
                name: "SubjectStaffMembers");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropTable(
                name: "TrainingCertificates");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AddressTypes");

            migrationBuilder.DropTable(
                name: "AttendanceCodes");

            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "StudentCharges");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "CommentBanks");

            migrationBuilder.DropTable(
                name: "CommunicationTypes");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "CurriculumBands");

            migrationBuilder.DropTable(
                name: "DiaryEventAttendeeResponses");

            migrationBuilder.DropTable(
                name: "EmailAddressTypes");

            migrationBuilder.DropTable(
                name: "ExamSpecialArrangements");

            migrationBuilder.DropTable(
                name: "ExamElements");

            migrationBuilder.DropTable(
                name: "ExamAwards");

            migrationBuilder.DropTable(
                name: "ExamCandidate");

            migrationBuilder.DropTable(
                name: "ExamComponentSittings");

            migrationBuilder.DropTable(
                name: "ExclusionAppealResults");

            migrationBuilder.DropTable(
                name: "ExclusionReasons");

            migrationBuilder.DropTable(
                name: "ExclusionTypes");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "HomeworkItems");

            migrationBuilder.DropTable(
                name: "LessonPlans");

            migrationBuilder.DropTable(
                name: "LogNoteTypes");

            migrationBuilder.DropTable(
                name: "MarksheetTemplates");

            migrationBuilder.DropTable(
                name: "NextOfKinRelationshipTypes");

            migrationBuilder.DropTable(
                name: "ObservationOutcomes");

            migrationBuilder.DropTable(
                name: "ParentEveningStaffMembers");

            migrationBuilder.DropTable(
                name: "MedicalConditions");

            migrationBuilder.DropTable(
                name: "DietaryRequirements");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes");

            migrationBuilder.DropTable(
                name: "YearGroups");

            migrationBuilder.DropTable(
                name: "ReportCardEntries");

            migrationBuilder.DropTable(
                name: "ReportCardTargets");

            migrationBuilder.DropTable(
                name: "Aspects");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "RoomClosureReasons");

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
                name: "Products");

            migrationBuilder.DropTable(
                name: "AchievementOutcomes");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "ChargeDiscounts");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "RelationshipTypes");

            migrationBuilder.DropTable(
                name: "Detentions");

            migrationBuilder.DropTable(
                name: "StudentIncidents");

            migrationBuilder.DropTable(
                name: "SubjectStaffMemberRoles");

            migrationBuilder.DropTable(
                name: "TrainingCertificateStatus");

            migrationBuilder.DropTable(
                name: "TrainingCourses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "AttendanceCodeTypes");

            migrationBuilder.DropTable(
                name: "ChargeBillingPeriods");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "ExamBaseElements");

            migrationBuilder.DropTable(
                name: "ExamComponents");

            migrationBuilder.DropTable(
                name: "ExamRooms");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "StudyTopics");

            migrationBuilder.DropTable(
                name: "ParentEvenings");

            migrationBuilder.DropTable(
                name: "CurriculumYearGroups");

            migrationBuilder.DropTable(
                name: "AttendancePeriods");

            migrationBuilder.DropTable(
                name: "AttendanceWeeks");

            migrationBuilder.DropTable(
                name: "BehaviourTargets");

            migrationBuilder.DropTable(
                name: "ReportCards");

            migrationBuilder.DropTable(
                name: "AspectTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "AchievementTypes");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "AgentTypes");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "DetentionTypes");

            migrationBuilder.DropTable(
                name: "StaffMembers");

            migrationBuilder.DropTable(
                name: "BehaviourOutcomes");

            migrationBuilder.DropTable(
                name: "BehaviourRoleTypes");

            migrationBuilder.DropTable(
                name: "BehaviourStatus");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "CurriculumGroups");

            migrationBuilder.DropTable(
                name: "ExamQualificationLevels");

            migrationBuilder.DropTable(
                name: "ExamBaseComponents");

            migrationBuilder.DropTable(
                name: "ExamDates");

            migrationBuilder.DropTable(
                name: "ExamSeries");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "DiaryEvents");

            migrationBuilder.DropTable(
                name: "AcademicTerms");

            migrationBuilder.DropTable(
                name: "AttendanceWeekPatterns");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "AgencyTypes");

            migrationBuilder.DropTable(
                name: "VatRates");

            migrationBuilder.DropTable(
                name: "IncidentTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CurriculumBlocks");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "ExamQualifications");

            migrationBuilder.DropTable(
                name: "GradeSets");

            migrationBuilder.DropTable(
                name: "ExamAssessmentModes");

            migrationBuilder.DropTable(
                name: "ExamAssessments");

            migrationBuilder.DropTable(
                name: "ExamSessions");

            migrationBuilder.DropTable(
                name: "ExamSeasons");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "DiaryEventTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "BoarderStatus");

            migrationBuilder.DropTable(
                name: "EnrolmentStatus");

            migrationBuilder.DropTable(
                name: "SenStatus");

            migrationBuilder.DropTable(
                name: "SenTypes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ExamBoards");

            migrationBuilder.DropTable(
                name: "ResultSets");

            migrationBuilder.DropTable(
                name: "SubjectCodes");

            migrationBuilder.DropTable(
                name: "BuildingFloors");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Directories");

            migrationBuilder.DropTable(
                name: "Ethnicities");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "SubjectCodeSets");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
