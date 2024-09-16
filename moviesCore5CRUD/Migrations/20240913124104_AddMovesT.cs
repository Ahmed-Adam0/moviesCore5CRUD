using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace moviesCore5CRUD.Migrations
{
    public partial class AddMovesT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "generas",
                columns: table => new
                {
                    id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    StoreLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    poster = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    GeneraId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movies_generas_GeneraId",
                        column: x => x.GeneraId,
                        principalTable: "generas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movies_GeneraId",
                table: "movies",
                column: "GeneraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "generas");
        }
    }
}
