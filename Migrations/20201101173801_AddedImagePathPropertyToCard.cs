using Microsoft.EntityFrameworkCore.Migrations;

namespace Card_Creator.Migrations
{
    public partial class AddedImagePathPropertyToCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImagePath",
                table: "Card",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Card",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortraitImagePath",
                table: "Card",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImagePath",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "PortraitImagePath",
                table: "Card");
        }
    }
}
