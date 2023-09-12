using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiPerpusApi.Migrations
{
    public partial class changeMigrationsUserAndRole1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NAME",
                table: "M_USERS",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NAME",
                table: "M_USERS");
        }
    }
}
