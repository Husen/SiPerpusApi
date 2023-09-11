using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SiPerpusApi.Migrations
{
    public partial class addMigrationLoans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CODE_LOAN = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MEMBER_ID = table.Column<int>(type: "integer", nullable: false),
                    DURATION = table.Column<int>(type: "integer", nullable: false),
                    START_DATE_LOAN = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    END_DATE_LOAN = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    STATUS_LOAN = table.Column<int>(type: "integer", nullable: false),
                    TOTAL_DAILY_FINES = table.Column<int>(type: "integer", nullable: true),
                    AMERCEMENT = table.Column<int>(type: "integer", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Loans_Members_MEMBER_ID",
                        column: x => x.MEMBER_ID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LOAN_ID = table.Column<int>(type: "integer", nullable: false),
                    LOAN_CODE = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BOOK_ID = table.Column<int>(type: "integer", nullable: false),
                    QTY = table.Column<int>(type: "integer", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LoanDetails_Books_BOOK_ID",
                        column: x => x.BOOK_ID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanDetails_Loans_LOAN_ID",
                        column: x => x.LOAN_ID,
                        principalTable: "Loans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_BOOK_ID",
                table: "LoanDetails",
                column: "BOOK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LOAN_ID",
                table: "LoanDetails",
                column: "LOAN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_MEMBER_ID",
                table: "Loans",
                column: "MEMBER_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDetails");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
