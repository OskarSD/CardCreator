using Microsoft.EntityFrameworkCore.Migrations;

namespace Card_Creator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    TypeColor = table.Column<string>(nullable: true),
                    Life = table.Column<int>(nullable: false),
                    Damage = table.Column<int>(nullable: false),
                    Mana = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ColorCode = table.Column<string>(nullable: true),
                    LifeMin = table.Column<int>(nullable: false),
                    LifeMax = table.Column<int>(nullable: false),
                    DamageMin = table.Column<int>(nullable: false),
                    DamageMax = table.Column<int>(nullable: false),
                    ManaMin = table.Column<int>(nullable: false),
                    ManaMax = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
