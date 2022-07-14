using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Am.Repository.Ef.Migrations
{
    public partial class SeedDataHelloWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "HelloWorld",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Message", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, "minmhan", new DateTime(2022, 7, 14, 5, 25, 54, 171, DateTimeKind.Utc).AddTicks(8419), true, "Hello World Message", "minmhan", new DateTime(2022, 7, 14, 5, 25, 54, 171, DateTimeKind.Utc).AddTicks(8420) });

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 5, 25, 54, 171, DateTimeKind.Utc).AddTicks(8296), new DateTime(2022, 7, 14, 5, 25, 54, 171, DateTimeKind.Utc).AddTicks(8294), new DateTime(2022, 7, 14, 5, 25, 54, 171, DateTimeKind.Utc).AddTicks(8297) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HelloWorld",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3592), new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3589), new DateTime(2022, 7, 14, 5, 11, 42, 196, DateTimeKind.Utc).AddTicks(3592) });
        }
    }
}
