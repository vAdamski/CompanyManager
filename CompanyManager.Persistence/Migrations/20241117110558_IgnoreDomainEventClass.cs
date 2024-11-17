using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IgnoreDomainEventClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_DomainEvent_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
        }
    }
}
