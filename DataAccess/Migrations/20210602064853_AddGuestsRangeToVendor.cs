using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddGuestsRangeToVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestsMax",
                table: "Vendors",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuestsMin",
                table: "Vendors",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuestsMax",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "GuestsMin",
                table: "Vendors");
        }
    }
}
