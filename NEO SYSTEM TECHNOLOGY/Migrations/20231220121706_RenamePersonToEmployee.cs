using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEO_SYSTEM_TECHNOLOGY.Migrations
{
    public partial class RenamePersonToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Organizations_OrganizationID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_People_OrganizationID",
                table: "Employees",
                newName: "IX_Employees_OrganizationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Organizations_OrganizationID",
                table: "Employees",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Organizations_OrganizationID",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employees");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_OrganizationID",
                table: "Employees",
                newName: "IX_People_OrganizationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Organizations_OrganizationID",
                table: "Employees",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
