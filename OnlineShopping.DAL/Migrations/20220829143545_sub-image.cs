using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopping.DAL.Migrations
{
    public partial class subimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "SubCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "SubCategory");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
