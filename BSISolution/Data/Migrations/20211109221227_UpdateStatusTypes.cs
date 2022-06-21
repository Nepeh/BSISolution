using Microsoft.EntityFrameworkCore.Migrations;

namespace BSISolution.Data.Migrations
{
    public partial class UpdateStatusTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StatusTypes");

            migrationBuilder.AddColumn<int>(
                name: "StatusTypeId",
                table: "StatusTypes",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes",
                column: "StatusTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "StatusTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StatusTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusTypes",
                table: "StatusTypes",
                column: "Id");
        }
    }
}
