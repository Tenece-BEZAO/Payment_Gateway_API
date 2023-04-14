using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Payment_Gateway.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseNEW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_User_UserId1",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId1",
                table: "Wallets");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2f6bb297-9ca7-4ed8-9505-2780efb0ad3d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3fa2910f-5c60-4f11-8638-ad0d0f598720");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b0631c90-2d9d-4358-a086-24796fb48ed5");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payouts");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payouts",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "IsSuccessful",
                table: "Payouts",
                newName: "responsestatus");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payoutId",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "recipient",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "reference",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Payouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApiSecretKey",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    ApiSecretKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.ApiSecretKey);
                    table.ForeignKey(
                        name: "FK_ApiKeys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ad95772-b9e8-4361-9e36-f7c64edc6f4e", "0515ff83-9224-4e5e-a8e1-159d11b92269", "Admin", "ADMIN" },
                    { "8b5f1ba4-909f-4177-90c1-bf8cd91b2aa3", "bd5da0ad-32da-4b63-8120-c53cc7d334be", "User", "USER" },
                    { "de837cf3-4d55-413b-b502-026e82e653c2", "e4944b48-3a00-49e8-bc96-b7cde2c3a268", "ThirdParty", "THIRDPARTY" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_UserId",
                table: "ApiKeys",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallets_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6ad95772-b9e8-4361-9e36-f7c64edc6f4e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8b5f1ba4-909f-4177-90c1-bf8cd91b2aa3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "de837cf3-4d55-413b-b502-026e82e653c2");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "payoutId",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "recipient",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "source",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Payouts");

            migrationBuilder.DropColumn(
                name: "ApiSecretKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Payouts",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "responsestatus",
                table: "Payouts",
                newName: "IsSuccessful");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Wallets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Payouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f6bb297-9ca7-4ed8-9505-2780efb0ad3d", "7bdaea07-ac06-450c-a697-e97bf4449d16", "ThirdParty", "THIRDPARTY" },
                    { "3fa2910f-5c60-4f11-8638-ad0d0f598720", "789fc331-7f46-49d5-963f-d46897a31416", "Admin", "ADMIN" },
                    { "b0631c90-2d9d-4358-a086-24796fb48ed5", "e85e5e38-7fb7-4789-8819-eeaa4321310b", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId1",
                table: "Wallets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_User_UserId1",
                table: "Wallets",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
