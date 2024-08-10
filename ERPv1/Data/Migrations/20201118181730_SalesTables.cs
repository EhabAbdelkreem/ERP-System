using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class SalesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                maxLength: 6,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Finance_Sales_Invoices",
                columns: table => new
                {
                    InoviceNum = table.Column<string>(maxLength: 6, nullable: false),
                    InvoiceCount = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    ContactDetailsId = table.Column<int>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    IsVAT = table.Column<bool>(nullable: false),
                    VATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Sales_Invoices", x => x.InoviceNum);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_Invoices_CRM_Contacts_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_Invoices_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Sales_ClientTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    InvoiceNum = table.Column<string>(nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "Date", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PaymentAccNum = table.Column<string>(nullable: true),
                    PaymentMenthod = table.Column<int>(nullable: false),
                    TransId = table.Column<string>(nullable: true),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Sales_ClientTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_CRM_Contacts_ClientId",
                        column: x => x.ClientId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_Sales_Invoices_InvoiceNum",
                        column: x => x.InvoiceNum,
                        principalTable: "Finance_Sales_Invoices",
                        principalColumn: "InoviceNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_GL_AccountChart_PaymentAccNum",
                        column: x => x.PaymentAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Sales_ClientTransaction_Finance_GL_Journal_TransId",
                        column: x => x.TransId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "TransId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "InvoiceNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_ClientId",
                table: "Finance_Sales_ClientTransaction",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_CurrencyId",
                table: "Finance_Sales_ClientTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_InvoiceNum",
                table: "Finance_Sales_ClientTransaction",
                column: "InvoiceNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_PaymentAccNum",
                table: "Finance_Sales_ClientTransaction",
                column: "PaymentAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_ClientTransaction_TransId",
                table: "Finance_Sales_ClientTransaction",
                column: "TransId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_Invoices_ContactDetailsId",
                table: "Finance_Sales_Invoices",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Sales_Invoices_CurrencyId",
                table: "Finance_Sales_Invoices",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_Sales_Invoices_InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "InvoiceNum",
                principalTable: "Finance_Sales_Invoices",
                principalColumn: "InoviceNum",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_Sales_Invoices_InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction");

            migrationBuilder.DropTable(
                name: "Finance_Sales_ClientTransaction");

            migrationBuilder.DropTable(
                name: "Finance_Sales_Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction");

            migrationBuilder.DropColumn(
                name: "InvoiceNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction");
        }
    }
}
