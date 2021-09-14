using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.Surveys.Repositories.Migrations
{
    public partial class RegistrationDateForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4beb0654-3b7a-4601-8b81-b284cc25a903",
                column: "RegistrationDate",
                value: new DateTime(2021, 9, 14, 13, 2, 32, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Users");
        }
    }
}
