using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Am.Repository.Ef.Migrations
{
    public partial class UpdateSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "WeatherForecast",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5376), new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5374), new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5376) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "WeatherForecast",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 4, 18, 46, 618, DateTimeKind.Utc).AddTicks(3943), new DateTime(2022, 7, 14, 4, 18, 46, 618, DateTimeKind.Utc).AddTicks(3942), new DateTime(2022, 7, 14, 4, 18, 46, 618, DateTimeKind.Utc).AddTicks(3944) });
        }
    }
}
