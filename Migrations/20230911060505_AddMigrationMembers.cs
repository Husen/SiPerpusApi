using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SiPerpusApi.Migrations
{
    public partial class AddMigrationMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FULL_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EMAIL = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
