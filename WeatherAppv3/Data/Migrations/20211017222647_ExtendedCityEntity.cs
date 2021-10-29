using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherAppv3.Data.Migrations
{
    public partial class ExtendedCityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TempC = table.Column<int>(type: "INTEGER", nullable: false),
                    Rainfall = table.Column<int>(type: "INTEGER", nullable: false),
                    WindSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    AppCityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_Cities_AppCityId",
                        column: x => x.AppCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_AppCityId",
                table: "WeatherData",
                column: "AppCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");
        }
    }
}
