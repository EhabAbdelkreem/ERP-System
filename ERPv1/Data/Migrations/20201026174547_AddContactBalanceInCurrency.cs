using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class AddContactBalanceInCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRM_ContactBalanceInCurrency",
                columns: table => new
                {
                    ContactId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    AccNum = table.Column<string>(maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRM_ContactBalanceInCurrency", x => new { x.ContactId, x.CurrencyId, x.AccNum });
                    table.ForeignKey(
                        name: "FK_CRM_ContactBalanceInCurrency_CRM_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CRM_ContactBalanceInCurrency_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRM_ContactBalanceInCurrency_CurrencyId",
                table: "CRM_ContactBalanceInCurrency",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRM_ContactBalanceInCurrency");
        }
    }
}
