using Microsoft.EntityFrameworkCore;
using OrdenDetalle.Entidades;
using System.Collections.Generic;
using System.Text;


namespace OrdenDetalle.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=Data\BDPedidos.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 1, Nombres = "Yoma" }
                );

            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 2, Nombres = "Sirena" }
                );
            modelBuilder.Entity<Suplidores>().HasData(
                new Suplidores { SuplidorId = 3, Nombres = " Rica" }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 1, Descripcion = "Habichuela", Costo = 75, Inventario = 5 }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 2, Descripcion = "Pan", Costo = 50, Inventario = 5 }
                );

            modelBuilder.Entity<Productos>().HasData(
                new Productos { ProductoId = 3, Descripcion = "juego Manzana", Costo = 150, Inventario = 10 }
                );
        }
    }
}
