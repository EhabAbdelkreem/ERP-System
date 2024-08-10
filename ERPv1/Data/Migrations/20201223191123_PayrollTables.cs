using Microsoft.EntityFrameworkCore.Migrations;

namespace ERPv1.Data.Migrations
{
    public partial class PayrollTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                table: "Finance_Expense_ExpenseSummary");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Finance_Supplier_Purchase",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "HR_Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    InsuranceSalary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HR_Employee_HR_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "HR_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HR_SalaryBatch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchMonth = table.Column<int>(nullable: false),
                    BatchYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_SalaryBatch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HR_SalaryDetails",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    BatchId = table.Column<int>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    Allowonces = table.Column<decimal>(nullable: false),
                    Overtime = table.Column<decimal>(nullable: false),
                    Commision = table.Column<decimal>(nullable: false),
                    InsuranceEmployer = table.Column<decimal>(nullable: false),
                    GrossIncome = table.Column<decimal>(nullable: false),
                    InsuranceEmployee = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    StaffAdvance = table.Column<decimal>(nullable: false),
                    Penalties = table.Column<decimal>(nullable: false),
                    TotalDeductions = table.Column<decimal>(nullable: false),
                    NetIncome = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HR_SalaryDetails", x => new { x.EmployeeId, x.BatchId });
                    table.ForeignKey(
                        name: "FK_HR_SalaryDetails_HR_SalaryBatch_BatchId",
                        column: x => x.BatchId,
                        principalTable: "HR_SalaryBatch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HR_SalaryDetails_HR_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "HR_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HR_Employee",
                columns: new[] { "Id", "BasicSalary", "DepartmentId", "InsuranceSalary", "Name", "Phone", "Title" },
                values: new object[,]
                {
                    { 1, 5000m, 1, 2500m, "Ahmed", "0124558880", "HR Manager" },
                    { 2, 10000m, 2, 6000m, "Mohamed", "0124558881", "IT Manager" },
                    { 3, 20000m, 3, 7000m, "Peter", "0124558882", "Finance Manager" }
                });

            migrationBuilder.InsertData(
                table: "HR_SalaryBatch",
                columns: new[] { "Id", "BatchMonth", "BatchYear" },
                values: new object[] { 1, 12, 2020 });

            migrationBuilder.CreateIndex(
                name: "IX_Finance_Supplier_Purchase_CurrencyId",
                table: "Finance_Supplier_Purchase",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_HR_Employee_DepartmentId",
                table: "HR_Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HR_SalaryDetails_BatchId",
                table: "HR_SalaryDetails",
                column: "BatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                column: "SupplierId",
                principalTable: "CRM_Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Supplier_Purchase_Finance_Settings_Currency_CurrencyId",
                table: "Finance_Supplier_Purchase",
                column: "CurrencyId",
                principalTable: "Finance_Settings_Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                table: "Finance_Expense_ExpenseSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_Finance_Supplier_Purchase_Finance_Settings_Currency_CurrencyId",
                table: "Finance_Supplier_Purchase");

            migrationBuilder.DropTable(
                name: "HR_SalaryDetails");

            migrationBuilder.DropTable(
                name: "HR_SalaryBatch");

            migrationBuilder.DropTable(
                name: "HR_Employee");

            migrationBuilder.DropIndex(
                name: "IX_Finance_Supplier_Purchase_CurrencyId",
                table: "Finance_Supplier_Purchase");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Finance_Supplier_Purchase");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Finance_Expense_ExpenseSummary_CRM_Contacts_SupplierId",
                table: "Finance_Expense_ExpenseSummary",
                column: "SupplierId",
                principalTable: "CRM_Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
