using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdly_BE.Migrations
{
    public partial class RenameImageUrlsToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Vendors",
                newName: "Images");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Vendors",
                newName: "ImageUrls");
        }
    }
}
