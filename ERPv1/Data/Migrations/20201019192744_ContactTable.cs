using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class ContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRM_Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    NameAR = table.Column<string>(maxLength: 255, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    IsClient = table.Column<bool>(nullable: false),
                    ClientAccNum = table.Column<string>(maxLength: 50, nullable: true),
                    ClientBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSupplier = table.Column<bool>(maxLength: 50, nullable: false),
                    SupplierAccNum = table.Column<string>(nullable: true),
                    SupplierBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRM_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CRM_Contacts_Finance_GL_AccountChart_ClientAccNum",
                        column: x => x.ClientAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CRM_Contacts_Finance_GL_AccountChart_SupplierAccNum",
                        column: x => x.SupplierAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRM_Contacts_ClientAccNum",
                table: "CRM_Contacts",
                column: "ClientAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_CRM_Contacts_SupplierAccNum",
                table: "CRM_Contacts",
                column: "SupplierAccNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRM_Contacts");
        }
    }
}
