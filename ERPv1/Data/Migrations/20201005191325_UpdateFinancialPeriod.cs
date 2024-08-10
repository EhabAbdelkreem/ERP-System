using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class UpdateFinancialPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Finance_GL_FiniacialPeriod",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Finance_GL_FiniacialPeriod",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Finance_GL_FiniacialPeriod",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Finance_GL_FiniacialPeriod",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Finance_GL_FiniacialPeriod");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Finance_GL_FiniacialPeriod");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Finance_GL_FiniacialPeriod");
        }
    }
}
