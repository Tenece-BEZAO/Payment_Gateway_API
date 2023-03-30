using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Payment_Gateway.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminIdentity = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminProfiles_Admins_AdminIdentity",
                        column: x => x.AdminIdentity,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26c51a3b-d1af-4022-a50c-8f12b09e69e2", "4e6aafae-554c-491d-a68c-3507e4704b0c", "Admin", "ADMIN" },
                    { "583a8db5-3ab3-473c-a0a8-cd4557dd03ec", "620630b3-e43f-47c2-9905-e10a723b3efd", "ThirdParty", "THIRDPARTY" },
                    { "ce203c7a-eaff-419d-b503-0708a786bdff", "a1bf38ce-acda-40c3-bcb7-c1135623cfa1", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminProfiles_AdminIdentity",
                table: "AdminProfiles",
                column: "AdminIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminProfiles");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26c51a3b-d1af-4022-a50c-8f12b09e69e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "583a8db5-3ab3-473c-a0a8-cd4557dd03ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce203c7a-eaff-419d-b503-0708a786bdff");

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
    }
}
