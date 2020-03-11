using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortal.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    FirstDate = table.Column<DateTime>(type: "date", nullable: false),
                    LastDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AchievementType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    DefaultPoints = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
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
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspectType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspectType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
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
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCodeMeaning",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodeMeaning", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendancePeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Weekday = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    IsAm = table.Column<bool>(nullable: false),
                    IsPm = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendancePeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentBank",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    System = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentBank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumBand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumBand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumYearGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(nullable: true),
                    KeyStage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumYearGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetentionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(2)", nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetentionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventAttendeeResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventAttendeeResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaryRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRequirement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    Staff = table.Column<bool>(nullable: false),
                    Student = table.Column<bool>(nullable: false),
                    Contact = table.Column<bool>(nullable: false),
                    General = table.Column<bool>(nullable: false),
                    Sen = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddressType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GovernanceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernanceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SubmitOnline = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    System = table.Column<bool>(nullable: false),
                    DefaultPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntakeType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlanTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    PlanTemplate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlanTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalAuthority",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LeaCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalAuthority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObservationOutcome",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationOutcome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    IsMeal = table.Column<bool>(nullable: false),
                    System = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileLogNoteType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLogNoteType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResultSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenEventType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenEventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenProvisionType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenProvisionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenReviewType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenReviewType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStaffMemberRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMemberRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificateStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificateStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCourse",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCourse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceWeek",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Beginning = table.Column<DateTime>(type: "date", nullable: false),
                    IsHoliday = table.Column<bool>(nullable: false),
                    IsNonTimetable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceWeek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceWeek_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
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
                name: "Bulletin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AuthorId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Detail = table.Column<string>(nullable: false),
                    ShowStudents = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulletin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bulletin_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 256, nullable: true),
                    PhotoId = table.Column<int>(nullable: true),
                    NhsNumber = table.Column<string>(maxLength: 256, nullable: true),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    Gender = table.Column<string>(unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Dob = table.Column<DateTime>(type: "date", nullable: true),
                    Deceased = table.Column<DateTime>(type: "date", nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceCode",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    Description = table.Column<string>(maxLength: 128, nullable: false),
                    MeaningId = table.Column<Guid>(nullable: false),
                    DoNotUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceCode_AttendanceCodeMeaning_MeaningId",
                        column: x => x.MeaningId,
                        principalTable: "AttendanceCodeMeaning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CommentBankId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_CommentBank_CommentBankId",
                        column: x => x.CommentBankId,
                        principalTable: "CommentBank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    CommunicationTypeId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationLog_CommunicationType_CommunicationTypeId",
                        column: x => x.CommunicationTypeId,
                        principalTable: "CommunicationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventTypeId = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_DiaryEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEvent_DiaryEventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "DiaryEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EventTypeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    Hours = table.Column<int>(nullable: false),
                    Days = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryEventTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEventTemplate_DiaryEventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "DiaryEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    DownloadUrl = table.Column<string>(nullable: false),
                    UploaderId = table.Column<Guid>(nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "date", nullable: false),
                    NonPublic = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_DocumentType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Document_AspNetUsers_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aspect",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    GradeSetId = table.Column<Guid>(nullable: true),
                    MaxMark = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aspect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aspect_GradeSet_GradeSetId",
                        column: x => x.GradeSetId,
                        principalTable: "GradeSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Aspect_AspectType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AspectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
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
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_GradeSet_GradeSetId",
                        column: x => x.GradeSetId,
                        principalTable: "GradeSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Product",
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
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
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
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_SystemArea_AreaId",
                        column: x => x.AreaId,
                        principalTable: "SystemArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AreaId = table.Column<Guid>(nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    HasPermissions = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemResource_SystemArea_AreaId",
                        column: x => x.AreaId,
                        principalTable: "SystemArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AddressId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressPerson_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressPerson_Person_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
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
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(maxLength: 128, nullable: false),
                    Main = table.Column<bool>(nullable: false),
                    Primary = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddress_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailAddress_EmailAddressType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmailAddressType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonCondition",
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
                    table.PrimaryKey("PK_PersonCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCondition_MedicalCondition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "MedicalCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCondition_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonDietaryRequirement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    DietaryRequirementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDietaryRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonDietaryRequirement_DietaryRequirement_DietaryRequirementId",
                        column: x => x.DietaryRequirementId,
                        principalTable: "DietaryRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonDietaryRequirement_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Number = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_PhoneNumberType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PhoneNumberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "School",
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
                    table.PrimaryKey("PK_School", x => x.Id);
                    table.ForeignKey(
                        name: "FK_School_GovernanceType_GovernanceTypeId",
                        column: x => x.GovernanceTypeId,
                        principalTable: "GovernanceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_Person_HeadTeacherId",
                        column: x => x.HeadTeacherId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_IntakeType_IntakeTypeId",
                        column: x => x.IntakeTypeId,
                        principalTable: "IntakeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_LocalAuthority_LocalAuthorityId",
                        column: x => x.LocalAuthorityId,
                        principalTable: "LocalAuthority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_Phase_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_School_SchoolType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SchoolType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 128, nullable: false),
                    NiNumber = table.Column<string>(maxLength: 128, nullable: true),
                    PostNominal = table.Column<string>(maxLength: 128, nullable: true),
                    TeachingStaff = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffMember_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AssignedToId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Personal = table.Column<bool>(nullable: false),
                    Completed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Person_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DiaryEventAttendee",
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
                    table.PrimaryKey("PK_DiaryEventAttendee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendee_DiaryEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "DiaryEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendee_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryEventAttendee_DiaryEventAttendeeResponse_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "DiaryEventAttendeeResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    HomeworkId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkAttachment_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkAttachment_Homework_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    DocumentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAttachment_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAttachment_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResourceId = table.Column<Guid>(nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 128, nullable: false),
                    FullDescription = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetPermissions_SystemResource_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "SystemResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Detention",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DetentionTypeId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false),
                    SupervisorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detention_DetentionType_DetentionTypeId",
                        column: x => x.DetentionTypeId,
                        principalTable: "DetentionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detention_DiaryEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "DiaryEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Detention_StaffMember_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    HeadId = table.Column<Guid>(nullable: true),
                    ColourCode = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                    table.ForeignKey(
                        name: "FK_House_StaffMember_HeadId",
                        column: x => x.HeadId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Observation",
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
                    table.PrimaryKey("PK_Observation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Observation_StaffMember_ObserveeId",
                        column: x => x.ObserveeId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_StaffMember_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Observation_ObservationOutcome_OutcomeId",
                        column: x => x.OutcomeId,
                        principalTable: "ObservationOutcome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectStaffMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(nullable: false),
                    StaffMemberId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectStaffMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMember_SubjectStaffMemberRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SubjectStaffMemberRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMember_StaffMember_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectStaffMember_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CourseId = table.Column<Guid>(nullable: false),
                    StaffId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingCertificate_TrainingCourse_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TrainingCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingCertificate_StaffMember_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainingCertificate_TrainingCertificateStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TrainingCertificateStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    HeadId = table.Column<Guid>(nullable: true),
                    CurriculumYearGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearGroup_CurriculumYearGroup_CurriculumYearGroupId",
                        column: x => x.CurriculumYearGroupId,
                        principalTable: "CurriculumYearGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearGroup_StaffMember_HeadId",
                        column: x => x.HeadId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: true),
                    BandId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_CurriculumBand_BandId",
                        column: x => x.BandId,
                        principalTable: "CurriculumBand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_StaffMember_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_YearGroup_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TutorId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegGroup_StaffMember_TutorId",
                        column: x => x.TutorId,
                        principalTable: "StaffMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegGroup_YearGroup_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyTopic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SubjectId = table.Column<Guid>(nullable: false),
                    YearGroupId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTopic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyTopic_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyTopic_YearGroup_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ClassId = table.Column<Guid>(nullable: false),
                    PeriodId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Session_AttendancePeriod_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Student",
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
                    GiftedAndTalented = table.Column<bool>(nullable: false),
                    SenStatusId = table.Column<Guid>(nullable: true),
                    PupilPremium = table.Column<bool>(nullable: false),
                    Upn = table.Column<string>(unicode: false, maxLength: 13, nullable: true),
                    Uci = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_RegGroup_RegGroupId",
                        column: x => x.RegGroupId,
                        principalTable: "RegGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_SenStatus_SenStatusId",
                        column: x => x.SenStatusId,
                        principalTable: "SenStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_YearGroup_YearGroupId",
                        column: x => x.YearGroupId,
                        principalTable: "YearGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudyTopicId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    LearningObjectives = table.Column<string>(nullable: false),
                    PlanContent = table.Column<string>(nullable: false),
                    Homework = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonPlan_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonPlan_StudyTopic_StudyTopicId",
                        column: x => x.StudyTopicId,
                        principalTable: "StudyTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achievement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    AchievementTypeId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    RecordedById = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievement_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievement_AchievementType_AchievementTypeId",
                        column: x => x.AchievementTypeId,
                        principalTable: "AchievementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievement_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievement_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achievement_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceMark",
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
                    table.PrimaryKey("PK_AttendanceMark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceMark_AttendancePeriod_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "AttendancePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMark_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceMark_AttendanceWeek_WeekId",
                        column: x => x.WeekId,
                        principalTable: "AttendanceWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketItem_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrolment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    BandId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolment_CurriculumBand_BandId",
                        column: x => x.BandId,
                        principalTable: "CurriculumBand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolment_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftedTalented",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    StudentId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    Notes = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftedTalented", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftedTalented_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftedTalented_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeworkSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    HomeworkId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    MaxPoints = table.Column<int>(nullable: false),
                    PointsAchieved = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmission_Homework_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homework",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmission_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeworkSubmission_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    BehaviourTypeId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    RecordedById = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Resolved = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incident_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_IncidentType_BehaviourTypeId",
                        column: x => x.BehaviourTypeId,
                        principalTable: "IncidentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incident_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalEvent",
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
                    table.PrimaryKey("PK_MedicalEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalEvent_AspNetUsers_RecordedById",
                        column: x => x.RecordedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalEvent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileLogNote",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    TypeId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    AcademicYearId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLogNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileLogNote_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileLogNote_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileLogNote_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileLogNote_ProfileLogNoteType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ProfileLogNoteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ResultSetId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    AspectId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    GradeId = table.Column<Guid>(nullable: false),
                    Mark = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Aspect_AspectId",
                        column: x => x.AspectId,
                        principalTable: "Aspect",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_ResultSet_ResultSetId",
                        column: x => x.ResultSetId,
                        principalTable: "ResultSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Result_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
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
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sale_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenEvent",
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
                    table.PrimaryKey("PK_SenEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenEvent_SenEventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "SenEventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenEvent_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenProvision",
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
                    table.PrimaryKey("PK_SenProvision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenProvision_SenProvisionType_ProvisionTypeId",
                        column: x => x.ProvisionTypeId,
                        principalTable: "SenProvisionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenProvision_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SenReview",
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
                    table.PrimaryKey("PK_SenReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SenReview_SenReviewType_ReviewTypeId",
                        column: x => x.ReviewTypeId,
                        principalTable: "SenReviewType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SenReview_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentContact",
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
                    table.PrimaryKey("PK_StudentContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentContact_RelationshipType_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "RelationshipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentContact_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncidentDetention",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    IncidentId = table.Column<Guid>(nullable: false),
                    DetentionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentDetention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentDetention_Detention_DetentionId",
                        column: x => x.DetentionId,
                        principalTable: "Detention",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncidentDetention_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_AcademicYearId",
                table: "Achievement",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_AchievementTypeId",
                table: "Achievement",
                column: "AchievementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_LocationId",
                table: "Achievement",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_RecordedById",
                table: "Achievement",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_Achievement_StudentId",
                table: "Achievement",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressPerson_AddressId",
                table: "AddressPerson",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspect_GradeSetId",
                table: "Aspect",
                column: "GradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Aspect_TypeId",
                table: "Aspect",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetPermissions_ResourceId",
                table: "AspNetPermissions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
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
                name: "IX_AttendanceCode_MeaningId",
                table: "AttendanceCode",
                column: "MeaningId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMark_PeriodId",
                table: "AttendanceMark",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMark_StudentId",
                table: "AttendanceMark",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceMark_WeekId",
                table: "AttendanceMark",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceWeek_AcademicYearId",
                table: "AttendanceWeek",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_ProductId",
                table: "BasketItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_StudentId",
                table: "BasketItem",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bulletin_AuthorId",
                table: "Bulletin",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_AcademicYearId",
                table: "Class",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_BandId",
                table: "Class",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SubjectId",
                table: "Class",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_TeacherId",
                table: "Class",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_YearGroupId",
                table: "Class",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentBankId",
                table: "Comment",
                column: "CommentBankId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationLog_CommunicationTypeId",
                table: "CommunicationLog",
                column: "CommunicationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_PersonId",
                table: "Contact",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detention_DetentionTypeId",
                table: "Detention",
                column: "DetentionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Detention_EventId",
                table: "Detention",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Detention_SupervisorId",
                table: "Detention",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEvent_EventTypeId",
                table: "DiaryEvent",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendee_EventId",
                table: "DiaryEventAttendee",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendee_PersonId",
                table: "DiaryEventAttendee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventAttendee_ResponseId",
                table: "DiaryEventAttendee",
                column: "ResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryEventTemplate_EventTypeId",
                table: "DiaryEventTemplate",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_TypeId",
                table: "Document",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UploaderId",
                table: "Document",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_PersonId",
                table: "EmailAddress",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddress_TypeId",
                table: "EmailAddress",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_BandId",
                table: "Enrolment",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolment_StudentId",
                table: "Enrolment",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftedTalented_StudentId",
                table: "GiftedTalented",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftedTalented_SubjectId",
                table: "GiftedTalented",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GradeSetId",
                table: "Grade",
                column: "GradeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkAttachment_DocumentId",
                table: "HomeworkAttachment",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkAttachment_HomeworkId",
                table: "HomeworkAttachment",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_HomeworkId",
                table: "HomeworkSubmission",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_StudentId",
                table: "HomeworkSubmission",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_TaskId",
                table: "HomeworkSubmission",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_House_HeadId",
                table: "House",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_AcademicYearId",
                table: "Incident",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_BehaviourTypeId",
                table: "Incident",
                column: "BehaviourTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_LocationId",
                table: "Incident",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_RecordedById",
                table: "Incident",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_StudentId",
                table: "Incident",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetention_DetentionId",
                table: "IncidentDetention",
                column: "DetentionId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentDetention_IncidentId",
                table: "IncidentDetention",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlan_AuthorId",
                table: "LessonPlan",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPlan_StudyTopicId",
                table: "LessonPlan",
                column: "StudyTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvent_RecordedById",
                table: "MedicalEvent",
                column: "RecordedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalEvent_StudentId",
                table: "MedicalEvent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_ObserveeId",
                table: "Observation",
                column: "ObserveeId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_ObserverId",
                table: "Observation",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_Observation_OutcomeId",
                table: "Observation",
                column: "OutcomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAttachment_DocumentId",
                table: "PersonAttachment",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonAttachment_PersonId",
                table: "PersonAttachment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCondition_ConditionId",
                table: "PersonCondition",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCondition_PersonId",
                table: "PersonCondition",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDietaryRequirement_DietaryRequirementId",
                table: "PersonDietaryRequirement",
                column: "DietaryRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonDietaryRequirement_PersonId",
                table: "PersonDietaryRequirement",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                table: "PhoneNumber",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_TypeId",
                table: "PhoneNumber",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLogNote_AcademicYearId",
                table: "ProfileLogNote",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLogNote_AuthorId",
                table: "ProfileLogNote",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLogNote_StudentId",
                table: "ProfileLogNote",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLogNote_TypeId",
                table: "ProfileLogNote",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroup_TutorId",
                table: "RegGroup",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_RegGroup_YearGroupId",
                table: "RegGroup",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_AreaId",
                table: "Report",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_AspectId",
                table: "Result",
                column: "AspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_GradeId",
                table: "Result",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_ResultSetId",
                table: "Result",
                column: "ResultSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_StudentId",
                table: "Result",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_AcademicYearId",
                table: "Sale",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ProductId",
                table: "Sale",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_StudentId",
                table: "Sale",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_School_GovernanceTypeId",
                table: "School",
                column: "GovernanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_School_HeadTeacherId",
                table: "School",
                column: "HeadTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_School_IntakeTypeId",
                table: "School",
                column: "IntakeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_School_LocalAuthorityId",
                table: "School",
                column: "LocalAuthorityId");

            migrationBuilder.CreateIndex(
                name: "IX_School_PhaseId",
                table: "School",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_School_TypeId",
                table: "School",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenEvent_EventTypeId",
                table: "SenEvent",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenEvent_StudentId",
                table: "SenEvent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SenProvision_ProvisionTypeId",
                table: "SenProvision",
                column: "ProvisionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenProvision_StudentId",
                table: "SenProvision",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReview_ReviewTypeId",
                table: "SenReview",
                column: "ReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReview_StudentId",
                table: "SenReview",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_ClassId",
                table: "Session",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_PeriodId",
                table: "Session",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMember_PersonId",
                table: "StaffMember",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_HouseId",
                table: "Student",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_PersonId",
                table: "Student",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_RegGroupId",
                table: "Student",
                column: "RegGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SenStatusId",
                table: "Student",
                column: "SenStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_YearGroupId",
                table: "Student",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContact_ContactId",
                table: "StudentContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContact_RelationshipTypeId",
                table: "StudentContact",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentContact_StudentId",
                table: "StudentContact",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTopic_SubjectId",
                table: "StudyTopic",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTopic_YearGroupId",
                table: "StudyTopic",
                column: "YearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMember_RoleId",
                table: "SubjectStaffMember",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMember_StaffMemberId",
                table: "SubjectStaffMember",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectStaffMember_SubjectId",
                table: "SubjectStaffMember",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemResource_AreaId",
                table: "SystemResource",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssignedToId",
                table: "Task",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificate_CourseId",
                table: "TrainingCertificate",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificate_StaffId",
                table: "TrainingCertificate",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingCertificate_StatusId",
                table: "TrainingCertificate",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroup_CurriculumYearGroupId",
                table: "YearGroup",
                column: "CurriculumYearGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_YearGroup_HeadId",
                table: "YearGroup",
                column: "HeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievement");

            migrationBuilder.DropTable(
                name: "AddressPerson");

            migrationBuilder.DropTable(
                name: "AspNetPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttendanceCode");

            migrationBuilder.DropTable(
                name: "AttendanceMark");

            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "Bulletin");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommunicationLog");

            migrationBuilder.DropTable(
                name: "DiaryEventAttendee");

            migrationBuilder.DropTable(
                name: "DiaryEventTemplate");

            migrationBuilder.DropTable(
                name: "EmailAddress");

            migrationBuilder.DropTable(
                name: "Enrolment");

            migrationBuilder.DropTable(
                name: "GiftedTalented");

            migrationBuilder.DropTable(
                name: "HomeworkAttachment");

            migrationBuilder.DropTable(
                name: "HomeworkSubmission");

            migrationBuilder.DropTable(
                name: "IncidentDetention");

            migrationBuilder.DropTable(
                name: "LessonPlan");

            migrationBuilder.DropTable(
                name: "LessonPlanTemplate");

            migrationBuilder.DropTable(
                name: "MedicalEvent");

            migrationBuilder.DropTable(
                name: "Observation");

            migrationBuilder.DropTable(
                name: "PersonAttachment");

            migrationBuilder.DropTable(
                name: "PersonCondition");

            migrationBuilder.DropTable(
                name: "PersonDietaryRequirement");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "ProfileLogNote");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "SenEvent");

            migrationBuilder.DropTable(
                name: "SenProvision");

            migrationBuilder.DropTable(
                name: "SenReview");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "StudentContact");

            migrationBuilder.DropTable(
                name: "SubjectStaffMember");

            migrationBuilder.DropTable(
                name: "TrainingCertificate");

            migrationBuilder.DropTable(
                name: "AchievementType");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "SystemResource");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AttendanceCodeMeaning");

            migrationBuilder.DropTable(
                name: "AttendanceWeek");

            migrationBuilder.DropTable(
                name: "CommentBank");

            migrationBuilder.DropTable(
                name: "CommunicationType");

            migrationBuilder.DropTable(
                name: "DiaryEventAttendeeResponse");

            migrationBuilder.DropTable(
                name: "EmailAddressType");

            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Detention");

            migrationBuilder.DropTable(
                name: "Incident");

            migrationBuilder.DropTable(
                name: "StudyTopic");

            migrationBuilder.DropTable(
                name: "ObservationOutcome");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "MedicalCondition");

            migrationBuilder.DropTable(
                name: "DietaryRequirement");

            migrationBuilder.DropTable(
                name: "PhoneNumberType");

            migrationBuilder.DropTable(
                name: "ProfileLogNoteType");

            migrationBuilder.DropTable(
                name: "Aspect");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "ResultSet");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "GovernanceType");

            migrationBuilder.DropTable(
                name: "IntakeType");

            migrationBuilder.DropTable(
                name: "LocalAuthority");

            migrationBuilder.DropTable(
                name: "Phase");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "SenEventType");

            migrationBuilder.DropTable(
                name: "SenProvisionType");

            migrationBuilder.DropTable(
                name: "SenReviewType");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "AttendancePeriod");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "RelationshipType");

            migrationBuilder.DropTable(
                name: "SubjectStaffMemberRole");

            migrationBuilder.DropTable(
                name: "TrainingCourse");

            migrationBuilder.DropTable(
                name: "TrainingCertificateStatus");

            migrationBuilder.DropTable(
                name: "SystemArea");

            migrationBuilder.DropTable(
                name: "DetentionType");

            migrationBuilder.DropTable(
                name: "DiaryEvent");

            migrationBuilder.DropTable(
                name: "IncidentType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "AspectType");

            migrationBuilder.DropTable(
                name: "GradeSet");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "CurriculumBand");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "DiaryEventType");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "RegGroup");

            migrationBuilder.DropTable(
                name: "SenStatus");

            migrationBuilder.DropTable(
                name: "YearGroup");

            migrationBuilder.DropTable(
                name: "CurriculumYearGroup");

            migrationBuilder.DropTable(
                name: "StaffMember");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
