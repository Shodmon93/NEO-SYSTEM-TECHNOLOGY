using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEO_SYSTEM_TECHNOLOGY.Migrations
{
    public partial class AddedContractClassToDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Organizations_OrganizationID",
                table: "Contract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_OrganizationID",
                table: "Contracts",
                newName: "IX_Contracts_OrganizationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Organizations_OrganizationID",
                table: "Contracts",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Organizations_OrganizationID",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_OrganizationID",
                table: "Contract",
                newName: "IX_Contract_OrganizationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Organizations_OrganizationID",
                table: "Contract",
                column: "OrganizationID",
                principalTable: "Organizations",
                principalColumn: "ID");
        }
    }
}
