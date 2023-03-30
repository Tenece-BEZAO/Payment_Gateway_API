using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Payment_Gateway.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b234fe6-2590-4943-a3e9-9d239d98a3bb", "cbb5c6f7-dc1e-4caf-b45a-49bf148d9c67", "Admin", "ADMIN" },
                    { "409debe4-f57f-41a5-859c-70309c008a13", "131b0257-ace1-4135-87bb-3c38ad8380a2", "User", "USER" },
                    { "bf1a930f-d1f2-4160-a2d6-e7d70480dc23", "55b51e48-c1dd-4bf5-becb-ac683b7cd268", "ThirdParty", "THIRDPARTY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b234fe6-2590-4943-a3e9-9d239d98a3bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "409debe4-f57f-41a5-859c-70309c008a13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf1a930f-d1f2-4160-a2d6-e7d70480dc23");
        }
    }
}
