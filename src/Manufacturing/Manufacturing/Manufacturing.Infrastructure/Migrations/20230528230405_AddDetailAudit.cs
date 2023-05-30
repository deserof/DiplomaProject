using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manufacturing.Infrastructure.Migrations
{
    public partial class AddDetailAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Details",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Details",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Details",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Details",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Details");
        }
    }
}
