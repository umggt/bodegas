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

                    b.HasAlternateKey("Nombre");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasAlternateKey("Nombre");
                });

            modelBuilder.Entity("Bodegas.Db.Entities.RolPermiso", b =>
                {
                    b.Property<int>("RolId");

                    b.Property<int>("PermisoId");

                    b.HasKey("RolId", "PermisoId");
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

                    b.HasAlternateKey("Login");
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

            modelBuilder.Entity("Bodegas.Db.Entities.OpcionDeMenu", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.OpcionDeMenu")
                        .WithMany()
                        .HasForeignKey("OpcionPadreId");
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
