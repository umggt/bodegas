using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bodegas.Db;

namespace core.Migrations
{
    [DbContext(typeof(BodegasContext))]
    partial class BodegasContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Bodegas.Db.Entities.Bodega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("UsuarioCreacionId");

                    b.Property<int>("UsuarioModificaId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Egreso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BodegaId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.EgresoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cantidad");

                    b.Property<int>("EgresoId");

                    b.Property<int>("MarcaId");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UnidadDeMedidaId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Existencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductoId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ExistenciaCantidad", b =>
                {
                    b.Property<int>("ExistenciaId");

                    b.Property<int>("UnidadDeMedidaId");

                    b.Property<int>("MarcaId");

                    b.Property<decimal>("Cantidad");

                    b.Property<DateTime>("FechaModificacion");

                    b.HasKey("ExistenciaId", "UnidadDeMedidaId", "MarcaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Ingreso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BodegaId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("ProveedorId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.IngresoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cantidad");

                    b.Property<int>("IngresoId");

                    b.Property<int>("MarcaId");

                    b.Property<string>("NumeroDeSerie")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<decimal>("Precio");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UnidadDeMedidaId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.IngresoProductoCaracteristica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CaracteristicaId");

                    b.Property<int>("IngresoProductoId");

                    b.Property<int?>("ListaValorId");

                    b.Property<string>("Valor");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Lista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ListaValor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ListaId");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.OpcionDeMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .HasAnnotation("MaxLength", 300);

                    b.Property<string>("Icono")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("OpcionPadreId");

                    b.Property<string>("Ruta")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "OpcionesDeMenu");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .HasAnnotation("MaxLength", 5000);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoCaracteristica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExpresionDeValidacion");

                    b.Property<int?>("ListaId");

                    b.Property<decimal?>("Maximo");

                    b.Property<decimal?>("Minimo");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<int>("ProductoId");

                    b.Property<bool>("Requerido");

                    b.Property<int>("TipoCaracteristica");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoMarca", b =>
                {
                    b.Property<int>("ProductoId");

                    b.Property<int>("MarcaId");

                    b.HasKey("ProductoId", "MarcaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoUnidadDeMedida", b =>
                {
                    b.Property<int>("ProductoId");

                    b.Property<int>("UnidadDeMedidaId");

                    b.HasKey("ProductoId", "UnidadDeMedidaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaModificacion");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("NombreDeContacto")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<int>("UsuarioCreacionId");

                    b.Property<int>("UsuarioModificaId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProveedorProducto", b =>
                {
                    b.Property<int>("ProveedorId");

                    b.Property<int>("ProductoId");

                    b.HasKey("ProveedorId", "ProductoId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProveedorTelefono", b =>
                {
                    b.Property<int>("ProveedorId");

                    b.Property<long>("Telefono");

                    b.HasKey("ProveedorId", "Telefono");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.RolPermiso", b =>
                {
                    b.Property<int>("RolId");

                    b.Property<int>("PermisoId");

                    b.HasKey("RolId", "PermisoId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.UnidadDeMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activo");

                    b.Property<string>("Apellidos")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<byte[]>("Clave")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("CorreoVerificado");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 400);

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("SitioWeb")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.UsuarioAtributo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<int>("UsuarioId");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 3000);

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId", "Nombre");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.UsuarioRol", b =>
                {
                    b.Property<int>("UsuarioId");

                    b.Property<int>("RolId");

                    b.HasKey("UsuarioId", "RolId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Bodega", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioCreacionId");

                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioModificaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Egreso", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Bodega")
                        .WithMany()
                        .HasForeignKey("BodegaId");

                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.EgresoProducto", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Egreso")
                        .WithMany()
                        .HasForeignKey("EgresoId");

                    b.HasOne("Bodegas.Db.Entities.Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.HasOne("Bodegas.Db.Entities.UnidadDeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadDeMedidaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Existencia", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ExistenciaCantidad", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Existencia")
                        .WithMany()
                        .HasForeignKey("ExistenciaId");

                    b.HasOne("Bodegas.Db.Entities.Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("Bodegas.Db.Entities.UnidadDeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadDeMedidaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Ingreso", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Bodega")
                        .WithMany()
                        .HasForeignKey("BodegaId");

                    b.HasOne("Bodegas.Db.Entities.Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.IngresoProducto", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Ingreso")
                        .WithMany()
                        .HasForeignKey("IngresoId");

                    b.HasOne("Bodegas.Db.Entities.Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.HasOne("Bodegas.Db.Entities.UnidadDeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadDeMedidaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.IngresoProductoCaracteristica", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.ProductoCaracteristica")
                        .WithMany()
                        .HasForeignKey("CaracteristicaId");

                    b.HasOne("Bodegas.Db.Entities.IngresoProducto")
                        .WithMany()
                        .HasForeignKey("IngresoProductoId");

                    b.HasOne("Bodegas.Db.Entities.ListaValor")
                        .WithMany()
                        .HasForeignKey("ListaValorId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ListaValor", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Lista")
                        .WithMany()
                        .HasForeignKey("ListaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.OpcionDeMenu", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.OpcionDeMenu")
                        .WithMany()
                        .HasForeignKey("OpcionPadreId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoCaracteristica", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Lista")
                        .WithMany()
                        .HasForeignKey("ListaId");

                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoMarca", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProductoUnidadDeMedida", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.HasOne("Bodegas.Db.Entities.UnidadDeMedida")
                        .WithMany()
                        .HasForeignKey("UnidadDeMedidaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Proveedor", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioCreacionId");

                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioModificaId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProveedorProducto", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.HasOne("Bodegas.Db.Entities.Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.ProveedorTelefono", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.RolPermiso", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Permiso")
                        .WithMany()
                        .HasForeignKey("PermisoId");

                    b.HasOne("Bodegas.Db.Entities.Rol")
                        .WithMany()
                        .HasForeignKey("RolId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.UsuarioAtributo", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.UsuarioRol", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.Rol")
                        .WithMany()
                        .HasForeignKey("RolId");

                    b.HasOne("Bodegas.Db.Entities.Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });
        }
    }
}
