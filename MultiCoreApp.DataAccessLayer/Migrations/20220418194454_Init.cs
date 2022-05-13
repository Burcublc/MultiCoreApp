using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiCoreApp.DataAccessLayer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProducts_tblCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCategories",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("98bc8f9a-2444-4116-ab10-8000f68324f4"), false, "Defterler" },
                    { new Guid("cd7717fe-caaa-44a4-bf7b-0a86767325d5"), false, "Kalemler" }
                });

            migrationBuilder.InsertData(
                table: "tblCustomers",
                columns: new[] { "Id", "Address", "City", "Email", "IsDeleted", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("98bc8f9a-2444-4116-ab10-8000f68324f4"), "Aydinli mah. İstanbul", "İstanbul", "ozan@gmail.com", false, "Ozan BALCI", "0165454" },
                    { new Guid("cd7717fe-caaa-44a4-bf7b-0a86767325d5"), "Aydinli mah. İstanbul", "İstanbul", "burcu@gmail.com", false, "Burcu BALCI", "02456546" }
                });

            migrationBuilder.InsertData(
                table: "tblProducts",
                columns: new[] { "Id", "CategoryId", "IsDeleted", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("112521c7-3022-49e2-8c0d-0cdcad9f0ada"), new Guid("98bc8f9a-2444-4116-ab10-8000f68324f4"), false, "Dumduz Defter", 12.19m, 100 },
                    { new Guid("58b50ac9-ee44-48f1-b88b-66dbda181042"), new Guid("cd7717fe-caaa-44a4-bf7b-0a86767325d5"), false, "Tükenmez Kalem", 18.06m, 100 },
                    { new Guid("81e06acd-1567-49cd-83eb-bbcdbae9a124"), new Guid("98bc8f9a-2444-4116-ab10-8000f68324f4"), false, "Karali Defter", 8.06m, 100 },
                    { new Guid("b7b568e8-0926-4734-9c31-25f5d5089eec"), new Guid("cd7717fe-caaa-44a4-bf7b-0a86767325d5"), false, "Kursun Kalem", 62.19m, 100 },
                    { new Guid("b921a963-b4a1-4717-bd65-8d2ae25fce16"), new Guid("98bc8f9a-2444-4116-ab10-8000f68324f4"), false, "Çizgili Defter", 22.53m, 100 },
                    { new Guid("ff0d6e4f-78e6-4340-a14f-a76a2cee88ca"), new Guid("cd7717fe-caaa-44a4-bf7b-0a86767325d5"), false, "Dolma Kalem", 122.53m, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProducts_CategoryId",
                table: "tblProducts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCustomers");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
