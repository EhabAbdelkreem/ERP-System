using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class AccountChartTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_GL_AccountChartCounter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountType = table.Column<string>(maxLength: 50, nullable: false),
                    AccountCategory = table.Column<int>(nullable: false),
                    ParentAccNum = table.Column<string>(maxLength: 50, nullable: true),
                    Count = table.Column<int>(nullable: false),
                    BalanceSheet = table.Column<bool>(nullable: false),
                    IncomeStatement = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_AccountChartCounter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Settings_Branch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Settings_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Settings_Currency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(maxLength: 25, nullable: false),
                    CurrencyNameAr = table.Column<string>(maxLength: 25, nullable: false),
                    CurrencyAbbrev = table.Column<string>(maxLength: 10, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Settings_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_GL_AccountChart",
                columns: table => new
                {
                    AccNum = table.Column<string>(maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(maxLength: 150, nullable: false),
                    AccountNameAr = table.Column<string>(maxLength: 150, nullable: true),
                    AccTypeId = table.Column<int>(nullable: false),
                    AccountNature = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsParent = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    ParentAcNum = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    BranchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_GL_AccountChart", x => x.AccNum);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_GL_AccountChartCounter_AccTypeId",
                        column: x => x.AccTypeId,
                        principalTable: "Finance_GL_AccountChartCounter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_Settings_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Finance_Settings_Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_GL_AccountChart_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_AccTypeId",
                table: "Finance_GL_AccountChart",
                column: "AccTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_BranchId",
                table: "Finance_GL_AccountChart",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_GL_AccountChart_CurrencyId",
                table: "Finance_GL_AccountChart",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_GL_AccountChart");

            migrationBuilder.DropTable(
                name: "Finance_GL_AccountChartCounter");

            migrationBuilder.DropTable(
                name: "Finance_Settings_Branch");

            migrationBuilder.DropTable(
                name: "Finance_Settings_Currency");
        }
    }
}
