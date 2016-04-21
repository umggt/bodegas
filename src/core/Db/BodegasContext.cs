using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Microsoft.Data.Entity;
using Serilog;

namespace Bodegas.Db
{
    public sealed class BodegasContext : DbContext
    {
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
            modelBuilder.Entity<OpcionDeMenu>(options => options.HasOne(x => x.OpcionPadre).WithMany(x => x.Opciones).HasForeignKey(x => x.OpcionPadreId));
        }
    }
}
