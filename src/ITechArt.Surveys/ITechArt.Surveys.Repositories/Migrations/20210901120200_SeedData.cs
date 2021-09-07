using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.Surveys.Repositories.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "76e401a9-1e91-4dff-adb7-c455cefe6fa9", "4179d8bd-907e-4293-bf2b-5a4598e34551", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b03bd4cc-93a8-4623-ab9d-606823a1547e", "a00343f0-cc82-452e-b00b-663216eadce8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4beb0654-3b7a-4601-8b81-b284cc25a903", 0, "01fbbd27-bd79-4f36-a892-384df2a5cea6", "egorfedorenko.w@gmail.com", false, false, null, "EGORFEDORENKO.W@GMAIL.COM", "EGORFEDORENKO", "AQAAAAEAACcQAAAAEDxts21ZFCTO9PJMekWmZIcRpZFtuqrjSI4xwd76L0h5zF3WoQlhE015Xr+kBSDqsw==", null, false, "9dd2b025-477a-4ab2-af59-dfe6f16ea4e7", false, "EgorFedorenko" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "76e401a9-1e91-4dff-adb7-c455cefe6fa9", "4beb0654-3b7a-4601-8b81-b284cc25a903" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b03bd4cc-93a8-4623-ab9d-606823a1547e", "4beb0654-3b7a-4601-8b81-b284cc25a903" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "76e401a9-1e91-4dff-adb7-c455cefe6fa9", "4beb0654-3b7a-4601-8b81-b284cc25a903" });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b03bd4cc-93a8-4623-ab9d-606823a1547e", "4beb0654-3b7a-4601-8b81-b284cc25a903" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "76e401a9-1e91-4dff-adb7-c455cefe6fa9");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b03bd4cc-93a8-4623-ab9d-606823a1547e");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "4beb0654-3b7a-4601-8b81-b284cc25a903");
        }
    }
}
