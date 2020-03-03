using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortal.Database.Migrations
{
    public partial class ModelUpdateHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetPermissions_SystemResource_ResourceId",
                table: "AspNetPermissions");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "SubjectStaffMemberRole",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "HomeworkSubmission",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "SubmitOnline",
                table: "Homework",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DocumentType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DiaryEventTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DetentionType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AchievementType",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkSubmission_StudentId",
                table: "HomeworkSubmission",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReview_ReviewTypeId",
                table: "SenReview",
                column: "ReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SenReview_StudentId",
                table: "SenReview",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetPermissions_SystemResource_ResourceId",
                table: "AspNetPermissions",
                column: "ResourceId",
                principalTable: "SystemResource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeworkSubmission_Student_StudentId",
                table: "HomeworkSubmission",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetPermissions_SystemResource_ResourceId",
                table: "AspNetPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeworkSubmission_Student_StudentId",
                table: "HomeworkSubmission");

            migrationBuilder.DropTable(
                name: "SenReview");

            migrationBuilder.DropIndex(
                name: "IX_HomeworkSubmission_StudentId",
                table: "HomeworkSubmission");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "SubjectStaffMemberRole");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "HomeworkSubmission");

            migrationBuilder.DropColumn(
                name: "SubmitOnline",
                table: "Homework");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DocumentType");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DiaryEventTemplate");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "DetentionType");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AchievementType");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetPermissions_SystemResource_ResourceId",
                table: "AspNetPermissions",
                column: "ResourceId",
                principalTable: "SystemResource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
