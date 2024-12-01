using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferWarehouseItem_TransferWarehouse_TransferWarehouseId1",
                table: "TransferWarehouseItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferWarehouseItem_TransferWarehouseId1",
                table: "TransferWarehouseItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "TransferWarehouseId1",
                table: "TransferWarehouseItem");

            migrationBuilder.RenameTable(
                name: "ProductStocks",
                newName: "ProductStock");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "ProductStock",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ProductStock",
                type: "numeric(10,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccountsPayable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Document = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierName = table.Column<string>(type: "varchar(150)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Value = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    PaymentValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    CostCenter = table.Column<string>(type: "varchar(8)", nullable: false),
                    Installment = table.Column<int>(type: "integer", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserPaymentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Canceled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Serie = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "varchar(44)", nullable: false),
                    DateEntry = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Freight = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Deduction = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Observation = table.Column<string>(type: "varchar(200)", nullable: false),
                    InvoiceSituation = table.Column<int>(type: "integer", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountsPayableOccurrence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountsPayableId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Occurrence = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayableOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsPayableOccurrence_AccountsPayable_AccountsPayableId",
                        column: x => x.AccountsPayableId,
                        principalTable: "AccountsPayable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductInternalCode = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,3)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false),
                    Bonus = table.Column<bool>(type: "boolean", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayableOccurrence_AccountsPayableId",
                table: "AccountsPayableOccurrence",
                column: "AccountsPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                table: "InvoiceItem",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsPayableOccurrence");

            migrationBuilder.DropTable(
                name: "InvoiceItem");

            migrationBuilder.DropTable(
                name: "AccountsPayable");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock");

            migrationBuilder.RenameTable(
                name: "ProductStock",
                newName: "ProductStocks");

            migrationBuilder.AddColumn<Guid>(
                name: "TransferWarehouseId1",
                table: "TransferWarehouseItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "ProductStocks",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ProductStocks",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,3)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseItem_TransferWarehouseId1",
                table: "TransferWarehouseItem",
                column: "TransferWarehouseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferWarehouseItem_TransferWarehouse_TransferWarehouseId1",
                table: "TransferWarehouseItem",
                column: "TransferWarehouseId1",
                principalTable: "TransferWarehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
