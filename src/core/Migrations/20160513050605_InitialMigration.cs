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
                    Icono = table.Column<string>(nullable: true),
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
            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.Id);
                    table.UniqueConstraint("AK_Permiso_Nombre", x => x.Nombre);
                });
            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                    table.UniqueConstraint("AK_Rol_Nombre", x => x.Nombre);
                });
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Activo = table.Column<bool>(nullable: false),
                    Apellidos = table.Column<string>(nullable: true),
                    Clave = table.Column<byte[]>(nullable: false),
                    Correo = table.Column<string>(nullable: false),
                    CorreoVerificado = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    NombreCompleto = table.Column<string>(nullable: false),
                    Nombres = table.Column<string>(nullable: false),
                    SitioWeb = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.UniqueConstraint("AK_Usuario_Login", x => x.Login);
                });
            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    RolId = table.Column<int>(nullable: false),
                    PermisoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => new { x.RolId, x.PermisoId });
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permiso_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permiso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermiso_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "UsuarioAtributo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAtributo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioAtributo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    RolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAtributo_UsuarioId_Nombre",
                table: "UsuarioAtributo",
                columns: new[] { "UsuarioId", "Nombre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("OpcionesDeMenu");
            migrationBuilder.DropTable("RolPermiso");
            migrationBuilder.DropTable("UsuarioAtributo");
            migrationBuilder.DropTable("UsuarioRol");
            migrationBuilder.DropTable("Permiso");
            migrationBuilder.DropTable("Rol");
            migrationBuilder.DropTable("Usuario");
        }
    }
}
