using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empresa.Proyecto.Infra.Migrations
{
    public partial class AddNewEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewEntity_SimpleEntity_OptionId",
                        column: x => x.OptionId,
                        principalTable: "SimpleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewEntity_OptionId",
                table: "NewEntity",
                column: "OptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewEntity");
        }
    }
}
