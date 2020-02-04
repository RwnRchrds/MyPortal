using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortalCore.Data.Migrations
{
    public partial class PersistPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
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
                name: "AspNetPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetPermissions_ResourceId",
                table: "AspNetPermissions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemResource_AreaId",
                table: "SystemResource",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetPermissions");

            migrationBuilder.DropTable(
                name: "SystemResource");
        }
    }
}
