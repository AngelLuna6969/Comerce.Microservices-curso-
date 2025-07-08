using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    IDProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.IDProduct);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    IDProductInStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.IDProductInStock);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "IDProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "IDProduct", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Description for product 1", "Product 1", 952m },
                    { 2, "Description for product 2", "Product 2", 559m },
                    { 3, "Description for product 3", "Product 3", 947m },
                    { 4, "Description for product 4", "Product 4", 189m },
                    { 5, "Description for product 5", "Product 5", 332m },
                    { 6, "Description for product 6", "Product 6", 854m },
                    { 7, "Description for product 7", "Product 7", 423m },
                    { 8, "Description for product 8", "Product 8", 833m },
                    { 9, "Description for product 9", "Product 9", 278m },
                    { 10, "Description for product 10", "Product 10", 151m },
                    { 11, "Description for product 11", "Product 11", 322m },
                    { 12, "Description for product 12", "Product 12", 383m },
                    { 13, "Description for product 13", "Product 13", 887m },
                    { 14, "Description for product 14", "Product 14", 631m },
                    { 15, "Description for product 15", "Product 15", 550m },
                    { 16, "Description for product 16", "Product 16", 794m },
                    { 17, "Description for product 17", "Product 17", 459m },
                    { 18, "Description for product 18", "Product 18", 775m },
                    { 19, "Description for product 19", "Product 19", 537m },
                    { 20, "Description for product 20", "Product 20", 650m },
                    { 21, "Description for product 21", "Product 21", 744m },
                    { 22, "Description for product 22", "Product 22", 790m },
                    { 23, "Description for product 23", "Product 23", 110m },
                    { 24, "Description for product 24", "Product 24", 288m },
                    { 25, "Description for product 25", "Product 25", 941m },
                    { 26, "Description for product 26", "Product 26", 738m },
                    { 27, "Description for product 27", "Product 27", 813m },
                    { 28, "Description for product 28", "Product 28", 361m },
                    { 29, "Description for product 29", "Product 29", 509m },
                    { 30, "Description for product 30", "Product 30", 996m },
                    { 31, "Description for product 31", "Product 31", 915m },
                    { 32, "Description for product 32", "Product 32", 422m },
                    { 33, "Description for product 33", "Product 33", 346m },
                    { 34, "Description for product 34", "Product 34", 146m },
                    { 35, "Description for product 35", "Product 35", 321m },
                    { 36, "Description for product 36", "Product 36", 251m },
                    { 37, "Description for product 37", "Product 37", 249m },
                    { 38, "Description for product 38", "Product 38", 640m },
                    { 39, "Description for product 39", "Product 39", 804m },
                    { 40, "Description for product 40", "Product 40", 407m },
                    { 41, "Description for product 41", "Product 41", 758m },
                    { 42, "Description for product 42", "Product 42", 350m },
                    { 43, "Description for product 43", "Product 43", 856m },
                    { 44, "Description for product 44", "Product 44", 866m },
                    { 45, "Description for product 45", "Product 45", 803m },
                    { 46, "Description for product 46", "Product 46", 566m },
                    { 47, "Description for product 47", "Product 47", 949m },
                    { 48, "Description for product 48", "Product 48", 151m },
                    { 49, "Description for product 49", "Product 49", 827m },
                    { 50, "Description for product 50", "Product 50", 596m },
                    { 51, "Description for product 51", "Product 51", 744m },
                    { 52, "Description for product 52", "Product 52", 625m },
                    { 53, "Description for product 53", "Product 53", 749m },
                    { 54, "Description for product 54", "Product 54", 210m },
                    { 55, "Description for product 55", "Product 55", 461m },
                    { 56, "Description for product 56", "Product 56", 722m },
                    { 57, "Description for product 57", "Product 57", 893m },
                    { 58, "Description for product 58", "Product 58", 931m },
                    { 59, "Description for product 59", "Product 59", 141m },
                    { 60, "Description for product 60", "Product 60", 620m },
                    { 61, "Description for product 61", "Product 61", 529m },
                    { 62, "Description for product 62", "Product 62", 661m },
                    { 63, "Description for product 63", "Product 63", 812m },
                    { 64, "Description for product 64", "Product 64", 531m },
                    { 65, "Description for product 65", "Product 65", 130m },
                    { 66, "Description for product 66", "Product 66", 746m },
                    { 67, "Description for product 67", "Product 67", 853m },
                    { 68, "Description for product 68", "Product 68", 591m },
                    { 69, "Description for product 69", "Product 69", 712m },
                    { 70, "Description for product 70", "Product 70", 325m },
                    { 71, "Description for product 71", "Product 71", 842m },
                    { 72, "Description for product 72", "Product 72", 924m },
                    { 73, "Description for product 73", "Product 73", 887m },
                    { 74, "Description for product 74", "Product 74", 758m },
                    { 75, "Description for product 75", "Product 75", 359m },
                    { 76, "Description for product 76", "Product 76", 689m },
                    { 77, "Description for product 77", "Product 77", 215m },
                    { 78, "Description for product 78", "Product 78", 595m },
                    { 79, "Description for product 79", "Product 79", 706m },
                    { 80, "Description for product 80", "Product 80", 855m },
                    { 81, "Description for product 81", "Product 81", 881m },
                    { 82, "Description for product 82", "Product 82", 250m },
                    { 83, "Description for product 83", "Product 83", 967m },
                    { 84, "Description for product 84", "Product 84", 527m },
                    { 85, "Description for product 85", "Product 85", 846m },
                    { 86, "Description for product 86", "Product 86", 149m },
                    { 87, "Description for product 87", "Product 87", 805m },
                    { 88, "Description for product 88", "Product 88", 402m },
                    { 89, "Description for product 89", "Product 89", 577m },
                    { 90, "Description for product 90", "Product 90", 370m },
                    { 91, "Description for product 91", "Product 91", 995m },
                    { 92, "Description for product 92", "Product 92", 935m },
                    { 93, "Description for product 93", "Product 93", 123m },
                    { 94, "Description for product 94", "Product 94", 380m },
                    { 95, "Description for product 95", "Product 95", 281m },
                    { 96, "Description for product 96", "Product 96", 253m },
                    { 97, "Description for product 97", "Product 97", 746m },
                    { 98, "Description for product 98", "Product 98", 455m },
                    { 99, "Description for product 99", "Product 99", 100m },
                    { 100, "Description for product 100", "Product 100", 677m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "IDProductInStock", "ProductID", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 16 },
                    { 2, 2, 2 },
                    { 3, 3, 13 },
                    { 4, 4, 12 },
                    { 5, 5, 15 },
                    { 6, 6, 5 },
                    { 7, 7, 13 },
                    { 8, 8, 14 },
                    { 9, 9, 16 },
                    { 10, 10, 4 },
                    { 11, 11, 16 },
                    { 12, 12, 6 },
                    { 13, 13, 17 },
                    { 14, 14, 8 },
                    { 15, 15, 10 },
                    { 16, 16, 7 },
                    { 17, 17, 3 },
                    { 18, 18, 18 },
                    { 19, 19, 0 },
                    { 20, 20, 17 },
                    { 21, 21, 17 },
                    { 22, 22, 17 },
                    { 23, 23, 9 },
                    { 24, 24, 0 },
                    { 25, 25, 10 },
                    { 26, 26, 6 },
                    { 27, 27, 17 },
                    { 28, 28, 15 },
                    { 29, 29, 18 },
                    { 30, 30, 0 },
                    { 31, 31, 12 },
                    { 32, 32, 12 },
                    { 33, 33, 8 },
                    { 34, 34, 19 },
                    { 35, 35, 12 },
                    { 36, 36, 10 },
                    { 37, 37, 6 },
                    { 38, 38, 10 },
                    { 39, 39, 12 },
                    { 40, 40, 19 },
                    { 41, 41, 8 },
                    { 42, 42, 6 },
                    { 43, 43, 15 },
                    { 44, 44, 12 },
                    { 45, 45, 3 },
                    { 46, 46, 13 },
                    { 47, 47, 11 },
                    { 48, 48, 10 },
                    { 49, 49, 15 },
                    { 50, 50, 9 },
                    { 51, 51, 3 },
                    { 52, 52, 15 },
                    { 53, 53, 10 },
                    { 54, 54, 3 },
                    { 55, 55, 18 },
                    { 56, 56, 9 },
                    { 57, 57, 14 },
                    { 58, 58, 5 },
                    { 59, 59, 10 },
                    { 60, 60, 0 },
                    { 61, 61, 19 },
                    { 62, 62, 15 },
                    { 63, 63, 16 },
                    { 64, 64, 11 },
                    { 65, 65, 18 },
                    { 66, 66, 2 },
                    { 67, 67, 10 },
                    { 68, 68, 4 },
                    { 69, 69, 13 },
                    { 70, 70, 5 },
                    { 71, 71, 7 },
                    { 72, 72, 6 },
                    { 73, 73, 1 },
                    { 74, 74, 8 },
                    { 75, 75, 17 },
                    { 76, 76, 2 },
                    { 77, 77, 15 },
                    { 78, 78, 10 },
                    { 79, 79, 14 },
                    { 80, 80, 0 },
                    { 81, 81, 0 },
                    { 82, 82, 10 },
                    { 83, 83, 4 },
                    { 84, 84, 9 },
                    { 85, 85, 9 },
                    { 86, 86, 5 },
                    { 87, 87, 10 },
                    { 88, 88, 18 },
                    { 89, 89, 15 },
                    { 90, 90, 10 },
                    { 91, 91, 13 },
                    { 92, 92, 10 },
                    { 93, 93, 5 },
                    { 94, 94, 7 },
                    { 95, 95, 17 },
                    { 96, 96, 5 },
                    { 97, 97, 1 },
                    { 98, 98, 15 },
                    { 99, 99, 12 },
                    { 100, 100, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductID",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
