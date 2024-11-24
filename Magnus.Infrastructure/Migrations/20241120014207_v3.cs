using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(150)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    Occupation = table.Column<string>(type: "varchar(50)", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Address_ZipCode = table.Column<string>(type: "varchar(9)", nullable: true),
                    PublicPlace = table.Column<string>(type: "varchar(100)", nullable: true),
                    Number = table.Column<int>(type: "integer", nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(50)", nullable: true),
                    City = table.Column<string>(type: "varchar(50)", nullable: true),
                    State = table.Column<string>(type: "varchar(2)", nullable: true),
                    Complement = table.Column<string>(type: "varchar(50)", nullable: true),
                    RegisterNumber = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientPhone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "varchar(15)", nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPhone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPhone_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSocialMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Link = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSocialMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientSocialMedia_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientPhone_ClientId",
                table: "ClientPhone",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSocialMedia_ClientId",
                table: "ClientSocialMedia",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientPhone");

            migrationBuilder.DropTable(
                name: "ClientSocialMedia");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
