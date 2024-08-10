using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class PurchaseTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxationCard",
                table: "CRM_Contacts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    UnitMeasureId = table.Column<int>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithSN = table.Column<bool>(nullable: false),
                    StoreSystem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItem_Finance_CurrentAsset_Inventory_Settings_UnitMeasure_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "Finance_CurrentAsset_Inventory_Settings_UnitMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Supplier_Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "Date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVAT = table.Column<bool>(nullable: false),
                    VATAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountWithVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFullyPaid = table.Column<bool>(nullable: false),
                    Paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNum = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_Supplier_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_Purchase_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(nullable: false),
                    StoreTransType = table.Column<int>(nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseId = table.Column<int>(nullable: true),
                    QtyBalanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_Supplier_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Finance_Supplier_Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreTransaction_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finance_Supplier_SupplierTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: true),
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
                    table.PrimaryKey("PK_Finance_Supplier_SupplierTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_Settings_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Finance_Settings_Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_AccountChart_PaymentAccNum",
                        column: x => x.PaymentAccNum,
                        principalTable: "Finance_GL_AccountChart",
                        principalColumn: "AccNum",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_Supplier_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Finance_Supplier_Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_CRM_Contacts_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "CRM_Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_Supplier_SupplierTransaction_Finance_GL_Journal_TransId",
                        column: x => x.TransId,
                        principalTable: "Finance_GL_Journal",
                        principalColumn: "TransId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(nullable: false),
                    StoreTransactionId = table.Column<int>(nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_Finance_CurrentAsset_Inventory_Main_StoreTransaction_StoreTransa~",
                        column: x => x.StoreTransactionId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreItemId = table.Column<int>(nullable: false),
                    TransactionId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_Finance_CurrentAsset_Inventory_Main_StoreItem_StoreItemId",
                        column: x => x.StoreItemId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_Finance_CurrentAsset_Inventory_Main_StoreTransaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_BrandId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_ProductTypeId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItem_UnitMeasureId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItem",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails_StoreTransactionId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails",
                column: "StoreTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreItemWithSN_TransactionId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_PurchaseId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_CurrentAsset_Inventory_Main_StoreTransaction_StoreItemId",
                table: "Finance_CurrentAsset_Inventory_Main_StoreTransaction",
                column: "StoreItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchase_SupplierId",
                table: "Finance_Supplier_Purchase",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_CurrencyId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_PaymentAccNum",
                table: "Finance_Supplier_SupplierTransaction",
                column: "PaymentAccNum");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_PurchaseId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_SupplierId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_SupplierTransaction_TransId",
                table: "Finance_Supplier_SupplierTransaction",
                column: "TransId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemBalanceDetails");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItemWithSN");

            migrationBuilder.DropTable(
                name: "Finance_Supplier_SupplierTransaction");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreTransaction");

            migrationBuilder.DropTable(
                name: "Finance_Supplier_Purchase");

            migrationBuilder.DropTable(
                name: "Finance_CurrentAsset_Inventory_Main_StoreItem");

            migrationBuilder.DropColumn(
                name: "TaxationCard",
                table: "CRM_Contacts");
        }
    }
}
