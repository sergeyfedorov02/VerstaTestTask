using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerstaTestTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CityRegion = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    SenderCityId = table.Column<long>(type: "INTEGER", nullable: false),
                    SenderAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RecipientCityId = table.Column<long>(type: "INTEGER", nullable: false),
                    RecipientAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CargoWeight = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_RecipientCityId",
                        column: x => x.RecipientCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Cities_SenderCityId",
                        column: x => x.SenderCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecipientCityId",
                table: "Orders",
                column: "RecipientCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SenderCityId",
                table: "Orders",
                column: "SenderCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
