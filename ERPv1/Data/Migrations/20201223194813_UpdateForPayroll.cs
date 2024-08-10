using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class UpdateForPayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransId",
                table: "HR_SalaryBatch",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChartCounter",
                columns: new[] { "Id", "AccountCategory", "AccountType", "BalanceSheet", "Count", "IncomeStatement", "ParentAccNum" },
                values: new object[] { 24, 40, "OtherCurrentLiabilties", true, 0, false, "2260000000" });

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChart",
                columns: new[] { "AccNum", "AccTypeId", "AccountName", "AccountNameAr", "AccountNature", "Balance", "BranchId", "CurrencyId", "IsActive", "IsParent", "ParentAcNum", "StartingBalance" },
                values: new object[] { "2260000000", 24, "Other Current Liabilities", "التزمات متداولة أخري", 1, 0m, 1, 1, true, true, "", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2260000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "HR_SalaryBatch");
        }
    }
}
