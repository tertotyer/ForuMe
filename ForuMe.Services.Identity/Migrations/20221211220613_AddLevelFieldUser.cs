using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForuMe.Services.Identity.Migrations
{
    public partial class AddLevelFieldUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Level",
                table: "AspNetUsers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetUsers");
        }
    }
}
