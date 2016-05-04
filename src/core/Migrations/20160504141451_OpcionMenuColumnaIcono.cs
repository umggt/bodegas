using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace core.Migrations
{
    public partial class OpcionMenuColumnaIcono : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icono",
                table: "OpcionesDeMenu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Icono", table: "OpcionesDeMenu");
        }
    }
}
