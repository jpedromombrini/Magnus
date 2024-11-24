using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Magnus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bars_Products_ProductId",
                table: "Bars");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceRules_Products_ProductId",
                table: "PriceRules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRules",
                table: "PriceRules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laboratories",
                table: "Laboratories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bars",
                table: "Bars");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "PriceRules",
                newName: "PriceRule");

            migrationBuilder.RenameTable(
                name: "Laboratories",
                newName: "Laboratory");

            migrationBuilder.RenameTable(
                name: "Bars",
                newName: "Bar");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRules_ProductId",
                table: "PriceRule",
                newName: "IX_PriceRule_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Bars_ProductId",
                table: "Bar",
                newName: "IX_Bar_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "InternalCode",
                table: "Product",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Product",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "PriceRule",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Laboratory",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Laboratory",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Bar",
                type: "varchar(14)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRule",
                table: "PriceRule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laboratory",
                table: "Laboratory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bar",
                table: "Bar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bar_Product_ProductId",
                table: "Bar",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRule_Product_ProductId",
                table: "PriceRule",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bar_Product_ProductId",
                table: "Bar");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceRule_Product_ProductId",
                table: "PriceRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceRule",
                table: "PriceRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laboratory",
                table: "Laboratory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bar",
                table: "Bar");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "PriceRule",
                newName: "PriceRules");

            migrationBuilder.RenameTable(
                name: "Laboratory",
                newName: "Laboratories");

            migrationBuilder.RenameTable(
                name: "Bar",
                newName: "Bars");

            migrationBuilder.RenameIndex(
                name: "IX_PriceRule_ProductId",
                table: "PriceRules",
                newName: "IX_PriceRules_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Bar_ProductId",
                table: "Bars",
                newName: "IX_Bars_ProductId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<int>(
                name: "InternalCode",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "PriceRules",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Laboratories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<int>(
                name: "Code",
                table: "Laboratories",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Bars",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceRules",
                table: "PriceRules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laboratories",
                table: "Laboratories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bars",
                table: "Bars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bars_Products_ProductId",
                table: "Bars",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceRules_Products_ProductId",
                table: "PriceRules",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
