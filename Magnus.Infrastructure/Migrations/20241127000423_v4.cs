using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Magnus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneType",
                table: "ClientPhone");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "ClientPhone",
                type: "varchar(14)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.CreateTable(
                name: "AuditProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Document = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: true),
                    Serie = table.Column<int>(type: "integer", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    TransferhouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCenterGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "varchar(2)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Crm = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidityDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Increase = table.Column<decimal>(type: "numeric", nullable: false),
                    InIstallments = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    Number = table.Column<string>(type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(14)", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "varchar(9)", nullable: false),
                    PublicPlace = table.Column<string>(type: "varchar(100)", nullable: false),
                    AddressNumber = table.Column<int>(type: "integer", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(50)", nullable: false),
                    City = table.Column<string>(type: "varchar(50)", nullable: false),
                    State = table.Column<string>(type: "varchar(2)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferWarehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    WarehouseOriginId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseOriginName = table.Column<string>(type: "varchar(100)", nullable: false),
                    WarehouseDestinyId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseDestinyName = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferWarehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostCenterSubGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "varchar(5)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CostCenterGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenterSubGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenterSubGroup_CostCenterGroup_CostCenterGroupId",
                        column: x => x.CostCenterGroupId,
                        principalTable: "CostCenterGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferWarehouseItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductInternalCode = table.Column<int>(type: "integer", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(100)", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    TransferWarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TransferWarehouseId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    Validity = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferWarehouseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseItem_TransferWarehouse_TransferWarehouseId",
                        column: x => x.TransferWarehouseId,
                        principalTable: "TransferWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferWarehouseItem_TransferWarehouse_TransferWarehouseId1",
                        column: x => x.TransferWarehouseId1,
                        principalTable: "TransferWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "varchar(8)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    CostCenterSubGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenter_CostCenterSubGroup_CostCenterSubGroupId",
                        column: x => x.CostCenterSubGroupId,
                        principalTable: "CostCenterSubGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_CostCenterSubGroupId",
                table: "CostCenter",
                column: "CostCenterSubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterSubGroup_CostCenterGroupId",
                table: "CostCenterSubGroup",
                column: "CostCenterGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseItem_TransferWarehouseId",
                table: "TransferWarehouseItem",
                column: "TransferWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferWarehouseItem_TransferWarehouseId1",
                table: "TransferWarehouseItem",
                column: "TransferWarehouseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_UserId",
                table: "Warehouse",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditProducts");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "TransferWarehouseItem");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "CostCenterSubGroup");

            migrationBuilder.DropTable(
                name: "TransferWarehouse");

            migrationBuilder.DropTable(
                name: "CostCenterGroup");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "ClientPhone",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)");

            migrationBuilder.AddColumn<int>(
                name: "PhoneType",
                table: "ClientPhone",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
