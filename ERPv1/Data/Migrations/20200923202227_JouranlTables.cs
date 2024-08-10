using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class JouranlTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_GL_Journal",
                columns: table => new
                {
                    TransId = table.Column<string>(maxLength: 15, nullable: false),
                    EntryDate = table.Column<DateTimeOffset>(nullable: false),
                    TransDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TransDesc = table.Column<string>(nullable: false),
                    DocName = table.Column<string>(nullable: true),
                    TransCount = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemModule = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_Journal", x => x.TransId);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_JournalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransId = table.Column<string>(maxLength: 15, nullable: true),
                    AccNum = table.Column<string>(maxLength: 15, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountLocal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Side = table.Column<int>(nullable: false),
                    BalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    UsedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_JournalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_GL_JournalDetails_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_GL_JournalDetails_Finance_GL_Journal_TransId",
                        column: x => x.TransId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "TransId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_JournalDetails_CurrencyId",
                table: "Finance_GL_JournalDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_JournalDetails_TransId",
                table: "Finance_GL_JournalDetails",
                column: "TransId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_GL_JournalDetails");

            migrationBuilder.DropTable(
                name: "Finance_GL_Journal");
        }
    }
}
