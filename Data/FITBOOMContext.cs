using Microsoft.EntityFrameworkCore;
using FITBOOM.Models;

namespace FITBOOM.Data
{
    public class FITBOOMContext : DbContext
    {
        public FITBOOMContext(DbContextOptions<FITBOOMContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Membresia> Membresia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Membresia>()
                .HasKey(m => m.IdMembresia);

            // Mapea a la tabla en plural de SQL Server
            modelBuilder.Entity<Membresia>()
                .ToTable("Membresias");
        }
    }
}