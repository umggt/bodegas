using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace core.Migrations
{
    public partial class UsuarioNombreCompletoActivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Usuario",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Usuario Etiqueta = NombreCompleto;");
            migrationBuilder.DropColumn(name: "Activo", table: "Usuario");
            migrationBuilder.DropColumn(name: "NombreCompleto", table: "Usuario");
        }
    }
}
