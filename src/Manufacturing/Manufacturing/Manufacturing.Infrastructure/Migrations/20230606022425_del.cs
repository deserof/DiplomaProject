using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manufacturing.Infrastructure.Migrations
{
    public partial class del : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessExecutions_ProductionLines_LineId",
                table: "ProcessExecutions");

            migrationBuilder.DropTable(
                name: "ProductionLines");

            migrationBuilder.DropIndex(
                name: "IX_ProcessExecutions_LineId",
                table: "ProcessExecutions");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "ProcessExecutions");

            migrationBuilder.AddColumn<int>(
                name: "ProcessExecutionId",
                table: "ProductFile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductFile_ProcessExecutionId",
                table: "ProductFile",
                column: "ProcessExecutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFile_ProcessExecutions_ProcessExecutionId",
                table: "ProductFile",
                column: "ProcessExecutionId",
                principalTable: "ProcessExecutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFile_ProcessExecutions_ProcessExecutionId",
                table: "ProductFile");

            migrationBuilder.DropIndex(
                name: "IX_ProductFile_ProcessExecutionId",
                table: "ProductFile");

            migrationBuilder.DropColumn(
                name: "ProcessExecutionId",
                table: "ProductFile");

            migrationBuilder.AddColumn<int>(
                name: "LineId",
                table: "ProcessExecutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductionLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionLines", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessExecutions_LineId",
                table: "ProcessExecutions",
                column: "LineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessExecutions_ProductionLines_LineId",
                table: "ProcessExecutions",
                column: "LineId",
                principalTable: "ProductionLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
