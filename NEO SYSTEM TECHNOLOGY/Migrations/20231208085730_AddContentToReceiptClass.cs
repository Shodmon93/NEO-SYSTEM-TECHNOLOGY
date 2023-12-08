using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEO_SYSTEM_TECHNOLOGY.Migrations
{
    public partial class AddContentToReceiptClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Receipts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Receipts");
        }
    }
}
