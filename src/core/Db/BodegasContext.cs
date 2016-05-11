using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Bodegas.Db.Mappings;
using Microsoft.Data.Entity;
using Serilog;
using Microsoft.Data.Entity.Metadata;

namespace Bodegas.Db
{
    public sealed class BodegasContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<UsuarioAtributo> UsuarioAtributos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
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

            modelBuilder
                .MapearOpcionesDeMenu()
                .MapearUsuario()
                .MapearUsuarioAtributo()
                .MapearRol()
                .MapearPermiso()
                .MapearRolPermiso()
                .MapearUsuarioRol();
        }

        

        

        
        

        

        

       
    }
}
