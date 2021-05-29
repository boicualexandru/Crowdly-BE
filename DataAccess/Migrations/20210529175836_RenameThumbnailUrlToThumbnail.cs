using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdly_BE.Migrations
{
    public partial class RenameThumbnailUrlToThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Vendors",
                newName: "Thumbnail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumbnail",
                table: "Vendors",
                newName: "ThumbnailUrl");
        }
    }
}
