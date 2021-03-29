using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdly_BE.Migrations
{
    public partial class AddVendorImageUrls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "ImageUrls",
                table: "Vendors",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrls",
                table: "Vendors");
        }
    }
}
