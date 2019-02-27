using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkiStore.Migrations
{
    public partial class oi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkiStoreUsers",
                keyColumn: "Id",
                keyValue: "41aa1622-396a-4b5f-92aa-f2fe45f99326");

            migrationBuilder.AddColumn<bool>(
                name: "AgreedToWaiver",
                table: "SkiStoreUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "SkiStoreUsers",
                columns: new[] { "Id", "AccessFailedCount", "AgreedToWaiver", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "382173c1-668e-46e2-90a3-490a928c3ac9", 0, true, "dcb42ae4-7e74-429f-848e-89de0e0587e7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1), null, false, "Skier", "Skimanovski", false, null, null, null, null, null, false, null, false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkiStoreUsers",
                keyColumn: "Id",
                keyValue: "382173c1-668e-46e2-90a3-490a928c3ac9");

            migrationBuilder.DropColumn(
                name: "AgreedToWaiver",
                table: "SkiStoreUsers");

            migrationBuilder.InsertData(
                table: "SkiStoreUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "41aa1622-396a-4b5f-92aa-f2fe45f99326", 0, "02576680-ce57-4b54-8748-8870d410557f", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1), null, false, "Skier", "Skimanovski", false, null, null, null, null, null, false, null, false, null });
        }
    }
}
