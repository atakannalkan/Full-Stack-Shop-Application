using Microsoft.EntityFrameworkCore.Migrations;

namespace shopapp.data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Arrangement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arrangement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHome = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    DateAdded = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Arrangement",
                columns: new[] { "Id", "Content" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Telefon", "telefon" },
                    { 2, "Laptop", "laptop" },
                    { 3, "Masaüstü Bilgisayar", "bilgisayar" },
                    { 4, "Tablet", "tablet" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "DateAdded", "Description", "ImageUrl", "IsApproved", "IsHome", "Name", "Price", "Rating", "Stock", "Url" },
                values: new object[,]
                {
                    { 20, "23.01.2022 18:20:01", "<b>13.9 İnç</b> Ekran boyutu, <b>3000x2000 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i7</b> İşlemcisi <b>16 GB</b> Ram, <b>1 TB SSD</b> Depolamaya sahip sahip bir <b>Huawei</b> laptop ürünüdür.", "19.jpg", true, true, "Huawei Matebook X Pro", 18499.0, 4.5, 6, "huawei-matebook-x-pro" },
                    { 21, "23.01.2022 18:20:01", "<b>15.6 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i5</b> İşlemcisi <b>8 GB</b> Ram, <b>256 GB SSD</b> Depolamaya sahip sahip bir <b>Monster</b> laptop ürünüdür.", "20.jpg", true, true, "Monster Abra A5 V16", 11299.0, 5.0, 4, "monster-abra-a5-v16" },
                    { 22, "23.01.2022 18:20:01", "<b>17.3 İnç</b> Ekran boyutu, <b>2560x1440 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i7</b> İşlemcisi <b>32 GB</b> Ram, <b>1 TB SSD</b> Depolamaya sahip sahip bir <b>Msi</b> laptop ürünüdür.", "21.jpg", true, true, "Msi GE76 Raider 11UH", 41282.0, 5.0, 5, "msi-ge76-raider-11uh" },
                    { 23, "23.01.2022 18:20:01", "<b>16 İnç</b> Ekran boyutu, <b>2560x1440 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i9</b> İşlemcisi <b>32 GB</b> Ram, <b>4 TB SSD</b> Depolamaya sahip sahip bir <b>Casper</b> laptop ürünüdür.", "22.jpg", true, false, "Casper Excalibur G911", 24699.0, 4.5, 8, "casper-excalibur-g911" },
                    { 24, "23.01.2022 18:20:01", "<b>15.6 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i7</b> İşlemcisi <b>16 GB</b> Ram, <b>512 GB SSD</b> Depolamaya sahip sahip bir <b>Msi</b> laptop ürünüdür.", "23.jpg", true, true, "Msi GP66 Leopard 11UG", 35515.0, 5.0, 5, "msi-gp66-leopard-11ug" },
                    { 25, "23.01.2022 18:20:01", "<b>ATX</b> Anakartlar için, <b>10 Adet</b> Fan kapasitesi, <b>2 Adet</b> Harddisk kapasitesi, <b>6.75 Kg</b> Ağırlığa sahip sahip bir <b>Thermaltake</b> Masaüstü Bilgisayar ürünüdür.", "24.jpg", true, true, "Thermaltake Level 20 MT", 1444.0, 5.0, 6, "thermaltake-level-20-mt" },
                    { 28, "23.01.2022 18:20:01", "<b>ATX</b> Anakartlar için, <b>6 Adet</b> Fan kapasitesi, <b>1 HDD + 1 SSD</b> kapasitesi, <b>4.7 Kg</b> Ağırlığa sahip sahip bir <b>Gameforce</b> Masaüstü Bilgisayar ürünüdür.", "27.jpg", true, true, "Gameforce GF-8307 Glass 4X120M", 12000.0, 5.0, 9, "gameforce-gf-8307-glass-4x120m" },
                    { 27, "23.01.2022 18:20:01", "<b>ATX</b> Anakartlar için, <b>10 Adet</b> Fan kapasitesi, <b>2 Adet</b> Harddisk kapasitesi, <b>6.75 Kg</b> Ağırlığa sahip sahip bir <b>Thermaltake</b> Masaüstü Bilgisayar ürünüdür.", "26.jpg", true, false, "Power Boost VK-T01B", 1349.0, 4.5, 12, "power-boost-vk-t01b" },
                    { 19, "23.01.2022 18:20:01", "<b>15.6 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i3</b> İşlemcisi <b>8 GB</b> Ram, <b>256 GB SSD</b> Depolamaya sahip sahip bir <b>Honor</b> laptop ürünüdür.", "18.jpg", true, false, "Honor Magic Book X15 Core", 8539.0, 4.0, 12, "honor-magic-book-x15-core" },
                    { 29, "23.01.2022 18:20:01", "<b>10.4 İnç</b> Ekran boyutu, <b>64 GB</b> Dahili depolama, <b>4 GB</b> Ram, <b>8 MP</b> Kamera çözünürlüğü, <b>7040 mAh</b> Batarya kapasitesine sahip bir <b>Samsung</b> tablet ürünüdür.", "28.jpg", true, true, "Samsung Galaxy Tab S6 Lite", 3795.0, 5.0, 5, "samsung-galaxy-tab-s6-lite" },
                    { 30, "23.01.2022 18:20:01", "<b>10.1 İnç</b> Ekran boyutu, <b>64 GB</b> Dahili depolama, <b>4 GB</b> Ram, <b>8 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Lenovo</b> tablet ürünüdür.", "29.jpg", true, true, "Lenovo Tab m10 Mtk Helio", 2099.0, 4.5, 7, "lenovo-tab-m10-mtk-helio" },
                    { 31, "23.01.2022 18:20:01", "<b>8.7 İnç</b> Ekran boyutu, <b>32 GB</b> Dahili depolama, <b>3 GB</b> Ram, <b>8 MP</b> Kamera çözünürlüğü, <b>5100 mAh</b> Batarya kapasitesine sahip bir <b>Samsung</b> tablet ürünüdür.", "30.jpg", true, true, "Samsung Galaxy Tab A7 Lite", 1427.0, 5.0, 5, "samsung-galaxy-tab-a7-lite" },
                    { 32, "23.01.2022 18:20:01", "<b>8.3 İnç</b> Ekran boyutu, <b>64 GB</b> Dahili depolama, <b>4 GB</b> Ram, <b>12 MP</b> Kamera çözünürlüğü, <b>5124 mAh</b> Batarya kapasitesine sahip bir <b>Apple</b> tablet ürünüdür.", "32.jpg", true, true, "Apple iPad mini", 7099.0, 5.0, 6, "apple-ipad-mini" },
                    { 26, "23.01.2022 18:20:01", "<b>ATX</b> Anakartlar için, <b>6 Adet</b> Fan kapasitesi, <b>3 Adet</b> Harddisk kapasitesi, <b>5.6 Kg</b> Ağırlığa sahip sahip bir <b></b> Masaüstü Bilgisayar ürünüdür.", "25.jpg", true, false, "Msi Mag Forge Kasa", 927.0, 4.5, 9, "msi-mag-forge-100mm" },
                    { 18, "23.01.2022 18:20:01", "<b>15.6 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i7</b> İşlemcisi <b>16 GB</b> Ram, <b>512 GB SSD</b> Depolamaya sahip sahip bir <b>Asus</b> laptop ürünüdür.", "17.jpg", true, false, "Asus Rog Strix G15", 27690.0, 4.0, 11, "asus-rog-strix-g15" },
                    { 17, "23.01.2022 18:20:01", "<b>16.2 İnç</b> Ekran boyutu, <b>3456x2234 Piksel</b> Ekran çözünürlüğü, <b>Apple M1 Pro</b> İşlemcisi <b>16 GB</b> Ram, <b>512 GB SSD</b> Depolamaya sahip sahip bir <b>Apple</b> laptop ürünüdür.", "16.jpg", true, false, "Macbook Pro MK183TU/A M1 Pro", 16423.0, 4.0, 7, "macbook-pro-m1-pro" },
                    { 6, "23.01.2022 18:20:01", "<b>6.7 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>6 GB</b> Ram, <b>12 MP</b> Kamera çözünürlüğü, <b>3687 mAh</b> Batarya kapasitesine sahip bir <b>Apple</b> telefon ürünüdür.", "6.jpg", true, true, "iPhone 12 Pro Max", 20579.0, 5.0, 4, "iphone-12-pro-max" },
                    { 1, "23.01.2022 18:20:01", "<b>6.1 İnç</b> Ekran boyutu, <b>64 GB</b> Dahili depolama, <b>3GB</b> Ram, <b>12 MP</b> Kamera çözünürlüğü, <b>2942 mAh</b> Batarya kapasitesine sahip bir <b>Apple</b> telefon ürünüdür.", "1.jpg", true, true, "Apple iPhone Xr", 8923.0, 5.0, 4, "apple-iphone-xr" },
                    { 2, "23.01.2022 18:20:01", "<b>6.7 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>6 GB </b> Ram, <b>12 MP</b> Kamera çözünürlüğü, <b>4352 mAh</b> Batarya kapasitesine sahip bir <b>Apple</b> telefon ürünüdür.", "2.jpg", true, true, "iPhone 13 Pro Max", 24499.0, 5.0, 3, "iphone-13-pro-max" },
                    { 3, "23.01.2022 18:20:01", "<b>6.6 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>8 GB</b> Ram, <b>64 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Xiaomi</b> telefon ürünüdür.", "3.jpg", true, false, "Poco X3 GT", 4749.0, 4.0, 7, "poxo-x3-gt" },
                    { 4, "23.01.2022 18:20:01", "<b>6.5 İnç</b> Ekran boyutu,<b>128 GB</b> Dahili depolama, <b>6GB Ram</b>, <b>12 MP</b> Kamera çözünürlüğü, <b>4500 mAh</b> Batarya kapasitesine sahip bir <b>Samsung</b> telefon ürünüdür.", "4.jpg", true, true, "Samsung Galaxy S20 Fe", 6785.0, 4.5, 4, "samsung-galaxy-s20" },
                    { 5, "23.01.2022 18:20:01", "<b>6.4 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>6 GB</b> Ram, <b>64 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Samsung</b> telefon ürünüdür.", "5.jpg", true, true, "Samsung Galaxy A32", 3921.0, 4.0, 8, "samsung-galaxy-s32" },
                    { 16, "23.01.2022 18:20:01", "<b>15.6 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>İntel Core i5</b> İşlemcisi <b>16 GB</b> Ram, <b>512 GB SSD</b> Depolamaya sahip sahip bir <b>Huawei</b> laptop ürünüdür.", "15.jpg", true, true, "Huawei Matebook 15 Core", 7008.0, 4.5, 9, "huawei-matebook-15-core" },
                    { 7, "23.01.2022 18:20:01", "<b>6.4 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>8 GB</b> Ram, <b>48 MP</b> Kamera çözünürlüğü, <b>4310 mAh</b> Batarya kapasitesine sahip bir <b>Oppo</b> telefon ürünüdür.", "7.jpg", true, false, "Oppo Reno 5 Lite", 4399.0, 3.5, 10, "oppo-reno-5-lite" },
                    { 8, "23.01.2022 18:20:01", "<b>6.6 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>6 GB</b> Ram, <b>48 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>General Mobile</b> telefon ürünüdür.", "8.jpg", true, false, "General Mobile Gm21 Pro", 3233.0, 3.0, 10, "general-mobile-gm21-pro" },
                    { 9, "23.01.2022 18:20:01", "<b>6.5 İnç</b> Ekran boyutu, <b>32 GB</b> Dahili depolama, <b>3 GB</b> Ram, <b>13 MP</b> Kamera çözünürlüğü, <b>5020 mAh</b> Batarya kapasitesine sahip bir <b>Xiaomi</b> telefon ürünüdür.", "9.jpg", true, true, "Xiaomi Redmi 9", 2950.0, 4.5, 5, "xiaomi-redmi-9" },
                    { 10, "23.01.2022 18:20:01", "<b>6.4 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>4 GB</b> Ram, <b>48 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Oppo</b> telefon ürünüdür.", "10.jpg", true, false, "Oppo A74", 3299.0, 4.5, 6, "oppo-a74" },
                    { 11, "23.01.2022 18:20:01", "<b>6.6 İnç</b> Ekran boyutu, <b>256 GB</b> Dahili depolama, <b>8 GB</b> Ram, <b>108 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Xiaomi</b> telefon ürünüdür.", "11.jpg", true, true, "Xiaomi Mi 11T", 9799.0, 4.5, 5, "xiaomi-mi-11t" },
                    { 12, "23.01.2022 18:20:01", "<b>6.4 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>4 GB</b> Ram, <b>48 MP</b> Kamera çözünürlüğü, <b>5000 mAh</b> Batarya kapasitesine sahip bir <b>Oppo</b> telefon ürünüdür.", "12.jpg", true, false, "Oppo Realme 8 Pro", 3299.0, 3.5, 10, "oppo-realme-8-pro" },
                    { 13, "23.01.2022 18:20:01", "<b>6.7 İnç</b> Ekran boyutu, <b>128 GB</b> Dahili depolama, <b>8 GB</b> Ram, <b>12 MP</b> Kamera çözünürlüğü, <b>4800 mAh</b> Batarya kapasitesine sahip bir <b>Samsung</b> telefon ürünüdür.", "31.jpg", true, true, "Samsung Galaxy S21 Plus", 13885.0, 5.0, 8, "samsung-galaxy-s21-plus" },
                    { 14, "23.01.2022 18:20:01", "<b>16.1 İnç</b> Ekran boyutu, <b>1920x1080 Piksel</b> Ekran çözünürlüğü, <b>Amd Ryzen 5 4600H</b> İşlemcisi <b>16 GB</b> Ram, <b>512 GB SSD</b> Depolamaya sahip sahip bir <b>Huawei</b> laptop ürünüdür.", "13.jpg", true, true, "Huawei Matebook D 16", 10499.0, 5.0, 7, "huawei-matebook-d-16" },
                    { 15, "23.01.2022 18:20:01", "<b>14 İnç</b> Ekran boyutu, <b>2160x1440 Piksel</b> Ekran çözünürlüğü, <b>Amd Ryzen 5 4600 H</b> İşlemcisi <b>8 GB</b> Ram, <b>256 GB SSD</b> Depolamaya sahip sahip bir <b>Huawei</b> laptop ürünüdür.", "14.jpg", true, false, "Huawei Matebook 14", 9499.0, 4.0, 10, "huawei-matebook-14" }
                });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "Active", "ImageUrl", "Order" },
                values: new object[,]
                {
                    { 4, false, "slider4.png", 4 },
                    { 3, false, "slider3.png", 3 },
                    { 5, false, "slider5.png", 5 },
                    { 1, true, "slider1.png", 1 },
                    { 2, false, "slider2.png", 2 }
                });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "Active", "ImageUrl", "Order" },
                values: new object[] { 6, false, "slider6.png", 6 });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 30 },
                    { 4, 29 },
                    { 3, 28 },
                    { 3, 27 },
                    { 3, 26 },
                    { 3, 25 },
                    { 2, 24 },
                    { 2, 23 },
                    { 2, 22 },
                    { 2, 21 },
                    { 2, 20 },
                    { 2, 19 },
                    { 2, 18 },
                    { 2, 17 },
                    { 2, 16 },
                    { 2, 15 },
                    { 2, 14 },
                    { 1, 13 },
                    { 1, 12 },
                    { 1, 11 },
                    { 1, 10 },
                    { 1, 9 },
                    { 1, 8 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 5 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 4, 31 },
                    { 4, 32 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arrangement");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
