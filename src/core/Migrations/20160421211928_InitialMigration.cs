using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpcionesDeMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true),
                    OpcionPadreId = table.Column<int>(nullable: true),
                    Ruta = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcionDeMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcionDeMenu_OpcionDeMenu_OpcionPadreId",
                        column: x => x.OpcionPadreId,
                        principalTable: "OpcionesDeMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("OpcionesDeMenu");
        }
    }
}
