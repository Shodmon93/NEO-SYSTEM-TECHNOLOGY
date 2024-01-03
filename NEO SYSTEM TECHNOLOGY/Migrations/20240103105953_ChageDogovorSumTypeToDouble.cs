using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NEO_SYSTEM_TECHNOLOGY.Migrations
{
    public partial class ChageDogovorSumTypeToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DogovorSum",
                table: "Dogovors",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DogovorSum",
                table: "Dogovors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
