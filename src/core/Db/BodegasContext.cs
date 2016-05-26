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
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Egreso> Egresos { get; set; }
        public DbSet<EgresoProducto> EgresoProductos { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<ExistenciaCantidad> ExistenciaCantidades { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<IngresoProducto> IngresoProductos { get; set; }
        public DbSet<IngresoProductoCaracteristica> IngresoProductoCaracteristicas { get; set; }
        public DbSet<Lista> Listas { get; set; }
        public DbSet<ListaValor> ListaValores { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<OpcionDeMenu> OpcionesDeMenu { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoCaracteristica> ProductoCaracteristicas { get; set; }
        public DbSet<ProductoMarca> ProductoMarcas { get; set; }
        public DbSet<ProductoUnidadDeMedida> ProductoUnidadesDeMedida { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorProducto> ProveedorProductos { get; set; }
        public DbSet<ProveedorTelefono> ProveedorTelefonos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<UnidadDeMedida> UnidadesDeMedida { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioAtributo> UsuarioAtributos { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Log.Debug("No está configurado BodegasContext, se utiliza Sqlite por defecto.");
                // Para los migrations
                optionsBuilder.UseSqlite("Data Source=bodegas.db");    
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .MapearBodega()
                .MapearEgreso()
                .MapearEgresoProducto()
                .MapearExistencia()
                .MapearExistenciaCantidad()
                .MapearIngreso()
                .MapearIngresoProducto()
                .MapearIngresoProductoCaracteristica()
                .MapearLista()
                .MapearListaValor()
                .MapearMarca()
                .MapearOpcionesDeMenu()
                .MapearPermiso()
                .MapearProducto()
                .MapearProductoCaracteristia()
                .MapearProductoMarca()
                .MapearProductoUnidadDeMedida()
                .MapearProveedor()
                .MapearProveedorProducto()
                .MapearProveedorTelefono()
                .MapearRol()
                .MapearRolPermiso()
                //.MapearUnidadDeMedida()
                .MapearUsuario()
                .MapearUsuarioAtributo()
                .MapearUsuarioRol();
        }

        

        

        
        

        

        

       
    }
}
