using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmployeeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DomainEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveApplicationCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainEvent_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainEvent_LeaveApplicationComments_LeaveApplicationCommentId",
                        column: x => x.LeaveApplicationCommentId,
                        principalTable: "LeaveApplicationComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DomainEvent_LeaveApplications_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalTable: "LeaveApplications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_CompanyId",
                table: "DomainEvent",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_EmployeeId",
                table: "DomainEvent",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_LeaveApplicationCommentId",
                table: "DomainEvent",
                column: "LeaveApplicationCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_LeaveApplicationId",
                table: "DomainEvent",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeId",
                table: "Employee",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvent");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
