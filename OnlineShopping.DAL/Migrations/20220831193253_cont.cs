using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopping.DAL.Migrations
{
    public partial class cont : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Item",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Item");
        }
    }
}
