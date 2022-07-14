using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Am.Repository.Ef.Migrations
{
    public partial class AddHelloWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HelloWorld",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelloWorld", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(222), new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(219), new DateTime(2022, 7, 14, 4, 45, 56, 554, DateTimeKind.Utc).AddTicks(223) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HelloWorld");

            migrationBuilder.UpdateData(
                table: "WeatherForecast",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "Date", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5376), new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5374), new DateTime(2022, 7, 14, 4, 22, 29, 833, DateTimeKind.Utc).AddTicks(5376) });
        }
    }
}
