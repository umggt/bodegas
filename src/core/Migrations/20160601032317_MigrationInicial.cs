using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace core.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });
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
                });
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
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
                });
            migrationBuilder.CreateTable(
                name: "UnidadDeMedida",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadDeMedida", x => x.Id);
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
                });
            migrationBuilder.CreateTable(
                name: "ListaValor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ListaId = table.Column<int>(nullable: false),
                    Valor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaValor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaValor_Lista_ListaId",
                        column: x => x.ListaId,
                        principalTable: "Lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Existencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Existencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Existencia_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ProductoCaracteristica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExpresionDeValidacion = table.Column<string>(nullable: true),
                    ListaId = table.Column<int>(nullable: true),
                    Maximo = table.Column<decimal>(nullable: true),
                    Minimo = table.Column<decimal>(nullable: true),
                    Nombre = table.Column<string>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    Requerido = table.Column<bool>(nullable: false),
                    TipoCaracteristica = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoCaracteristica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductoCaracteristica_Lista_ListaId",
                        column: x => x.ListaId,
                        principalTable: "Lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoCaracteristica_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ProductoMarca",
                columns: table => new
                {
                    ProductoId = table.Column<int>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoMarca", x => new { x.ProductoId, x.MarcaId });
                    table.ForeignKey(
                        name: "FK_ProductoMarca_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoMarca_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ProductoUnidadDeMedida",
                columns: table => new
                {
                    ProductoId = table.Column<int>(nullable: false),
                    UnidadDeMedidaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoUnidadDeMedida", x => new { x.ProductoId, x.UnidadDeMedidaId });
                    table.ForeignKey(
                        name: "FK_ProductoUnidadDeMedida_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoUnidadDeMedida_UnidadDeMedida_UnidadDeMedidaId",
                        column: x => x.UnidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Bodega",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    UsuarioCreacionId = table.Column<int>(nullable: false),
                    UsuarioModificaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodega", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bodega_Usuario_UsuarioCreacionId",
                        column: x => x.UsuarioCreacionId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bodega_Usuario_UsuarioModificaId",
                        column: x => x.UsuarioModificaId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    NombreDeContacto = table.Column<string>(nullable: true),
                    UsuarioCreacionId = table.Column<int>(nullable: false),
                    UsuarioModificaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedor_Usuario_UsuarioCreacionId",
                        column: x => x.UsuarioCreacionId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proveedor_Usuario_UsuarioModificaId",
                        column: x => x.UsuarioModificaId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
            migrationBuilder.CreateTable(
                name: "ExistenciaCantidad",
                columns: table => new
                {
                    ExistenciaId = table.Column<int>(nullable: false),
                    UnidadDeMedidaId = table.Column<int>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<decimal>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistenciaCantidad", x => new { x.ExistenciaId, x.UnidadDeMedidaId, x.MarcaId });
                    table.ForeignKey(
                        name: "FK_ExistenciaCantidad_Existencia_ExistenciaId",
                        column: x => x.ExistenciaId,
                        principalTable: "Existencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExistenciaCantidad_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExistenciaCantidad_UnidadDeMedida_UnidadDeMedidaId",
                        column: x => x.UnidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Egreso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BodegaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Egreso_Bodega_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodega",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Egreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Ingreso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BodegaId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    ProveedorId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingreso_Bodega_BodegaId",
                        column: x => x.BodegaId,
                        principalTable: "Bodega",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingreso_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "ProveedorProducto",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorProducto", x => new { x.ProveedorId, x.ProductoId });
                    table.ForeignKey(
                        name: "FK_ProveedorProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedorProducto_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ProveedorTelefono",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(nullable: false),
                    Telefono = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorTelefono", x => new { x.ProveedorId, x.Telefono });
                    table.ForeignKey(
                        name: "FK_ProveedorTelefono_Proveedor_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "EgresoProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    EgresoId = table.Column<int>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    UnidadDeMedidaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EgresoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EgresoProducto_Egreso_EgresoId",
                        column: x => x.EgresoId,
                        principalTable: "Egreso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EgresoProducto_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EgresoProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EgresoProducto_UnidadDeMedida_UnidadDeMedidaId",
                        column: x => x.UnidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "IngresoProducto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    IngresoId = table.Column<int>(nullable: false),
                    MarcaId = table.Column<int>(nullable: false),
                    NumeroDeSerie = table.Column<string>(nullable: true),
                    Precio = table.Column<decimal>(nullable: false),
                    ProductoId = table.Column<int>(nullable: false),
                    UnidadDeMedidaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_Ingreso_IngresoId",
                        column: x => x.IngresoId,
                        principalTable: "Ingreso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngresoProducto_UnidadDeMedida_UnidadDeMedidaId",
                        column: x => x.UnidadDeMedidaId,
                        principalTable: "UnidadDeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "IngresoProductoCaracteristica",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CaracteristicaId = table.Column<int>(nullable: false),
                    IngresoProductoId = table.Column<int>(nullable: false),
                    ListaValorId = table.Column<int>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoProductoCaracteristica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresoProductoCaracteristica_ProductoCaracteristica_CaracteristicaId",
                        column: x => x.CaracteristicaId,
                        principalTable: "ProductoCaracteristica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngresoProductoCaracteristica_IngresoProducto_IngresoProductoId",
                        column: x => x.IngresoProductoId,
                        principalTable: "IngresoProducto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresoProductoCaracteristica_ListaValor_ListaValorId",
                        column: x => x.ListaValorId,
                        principalTable: "ListaValor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });
            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAtributo_UsuarioId_Nombre",
                table: "UsuarioAtributo",
                columns: new[] { "UsuarioId", "Nombre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("EgresoProducto");
            migrationBuilder.DropTable("ExistenciaCantidad");
            migrationBuilder.DropTable("IngresoProductoCaracteristica");
            migrationBuilder.DropTable("OpcionesDeMenu");
            migrationBuilder.DropTable("ProductoMarca");
            migrationBuilder.DropTable("ProductoUnidadDeMedida");
            migrationBuilder.DropTable("ProveedorProducto");
            migrationBuilder.DropTable("ProveedorTelefono");
            migrationBuilder.DropTable("RolPermiso");
            migrationBuilder.DropTable("UsuarioAtributo");
            migrationBuilder.DropTable("UsuarioRol");
            migrationBuilder.DropTable("Egreso");
            migrationBuilder.DropTable("Existencia");
            migrationBuilder.DropTable("ProductoCaracteristica");
            migrationBuilder.DropTable("IngresoProducto");
            migrationBuilder.DropTable("ListaValor");
            migrationBuilder.DropTable("Permiso");
            migrationBuilder.DropTable("Rol");
            migrationBuilder.DropTable("Ingreso");
            migrationBuilder.DropTable("Marca");
            migrationBuilder.DropTable("Producto");
            migrationBuilder.DropTable("UnidadDeMedida");
            migrationBuilder.DropTable("Lista");
            migrationBuilder.DropTable("Bodega");
            migrationBuilder.DropTable("Proveedor");
            migrationBuilder.DropTable("Usuario");
        }
    }
}
