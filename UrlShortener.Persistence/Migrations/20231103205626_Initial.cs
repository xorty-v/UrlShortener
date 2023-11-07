using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUrl = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    HitCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ShortName",
                table: "Urls",
                column: "ShortName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Urls");
        }
    }
}
