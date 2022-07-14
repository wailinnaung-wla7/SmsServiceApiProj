using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Am.Repository.Ef.Migrations
{
    public partial class DataSeedingHelloWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3592), new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3589), new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3592) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(222), new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(219), new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(223) });
        }
    }
}
