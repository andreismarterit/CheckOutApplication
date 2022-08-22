using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckOut.DataAccess.Migrations
{
    public partial class AddMissinColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Close",
                table: "Baskets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Payed",
                table: "Baskets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Close",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Payed",
                table: "Baskets");
        }
    }
}
