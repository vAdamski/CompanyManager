using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeEntityAddedSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_Employee_EmployeeId",
                table: "DomainEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employee");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_CompanyId",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EmployeeSupervisors",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSupervisors", x => new { x.EmployeeId, x.SupervisorId });
                    table.ForeignKey(
                        name: "FK_EmployeeSupervisors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeSupervisors_Employees_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSupervisors_SupervisorId",
                table: "EmployeeSupervisors",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_Employees_EmployeeId",
                table: "DomainEvent",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainEvent_Employees_EmployeeId",
                table: "DomainEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeSupervisors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                table: "Employee",
                newName: "IX_Employee_CompanyId");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainEvent_Employee_EmployeeId",
                table: "DomainEvent",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Companies_CompanyId",
                table: "Employee",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");
        }
    }
}
