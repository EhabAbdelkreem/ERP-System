using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class StoreAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "PurchaseAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "StoreAccNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "PurchaseAccNum",
                principalTable: "Finance_GL_AccountChart",
                principalColumn: "AccNum",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "StoreAccNum",
                principalTable: "Finance_GL_AccountChart",
                principalColumn: "AccNum",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_GL_AccountChart_StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropColumn(
                name: "PurchaseAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropColumn(
                name: "StoreAccNum",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem");
        }
    }
}
