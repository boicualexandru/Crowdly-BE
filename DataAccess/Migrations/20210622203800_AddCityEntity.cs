using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddCityEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Vendors",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    County = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "County", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("d6744b0e-b8d8-4d1e-93b8-31eccd3f9472"), "Cluj", 46.783481799999997, 23.5464725, "Cluj-Napoca" },
                    { new Guid("8d681e75-755c-4629-a6e9-3f8dadb6a355"), "Iași", 47.156232699999997, 27.5169304, "Iași" },
                    { new Guid("81d1e28e-915f-41f3-8514-c739ccfdacfb"), "București", 44.437985300000001, 25.954553099999998, "București" },
                    { new Guid("00c3300f-fa1f-41b7-ace2-92bf08e67d06"), "Constanța", 44.181203400000001, 28.489921800000001, "Constanța" },
                    { new Guid("a849f99c-49cf-401c-a586-47c22a9ef8a3"), "Timișoara", 45.741163, 21.146549799999999, "Timișoara" },
                    { new Guid("95c39ee9-fe71-4cc5-ae6f-b4f8bda8495a"), "Brașov", 45.652576699999997, 25.526422799999999, "Brașov" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CityId",
                table: "Vendors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CityId",
                table: "Events",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Cities_CityId",
                table: "Events",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Cities_CityId",
                table: "Vendors",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Cities_CityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Cities_CityId",
                table: "Vendors");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_CityId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Events_CityId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Events");
        }
    }
}
