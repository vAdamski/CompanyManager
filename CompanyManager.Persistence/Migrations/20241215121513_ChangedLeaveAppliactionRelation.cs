using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLeaveAppliactionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_Companies_CompanyId",
                table: "LeaveApplications");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "LeaveApplications",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveApplications_CompanyId",
                table: "LeaveApplications",
                newName: "IX_LeaveApplications_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_Employees_EmployeeId",
                table: "LeaveApplications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_Employees_EmployeeId",
                table: "LeaveApplications");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "LeaveApplications",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveApplications_EmployeeId",
                table: "LeaveApplications",
                newName: "IX_LeaveApplications_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_Companies_CompanyId",
                table: "LeaveApplications",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
