using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class AccountChartSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChartCounter",
                columns: new[] { "Id", "AccountCategory", "AccountType", "BalanceSheet", "Count", "IncomeStatement", "ParentAccNum" },
                values: new object[,]
                {
                    { 10, 20, "StaffAdvances", true, 0, false, "1262000000" },
                    { 21, 80, "Purchases", false, 0, true, "4112000000" },
                    { 20, 70, "Expense", false, 0, true, "4111000000" },
                    { 19, 60, "Income", false, 0, true, "3110000000" },
                    { 18, 40, "Advances Income", true, 1, false, "2250000000" },
                    { 17, 40, "Accrued Expenses", true, 0, false, "2240000000" },
                    { 16, 40, "Creditors", true, 0, false, "2230000000" },
                    { 15, 40, "Taxes", true, 0, false, "2220000000" },
                    { 14, 40, "Suppliers", true, 0, false, "2210000000" },
                    { 13, 40, "NotePayable", true, 1, false, "2170000000" },
                    { 12, 20, "OtherCurrentAsset", true, 0, false, "1269000000" },
                    { 11, 20, "SupplierAdvances", false, 0, false, "1263000000" },
                    { 23, 50, "OwnerWithdraw", true, 0, false, "5120000000" },
                    { 9, 20, "Custody", true, 0, false, "1261000000" },
                    { 8, 20, "Store", true, 0, false, "1250000000" },
                    { 7, 20, "Check", true, 3, false, "1240000000" },
                    { 6, 20, "Client", true, 0, false, "1230000000" },
                    { 5, 20, "Bank", true, 0, false, "1220000000" },
                    { 4, 20, "Safe", true, 1, false, "1210000000" },
                    { 3, 10, "Furnitiures", true, 0, false, "1130000000" },
                    { 2, 10, "Machines And Equipments", true, 0, false, "1120000000" },
                    { 1, 10, "Buildings", true, 0, false, "1110000000" },
                    { 22, 50, "Owners", true, 0, false, "5110000000" }
                });

            migrationBuilder.InsertData(
                table: "Finance_Settings_Branch",
                columns: new[] { "Id", "BranchName" },
                values: new object[] { 1, "Main" });

            migrationBuilder.InsertData(
                table: "Finance_Settings_Currency",
                columns: new[] { "Id", "CurrencyAbbrev", "CurrencyName", "CurrencyNameAr", "IsDefault", "Rate" },
                values: new object[,]
                {
                    { 2, "USD", "American Dollar", "دولار أمريكي", false, 16.00m },
                    { 1, "EGP", "Egyptain Pound", "جنية مصر", true, 1m }
                });

            migrationBuilder.InsertData(
                table: "Finance_GL_AccountChart",
                columns: new[] { "AccNum", "AccTypeId", "AccountName", "AccountNameAr", "AccountNature", "Balance", "BranchId", "CurrencyId", "IsActive", "IsParent", "ParentAcNum", "StartingBalance" },
                values: new object[,]
                {
                    { "1110000000", 1, "Buildings", "مباني", 0, 0m, 1, 1, true, true, "", 0m },
                    { "4112000000", 21, "Purchases", "مشتريات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "4111000000", 20, "Expenses", "مصروفات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "3110000000", 19, "Incomes", "إيرادات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2250000001", 18, "Advances Income", "ايرادات مستحقة", 0, 0m, 1, 1, false, true, "2250000000", 0m },
                    { "2250000000", 18, "Advances Income", "ايرادات مستحقة", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2240000000", 17, "Accrued Expenses", "مصروفات مستحقة", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2230000000", 16, "Creditors", "دائنون", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2220000000", 15, "Taxes", "ضرائب", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2210000000", 14, "Suppliers", "موردين", 0, 0m, 1, 1, true, true, "", 0m },
                    { "2170000001", 13, "NotePayable", "شيكات موردين", 0, 0m, 1, 1, false, true, "2170000000", 0m },
                    { "2170000000", 13, "NotePayable", "شيكات موردين", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1269000000", 12, "OtherCurrentAsset", "أصول متداولة أخري", 0, 0m, 1, 1, true, true, "", 0m },
                    { "5110000000", 22, "Capital", "رأس المال", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1263000000", 11, "Suppliers Advances", "دفعات مقدمة للموردين", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1261000000", 9, "Custody", "عهد", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1250000000", 8, "Store", "مخزن", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1240000003", 7, "Bounced Checks", "شيكات مرتدة", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000002", 7, "Checks In Bank", "شيكات في البنك", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000001", 7, "Checks In Safe", "شيكات في الخزنة", 0, 0m, 1, 1, true, false, "1240000000", 0m },
                    { "1240000000", 7, "Checks", "شيكات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1230000001", 6, "Clients", "عملاء", 0, 0m, 1, 1, false, true, "1230000000", 0m },
                    { "1230000000", 6, "Clients", "عملاء", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1220000000", 5, "Banks", "بنوك", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1210000000", 4, "Safes", "خزن", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1130000000", 3, "Furnitiures", "أثاث", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1120000000", 2, "Machines And Equipments", "أجهزة و معدات", 0, 0m, 1, 1, true, true, "", 0m },
                    { "1262000000", 10, "StaffAdvances", "سلف", 0, 0m, 1, 1, true, true, "", 0m },
                    { "5120000000", 23, "WithDrawls", "مسحوبات", 0, 0m, 1, 1, true, true, "", 0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1110000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1120000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1130000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1210000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1220000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1230000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1230000001");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1240000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1240000001");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1240000002");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1240000003");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1250000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1261000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1262000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1263000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "1269000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2170000001");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2210000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2220000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2230000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2240000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "2250000001");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "3110000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "4111000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "4112000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5110000000");

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChart",
                keyColumn: "AccNum",
                keyValue: "5120000000");

            migrationBuilder.DeleteData(
                table: "Finance_Settings_Currency",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Finance_GL_AccountChartCounter",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Finance_Settings_Branch",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Finance_Settings_Currency",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
