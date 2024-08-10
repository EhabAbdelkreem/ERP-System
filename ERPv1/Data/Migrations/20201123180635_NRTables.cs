using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class NRTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckHafza",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HafzaName = table.Column<string>(maxLength: 255, nullable: true),
                    BankAccNum = table.Column<string>(maxLength: 50, nullable: true),
                    HafzaDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckHafza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckLocationEN = table.Column<string>(maxLength: 255, nullable: false),
                    CheckLocationAR = table.Column<string>(maxLength: 255, nullable: false),
                    IsDefualt = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Checks_History",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(maxLength: 255, nullable: false),
                    TransID = table.Column<string>(maxLength: 255, nullable: true),
                    TransDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Checks_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_CheckStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckStatusEN = table.Column<string>(maxLength: 255, nullable: false),
                    CheckStatusAR = table.Column<string>(maxLength: 255, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_CheckStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Checks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChkNum = table.Column<string>(maxLength: 255, nullable: false),
                    DueDate = table.Column<DateTime>(type: "Date", nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountForgin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    OrginalBank = table.Column<string>(maxLength: 255, nullable: true),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckStatusId = table.Column<int>(nullable: false),
                    CheckLocationId = table.Column<int>(nullable: false),
                    BankAccNum = table.Column<string>(maxLength: 50, nullable: true),
                    HafzaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Checks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_GL_AccountChart_BankAccNum",
                        column: x => x.BankAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckLocation_CheckLocationId",
                        column: x => x.CheckLocationId,
                        principalTable: "Finance_CurrentAsset_CheckLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckStatus_CheckStatusId",
                        column: x => x.CheckStatusId,
                        principalTable: "Finance_CurrentAsset_CheckStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_CRM_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Checks_Finance_CurrentAsset_CheckHafza_HafzaId",
                        column: x => x.HafzaId,
                        principalTable: "Finance_CurrentAsset_CheckHafza",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_CheckLocation",
                columns: new[] { "Id", "CheckLocationAR", "CheckLocationEN", "IsDefualt" },
                values: new object[,]
                {
                    { 1, "الخزنة", "Safe", true },
                    { 2, "البنك", "Bank", false },
                    { 3, "محصل", "BankCollected", false },
                    { 4, "مع العميل", "Client", false }
                });

            migrationBuilder.InsertData(
                table: "Finance_CurrentAsset_CheckStatus",
                columns: new[] { "Id", "CheckStatusAR", "CheckStatusEN", "IsDefault" },
                values: new object[,]
                {
                    { 1, "تحت التحصيل", "Under Collection", true },
                    { 2, "محصل", "Collected", true },
                    { 3, "مرتد", "Bounced", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_BankAccNum",
                table: "Finance_CurrentAsset_Checks",
                column: "BankAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CheckLocationId",
                table: "Finance_CurrentAsset_Checks",
                column: "CheckLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CheckStatusId",
                table: "Finance_CurrentAsset_Checks",
                column: "CheckStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_ContactId",
                table: "Finance_CurrentAsset_Checks",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_CurrencyId",
                table: "Finance_CurrentAsset_Checks",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Checks_HafzaId",
                table: "Finance_CurrentAsset_Checks",
                column: "HafzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Checks");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Checks_History");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckLocation");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckStatus");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_CheckHafza");
        }
    }
}
