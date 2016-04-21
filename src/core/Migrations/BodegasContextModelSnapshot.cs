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

            modelBuilder.Entity("Bodegas.Db.Entities.OpcionDeMenu", b =>
                {
                    b.HasOne("Bodegas.Db.Entities.OpcionDeMenu")
                        .WithMany()
                        .HasForeignKey("OpcionPadreId");
                });
        }
    }
}
