using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechArt.Surveys.Repositories.Migrations
{
    public partial class RemoveInitialAdminFromRoleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "76e401a9-1e91-4dff-adb7-c455cefe6fa9", "4beb0654-3b7a-4601-8b81-b284cc25a903" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "76e401a9-1e91-4dff-adb7-c455cefe6fa9", "4beb0654-3b7a-4601-8b81-b284cc25a903" });
        }
    }
}
