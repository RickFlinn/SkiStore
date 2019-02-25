using Microsoft.EntityFrameworkCore.Migrations;

namespace SkiStore.Migrations.SkiStoreProductDb
{
    public partial class firstSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Category", "Description", "ImageURL", "Name", "Price", "Quantity", "SKU" },
                values: new object[,]
                {
                    { 1, null, null, null, "Skis", 300m, 0, null },
                    { 2, null, null, null, "Helmet", 150m, 0, null },
                    { 3, null, null, null, "Goggles", 60m, 0, null },
                    { 4, null, null, null, "Poles", 100m, 0, null },
                    { 5, null, null, null, "Boots", 200m, 0, null },
                    { 6, null, null, null, "Jacket", 100m, 0, null },
                    { 7, null, null, null, "Pants", 100m, 0, null },
                    { 8, null, null, null, "Gloves", 40m, 0, null },
                    { 9, null, null, null, "Flask", 20m, 0, null },
                    { 10, null, null, null, "Buff", 40m, 0, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10);
        }
    }
}
