using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkiStore.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkiStoreUsers",
                keyColumn: "Id",
                keyValue: "db6a2d0b-0b9e-4b99-affb-962909ac32ef");

            migrationBuilder.InsertData(
                table: "SkiStoreUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aa062382-24fe-4883-b567-f55dd8af26c0", 0, "4501f42d-9152-4b86-8730-414b433e093d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1), null, false, "Skier", "Skimanovski", false, null, null, null, null, null, false, null, false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkiStoreUsers",
                keyColumn: "Id",
                keyValue: "aa062382-24fe-4883-b567-f55dd8af26c0");

            migrationBuilder.InsertData(
                table: "SkiStoreUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "db6a2d0b-0b9e-4b99-affb-962909ac32ef", 0, "6bbd313a-eb32-43f6-8474-4a3c1441c92a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1), null, false, "Skier", "Skimanovski", false, null, null, null, null, null, false, null, false, null });
        }
    }
}
