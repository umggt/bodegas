using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Serilog;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db
{
    public sealed class BodegasContext : DbContext
    {
        //public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<OpcionDeMenu> OpcionesDeMenu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Log.Debug("No está configurado BodegasContext, se utiliza Sqlite por defecto.");
                // Para los migrations
                optionsBuilder.UseSqlite("Data Source=bodegas-for-migrations.sqlite");    
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpcionDeMenu>(opcion => opcion
                .HasOne(x => x.OpcionPadre)
                .WithMany(x => x.Opciones)
                .HasForeignKey(x => x.OpcionPadreId)
            );

            //modelBuilder.Entity<Usuario>(usuario =>
            //{
            //    usuario
            //        .HasAlternateKey(x => x.Login);

            //    usuario
            //        .HasMany(x => x.Atributos)
            //        .WithOne(x => x.Usuario)
            //        .HasForeignKey(x => x.UsuarioId)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});
        }
    }
}
