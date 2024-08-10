using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class FiniancialPeriodTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_GL_FiniacialPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_FiniacialPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_HistoricalBalance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialPeriodId = table.Column<int>(nullable: false),
                    AccNum = table.Column<string>(maxLength: 50, nullable: false),
                    AccountDetailsAccNum = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_HistoricalBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_GL_HistoricalBalance_Finance_GL_AccountChart_AccountDetailsAccNum",
                        column: x => x.AccountDetailsAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_GL_HistoricalBalance_Finance_GL_FiniacialPeriod_FinancialPeriodId",
                        column: x => x.FinancialPeriodId,
                        principalTable: "Finance_GL_FiniacialPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Finance_GL_FiniacialPeriod",
                columns: new[] { "Id", "YearName" },
                values: new object[] { 1, "2020-2021" });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_HistoricalBalance_AccountDetailsAccNum",
                table: "Finance_GL_HistoricalBalance",
                column: "AccountDetailsAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_HistoricalBalance_FinancialPeriodId",
                table: "Finance_GL_HistoricalBalance",
                column: "FinancialPeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_GL_HistoricalBalance");

            migrationBuilder.DropTable(
                name: "Finance_GL_FiniacialPeriod");
        }
    }
}
