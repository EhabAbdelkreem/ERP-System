using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class InventorySettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Settings_UnitMeasure", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_Brand");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_ProductType");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure");
        }
    }
}
