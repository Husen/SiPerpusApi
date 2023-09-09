using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SiPerpusApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NAME_CATEGORY = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NAME_PUBLISHER = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CODE_RACK = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CODE_BOOK = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NAME_BOOK = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CATEGORY_ID = table.Column<int>(type: "integer", nullable: false),
                    PUBLISHER_ID = table.Column<int>(type: "integer", nullable: false),
                    RACK_ID = table.Column<int>(type: "integer", nullable: false),
                    PENGARANG = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ISBN = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    PAGE_BOOK = table.Column<int>(type: "integer", nullable: false),
                    YEAR_BOOK = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    STOCK = table.Column<int>(type: "integer", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PUBLISHER_ID",
                        column: x => x.PUBLISHER_ID,
                        principalTable: "Publishers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Racks_RACK_ID",
                        column: x => x.RACK_ID,
                        principalTable: "Racks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CATEGORY_ID",
                table: "Books",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PUBLISHER_ID",
                table: "Books",
                column: "PUBLISHER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_RACK_ID",
                table: "Books",
                column: "RACK_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Racks");
        }
    }
}
