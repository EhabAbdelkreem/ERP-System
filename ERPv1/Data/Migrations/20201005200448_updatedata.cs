using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class updatedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1230000001",
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000001",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2210000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2220000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2230000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2240000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000000",
                columns: new[] { "AccountNameAr", "AccountNature" },
                values: new object[] { "ايرادات مقدمة", 1 });

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000001",
                columns: new[] { "AccountNameAr", "AccountNature" },
                values: new object[] { "ايرادات مقدمة", 1 });

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "3110000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5110000000",
                column: "AccountNature",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5120000000",
                column: "AccountNature",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1230000001",
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000001",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2210000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2220000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2230000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2240000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000000",
                columns: new[] { "AccountNameAr", "AccountNature" },
                values: new object[] { "ايرادات مستحقة", 0 });

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000001",
                columns: new[] { "AccountNameAr", "AccountNature" },
                values: new object[] { "ايرادات مستحقة", 0 });

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "3110000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5110000000",
                column: "AccountNature",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5120000000",
                column: "AccountNature",
                value: 0);
        }
    }
}
