using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manufacturing.Infrastructure.Migrations
{
    public partial class addaudites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "QualityControls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "QualityControls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QualityControls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "QualityControls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "QualityControls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductionProcesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductionProcesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductionProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductionProcesses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductionProcesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductionOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductionOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductionOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductionOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductionOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProductionLines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductionLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductionLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductionLines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductionLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProcessExecutions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProcessExecutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProcessExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProcessExecutions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProcessExecutions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "QualityControls");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "QualityControls");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QualityControls");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "QualityControls");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "QualityControls");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductionProcesses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductionLines");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
