using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SkiStore.Migrations
{
    public partial class prodInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageURL = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CartEntries",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    CartID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntries", x => new { x.CartID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_CartEntries_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntries_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CartEntries_ProductID",
                table: "CartEntries",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntries");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
