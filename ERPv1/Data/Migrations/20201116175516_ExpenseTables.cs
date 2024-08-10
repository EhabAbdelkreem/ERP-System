using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class ExpenseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                table: "CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                table: "CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                table: "CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayableTransactionHistory_CurrentLiabilties_NP_NotesPayable_ChkNum",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentLiabilties_NP_NotesPayable",
                table: "CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.RenameTable(
                name: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                newName: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.RenameTable(
                name: "CurrentLiabilties_NP_NotesPayable",
                newName: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                newName: "IX_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_SupplierId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                newName: "IX_Finance_CurrentLiabilties_NP_NotesPayable_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_CurrencyId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                newName: "IX_Finance_CurrentLiabilties_NP_NotesPayable_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_BankAccountNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                newName: "IX_Finance_CurrentLiabilties_NP_NotesPayable_BankAccountNum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Finance_CurrentLiabilties_NP_NotesPayable",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "ChkNum");

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseTypeName = table.Column<string>(maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HR_Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseName = table.Column<string>(nullable: true),
                    AccNum = table.Column<string>(nullable: true),
                    ExpenseTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseItem_Finance_GL_AccountChart_AccNum",
                        column: x => x.AccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseItem_Finance_Expense_ExpenseType_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "Finance_Expense_ExpenseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Expense_ExpenseSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseItemId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    ExpenseDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    LocalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostCenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Expense_ExpenseSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_HR_Department_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "HR_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_Finance_Expense_ExpenseItem_ExpenseItemId",
                        column: x => x.ExpenseItemId,
                        principalTable: "Finance_Expense_ExpenseItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Finance_Expense_ExpenseType",
                columns: new[] { "Id", "ExpenseTypeName" },
                values: new object[,]
                {
                    { 1, "Managment Expenses" },
                    { 2, "Banking Expenses" }
                });

            migrationBuilder.InsertData(
                table: "HR_Department",
                columns: new[] { "Id", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "HR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseItem_AccNum",
                table: "Finance_Expense_ExpenseItem",
                column: "AccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseItem_ExpenseTypeId",
                table: "Finance_Expense_ExpenseItem",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_CostCenterId",
                table: "Finance_Expense_ExpenseSummary",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_CurrencyId",
                table: "Finance_Expense_ExpenseSummary",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_ExpenseItemId",
                table: "Finance_Expense_ExpenseSummary",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Expense_ExpenseSummary_SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "BankAccountNum",
                principalTable: "Finance_GL_AccountChart",
                principalColumn: "AccNum",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "CurrencyId",
                principalTable: "Finance_Settings_Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable",
                column: "SupplierId",
                principalTable: "CRM_Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_Finance_CurrentLiabilties_NP_NotesPayable_ChkNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "ChkNum",
                principalTable: "Finance_CurrentLiabilties_NP_NotesPayable",
                principalColumn: "ChkNum",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                table: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_Finance_CurrentLiabilties_NP_NotesPayable_ChkNum",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseSummary");

            migrationBuilder.DropTable(
                name: "HR_Department");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseItem");

            migrationBuilder.DropTable(
                name: "Finance_Expense_ExpenseType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                table: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Finance_CurrentLiabilties_NP_NotesPayable",
                table: "Finance_CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.RenameTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                newName: "CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.RenameTable(
                name: "Finance_CurrentLiabilties_NP_NotesPayable",
                newName: "CurrentLiabilties_NP_NotesPayable");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                newName: "IX_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_SupplierId",
                table: "CurrentLiabilties_NP_NotesPayable",
                newName: "IX_CurrentLiabilties_NP_NotesPayable_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_CurrencyId",
                table: "CurrentLiabilties_NP_NotesPayable",
                newName: "IX_CurrentLiabilties_NP_NotesPayable_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Finance_CurrentLiabilties_NP_NotesPayable_BankAccountNum",
                table: "CurrentLiabilties_NP_NotesPayable",
                newName: "IX_CurrentLiabilties_NP_NotesPayable_BankAccountNum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentLiabilties_NP_NotesPayableTransactionHistory",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentLiabilties_NP_NotesPayable",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "ChkNum");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "BankAccountNum",
                principalTable: "Finance_GL_AccountChart",
                principalColumn: "AccNum",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "CurrencyId",
                principalTable: "Finance_Settings_Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "SupplierId",
                principalTable: "CRM_Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLiabilties_NP_NotesPayableTransactionHistory_CurrentLiabilties_NP_NotesPayable_ChkNum",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "ChkNum",
                principalTable: "CurrentLiabilties_NP_NotesPayable",
                principalColumn: "ChkNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
