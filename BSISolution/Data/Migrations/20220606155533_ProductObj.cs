using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSISolution.Data.Migrations
{
    public partial class ProductObj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    ProductPrice = table.Column<double>(nullable: false),
                    ParVolume = table.Column<int>(nullable: false),
                    UnitPerBox = table.Column<int>(nullable: false),
                    ExpiratioDate = table.Column<DateTime>(nullable: false),
                    AvailibleBalance = table.Column<int>(nullable: false),
                    RecordedBy = table.Column<string>(nullable: true),
                    DateRecorded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
