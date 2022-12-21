using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projektna.Migrations
{
    public partial class Update0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Sold",
                table: "Vehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Vehicle");
        }
    }
}
