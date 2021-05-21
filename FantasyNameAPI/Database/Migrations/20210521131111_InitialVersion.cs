using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyNameAPI.Database.Migrations
{
    public partial class InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fantasy_items",
                columns: table => new
                {
                    race = table.Column<string>(type: "text", nullable: false),
                    names = table.Column<List<string>>(type: "text[]", nullable: true),
                    descriptions = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fantasy_items", x => x.race);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fantasy_items");
        }
    }
}
