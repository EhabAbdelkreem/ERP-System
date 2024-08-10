using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class NotesPayable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentLiabilties_NP_NotesPayable",
                columns: table => new
                {
                    ChkNum = table.Column<string>(maxLength: 25, nullable: false),
                    WritingDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountForgin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    BankAccountNum = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(maxLength: 15, nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLiabilties_NP_NotesPayable", x => x.ChkNum);
                    table.ForeignKey(
                        name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_GL_AccountChart_BankAccountNum",
                        column: x => x.BankAccountNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrentLiabilties_NP_NotesPayable_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrentLiabilties_NP_NotesPayable_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(nullable: false),
                    TransId = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusAfterAction = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLiabilties_NP_NotesPayableTransactionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentLiabilties_NP_NotesPayableTransactionHistory_CurrentLiabilties_NP_NotesPayable_ChkNum",
                        column: x => x.ChkNum,
                        principalTable: "CurrentLiabilties_NP_NotesPayable",
                        principalColumn: "ChkNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_BankAccountNum",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "BankAccountNum");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_CurrencyId",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayable_SupplierId",
                table: "CurrentLiabilties_NP_NotesPayable",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLiabilties_NP_NotesPayableTransactionHistory_ChkNum",
                table: "CurrentLiabilties_NP_NotesPayableTransactionHistory",
                column: "ChkNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentLiabilties_NP_NotesPayableTransactionHistory");

            migrationBuilder.DropTable(
                name: "CurrentLiabilties_NP_NotesPayable");
        }
    }
}
