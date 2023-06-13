using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manufacturing.Infrastructure.Migrations
{
    public partial class updateproc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessExecutions_Employees_EmployeeId",
                table: "ProcessExecutions");

            migrationBuilder.DropIndex(
                name: "IX_ProcessExecutions_EmployeeId",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ProcessExecutions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ProcessExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExecutions_EmployeeId",
                table: "ProcessExecutions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessExecutions_Employees_EmployeeId",
                table: "ProcessExecutions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
